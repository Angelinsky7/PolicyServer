using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using PolicyServer1.Configuration;
using PolicyServer1.Extensions;
using PolicyServer1.ResponseHandling.Models;
using PolicyServer1.Stores;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PolicyServer1.Infrastructure;

namespace PolicyServer1.ResponseHandling.Default {
    public class DefaultPermissionRequestParser : IPermissionRequestParser {

        private readonly PolicyServerOptions _options;
        private readonly IClientStore _clientStore;
        private readonly ILogger _logger;

        private readonly Dictionary<String, PermissionRequestReponseMode> _listOfReponseMode = Enum.GetValues(typeof(PermissionRequestReponseMode)).Cast<Enum>().ToDictionary(p => EnumStringHelper.GetStringValue(p), p => (PermissionRequestReponseMode)p);

        public DefaultPermissionRequestParser(
            PolicyServerOptions options,
            IClientStore clientStore,
            ILogger<DefaultPermissionRequestParser> logger
        ) {
            _options = options;
            _clientStore = clientStore;
            _logger = logger;
        }

        public Task<PermissionRequest> ParseAsync(HttpContext context) {
            NameValueCollection query = context.Request.Query.AsNameValueCollection();

            String cliendIdFromQuery = query.Get("client_id");
            String reponseMode = query.Get("response_mode");
            IEnumerable<String> permissions = query.GetValues("permission");

            String clientId = context.User.FindFirst(_options.ClientIdentifier)?.Value;

            if (String.IsNullOrEmpty(clientId)) {
                return Task.FromResult(new PermissionRequest {
                    IsError = true,
                    Error = "clientId is mandatory"
                });
            }

            if (cliendIdFromQuery != clientId) {
                return Task.FromResult(new PermissionRequest {
                    IsError = true,
                    Error = "clientId from query and from token are not the same"
                });
            }

            if (!_listOfReponseMode.ContainsKey(reponseMode)) {
                return Task.FromResult(new PermissionRequest {
                    IsError = true,
                    Error = $"response mode must be one of this : {String.Join(", ", _listOfReponseMode.Keys)}"
                });
            }

            _logger.LogDebug($"ClientId: {clientId}, User: {context.User}");

            return Task.FromResult(new PermissionRequest {
                IsError = false,
                ClientId = cliendIdFromQuery,
                AudienceId = clientId,
                User = context.User,
                ResponseMode = _listOfReponseMode[reponseMode],
                Permissions = permissions
            });
        }
    }
}
