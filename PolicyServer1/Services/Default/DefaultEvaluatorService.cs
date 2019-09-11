using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace PolicyServer1.Services.Default {
    public class DefaultEvaluatorService : IEvaluatorService {
        
        public async Task<EvaluatorRequest> EvaluateAsync(EvaluatorRequest request) {

            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Client == null) { throw new ArgumentNullException(nameof(request.Client)); }

            foreach (Permission permission in request.Client.Permissions.Where(p => request.Permissions == null || request.Permissions.Contains(p.Name))) {
                await permission.EvaluateAsync(request);
            }

            //TODO(demarco): We need to check for the system strategy here...
            //TODO(demarco): like we can remove resource that are concurrent...

            return request;
        }

        public Task<IEnumerable<Resource>> GetResources(EvaluatorRequest request, Permission permission) {
            List<Resource> result = new List<Resource>();
            if (permission is ResourcePermission resourcePermission) {
                if (resourcePermission.Resource != null) {
                    result.Add(resourcePermission.Resource);
                } else {
                    result.AddRange(request.Client.Resources.Where(p => p.Type == resourcePermission.ResouceType));
                }
            } else if (permission is ScopePermission scopePermission) {
                if (scopePermission.Resource != null) {
                    result.Add(scopePermission.Resource);
                }
            }

            if (result.Count == 0) {
                //TODO(demarco): add an empty resouce that reprents no ressources....
            }

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<IEnumerable<Scope>> GetScopes(EvaluatorRequest request, Permission permission) {
            List<Scope> result = new List<Scope>();
            if (permission is ResourcePermission resourcePermission) {
                if (resourcePermission.Resource != null) {
                    result.AddRange(resourcePermission.Resource.Scopes);
                } else {
                    result.AddRange(request.Client.Resources.Where(p => p.Type == resourcePermission.ResouceType).SelectMany(p => p.Scopes).Distinct());
                }
            } else if (permission is ScopePermission scopePermission) {
                if (scopePermission.Resource != null) {
                    result.AddRange(scopePermission.Scopes);
                }
            }

            return Task.FromResult(result.AsEnumerable());
        }
    }
}
