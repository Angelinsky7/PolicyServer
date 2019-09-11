using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace PolicyServer1.Services.Default {
    public class DefaultEvaluatorService : IEvaluatorService {

        public async Task<EvaluatorRequest> EvaluateAsync(EvaluatorRequest request) {

            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Client == null) { throw new ArgumentNullException(nameof(request.Client)); }

            foreach (Permission permission in request.Client.Permissions.Where(p => request.Permissions == null || request.Permissions.Contains(p.Name))) {
                await permission.EvaluateAsync(request);
            }

            request.ResourceScopeResults = await GetResouceScopeResults(request);

            return request;
        }

        private async Task<List<ResouceScopeResult>> GetResouceScopeResults(EvaluatorRequest request) {
            Dictionary<Int64, ResouceScopeResult> resouceScopeResults = new Dictionary<Int64, ResouceScopeResult>();

            foreach (KeyValuePair<Permission, PermissionDecision> permission in request.Results.PermissionsDecisions) {
                IEnumerable<Resource> resources = await GetResources(request, permission.Key);
                IEnumerable<Scope> scopes = await GetScopes(request, permission.Key);

                foreach (Resource resource in resources) {
                    foreach (Scope scope in scopes) {
                        Int64 hash = ResouceScopeResult.GetHash(permission.Key, resource, scope);
                        if (!resouceScopeResults.ContainsKey(hash)) {
                            resouceScopeResults.Add(hash, new ResouceScopeResult {
                                Permission = permission.Key,
                                Resource = resource,
                                Scope = scope
                            });
                        }

                        ResouceScopeResult item = resouceScopeResults[hash];
                        if (permission.Value.Result) {
                            ++item.GrantedCount;
                        } else {
                            ++item.DeniedCount;
                        }
                    }
                }

            }

            foreach (KeyValuePair<Int64, ResouceScopeResult> item in resouceScopeResults) {
                switch (request.Client.Options.DecisionStrategy) {
                    case DecisionStrategy.Affirmative:
                        item.Value.Granted = item.Value.GrantedCount == 0;
                        break;
                    case DecisionStrategy.Unanimous:
                        item.Value.Granted = item.Value.DeniedCount == 0;
                        break;
                }
            }

            return resouceScopeResults.Values.ToList();
        }

        //private async Task<Evaluation> BuildResultEvaluationAsync(EvaluatorRequest request) {
        //    Evaluation result = new Evaluation();
        //    Dictionary<Resource, List<Scope>> resourceByScopes = new Dictionary<Resource, List<Scope>>();

        //    foreach (KeyValuePair<Permission, PermissionDecision> permission in request.Result.PermissionsDecisions) {
        //        IEnumerable<Resource> resources = await _evaluatorService.GetResources(request, permission.Key);
        //        IEnumerable<Scope> scopes = await _evaluatorService.GetScopes(request, permission.Key);

        //        foreach (Resource resource in resources) {
        //            if (!resourceByScopes.ContainsKey(resource)) {
        //                resourceByScopes.Add(resource, new List<Scope>());
        //            }
        //            foreach (Scope scope in scopes) {
        //                if (!resourceByScopes[resource].Contains(scope)) {
        //                    resourceByScopes[resource].Add(scope);
        //                    //TODO(demarco): It's missing the part of DENIED permission....
        //                }

        //            }
        //        }

        //    }

        //    _logger.LogDebug($"Ressouces by Scopes: { Newtonsoft.Json.JsonConvert.SerializeObject(resourceByScopes)}");

        //    foreach (KeyValuePair<Resource, List<Scope>> resourceByScope in resourceByScopes) {
        //        result.Results.Add(new EvaluationItem {
        //            RsId = resourceByScope.Key.Id,
        //            RsName = resourceByScope.Key.Name,
        //            Scopes = resourceByScope.Value.Select(p => p.Name).ToList()
        //        });
        //    }

        //    return result;
        //}

        private Task<IEnumerable<Resource>> GetResources(EvaluatorRequest request, Permission permission) {
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

        private Task<IEnumerable<Scope>> GetScopes(EvaluatorRequest request, Permission permission) {
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
