using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace IdentityServerAspNetIdentity.Services {
    public class CustomConsentService : DefaultConsentService {
        public CustomConsentService(ISystemClock clock, IUserConsentStore userConsentStore, ILogger<DefaultConsentService> logger) : base(clock, userConsentStore, logger) {
        }

        public override async Task<Boolean> RequiresConsentAsync(ClaimsPrincipal subject, Client client, IEnumerable<String> scopes) {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (subject == null) throw new ArgumentNullException(nameof(subject));

            if (!client.RequireConsent) {
                Logger.LogDebug("Client is configured to not require consent, no consent is required");
                return false;
            }

            if (!client.AllowRememberConsent) {
                Logger.LogDebug("Client is configured to not allow remembering consent, consent is required");
                return true;
            }

            if (scopes == null || !scopes.Any()) {
                Logger.LogDebug("No scopes being requested, no consent is required");
                return false;
            }

            //if (scopes.Contains(IdentityServerConstants.StandardScopes.OfflineAccess)) {
            //    Logger.LogDebug("Scopes contains offline_access, consent is required");
            //    return true;
            //}

            Consent consent = await UserConsentStore.GetUserConsentAsync(subject.GetSubjectId(), client.ClientId);

            if (consent == null) {
                Logger.LogDebug("Found no prior consent from consent store, consent is required");
                return true;
            }

            if (consent.Expiration.HasExpired(Clock.UtcNow.UtcDateTime)) {
                Logger.LogDebug("Consent found in consent store is expired, consent is required");
                await UserConsentStore.RemoveUserConsentAsync(consent.SubjectId, consent.ClientId);
                return true;
            }

            if (consent.Scopes != null) {
                IEnumerable<String> intersect = scopes.Intersect(consent.Scopes);
                Boolean different = !(scopes.Count() == intersect.Count());

                if (different) {
                    Logger.LogDebug("Consent found in consent store is different than current request, consent is required");
                } else {
                    Logger.LogDebug("Consent found in consent store is same as current request, consent is not required");
                }

                return different;
            }

            Logger.LogDebug("Consent found in consent store has no scopes, consent is required");

            return true;
        }

    }
}
