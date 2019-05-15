using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace IdentityServerAspNetIdentity.Services {
    public class CustomProfileService : IProfileService {

        protected readonly ILogger _Logger;

        public CustomProfileService(ILogger<CustomProfileService> logger) {
            _Logger = logger;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context) {
            context.LogProfileRequest(_Logger);
            context.AddRequestedClaims(context.Subject.Claims);
            context.LogIssuedClaims(_Logger);

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context) {
            _Logger.LogDebug("IsActive called from: {caller}", context.Caller);

            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
