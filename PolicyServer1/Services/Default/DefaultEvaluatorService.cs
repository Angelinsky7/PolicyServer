using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PolicyServer1.Extensions;
using PolicyServer1.Models;

namespace PolicyServer1.Services.Default {
    public class DefaultEvaluatorService : IEvaluatorService {

        private static readonly Func<PermissionResourceScopeItem, Boolean> _filterResourcePermissions = p => (p.ResouceName != null);
        private static Func<PermissionResourceScopeItem, Boolean> _filterScopesPermissions(Resource resource) {
            return p => (p.ResouceName == null || p.ResouceName == resource.Name) && p.ScopeName != null;
        }

        public async Task<EvaluatorRequest> EvaluateAsync(EvaluatorRequest request) {

            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Client == null) { throw new ArgumentNullException(nameof(request.Client)); }

            request.PermissionResourceScopeItems = await GetPermissionResourceScopeItems(request);

            foreach (Permission permission in FilterPermissions(request, request.Client.Permissions)) {
                await permission.EvaluateAsync(request);
            }

            request.ResourceScopeResults = await GetResouceScopeResults(request);

            return request;
        }

        private IEnumerable<Permission> FilterPermissions(EvaluatorRequest request, IEnumerable<Permission> src) {
            if (request.PermissionResourceScopeItems.Any(p => p.ResouceName == null)) { return src; }
            if (request.PermissionResourceScopeItems.Count(_filterResourcePermissions) == 0) { return src; }
            return src.Where(p => request.PermissionResourceScopeItems.Where(_filterResourcePermissions).Select(a => a.ResouceName).Contains(p.GetResourceName()));
        }

        private IEnumerable<Scope> FilterScopes(EvaluatorRequest request, Resource resource, IEnumerable<Scope> src) {
            if (request.PermissionResourceScopeItems.Any(p => p.ResouceName == resource.Name && p.ScopeName == null)) { return src; }
            if (request.PermissionResourceScopeItems.Count(_filterScopesPermissions(resource)) == 0) { return src; }
            return src.Where(p => request.PermissionResourceScopeItems.Where(_filterScopesPermissions(resource)).Select(a => a.ScopeName).Contains(p.Name));
        }

        private async Task<ICollection<ResouceScopeResult>> GetResouceScopeResults(EvaluatorRequest request) {
            Dictionary<Int64, ResouceScopeResult> resouceScopeResults = new Dictionary<Int64, ResouceScopeResult>();

            foreach (KeyValuePair<Permission, PermissionDecision> permission in request.EvaluatorResults.PermissionsDecisions) {
                IEnumerable<Resource> resources = await GetResources(request, permission.Key);
                IEnumerable<Scope> scopes = await GetScopes(request, permission.Key);

                foreach (Resource resource in resources) {
                    foreach (Scope scope in FilterScopes(request, resource, scopes)) {
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

        private Task<ICollection<PermissionResourceScopeItem>> GetPermissionResourceScopeItems(EvaluatorRequest request) {
            List<PermissionResourceScopeItem> result = new List<PermissionResourceScopeItem>();
            if (request.Permissions != null) {
                foreach (String permission in request.Permissions) {
                    String[] split = permission.Split(new String[] { request.Client.Options.PermissionSplitter }, StringSplitOptions.None);
                    if (split.Length == 2) {
                        result.Add(new PermissionResourceScopeItem {
                            ResouceName = split[0].ReturnNullIfEmpty(),
                            ScopeName = split[1].ReturnNullIfEmpty()
                        });
                    } else if (split.Length == 1) {
                        result.Add(new PermissionResourceScopeItem {
                            ResouceName = split[0].ReturnNullIfEmpty()
                        });
                    } else {
                        throw new ArgumentOutOfRangeException(nameof(request.Permissions));
                    }
                }
            }
            return Task.FromResult((ICollection<PermissionResourceScopeItem>)result);
        }

        public async Task<EvaluationAnalyse> BuildEvaluationAnalyseAsync(EvaluatorRequest request) {
            EvaluationAnalyse analyse = new EvaluationAnalyse();

            foreach (KeyValuePair<Permission, PermissionDecision> permission in request.EvaluatorResults.PermissionsDecisions) {
                IEnumerable<Resource> resources = await GetResources(request, permission.Key);
                IEnumerable<Scope> scopes = await GetScopes(request, permission.Key);

                EvaluationAnalyseItem analyseItem = null;

                foreach (Resource resource in resources) {
                    analyseItem = analyse.Items.FirstOrDefault(p => p.ResourceId == resource.Id);
                    if (analyseItem == null) {
                        analyseItem = new EvaluationAnalyseItem {
                            ResourceId = resource.Id,
                            ResourceName = resource.Name,
                            Strategy = request.Client.Options.DecisionStrategy.ToString()
                        };
                        analyse.Items.Add(analyseItem);
                    }

                    Evaluation evaluation = await BuildEvaluationAsync(request, resource);
                    foreach (String scope in evaluation.Results.SingleOrDefault(p => p.RsId == resource.Id)?.Scopes) {
                        analyseItem.Scopes.Add(scope);
                    }
                }

                if (analyseItem != null) {
                    EvaluationAnalysePermissionItem evaluationAnalysePermissionItem = new EvaluationAnalysePermissionItem {
                        PermissionId = permission.Key.Id,
                        PermissionName = permission.Key.Name,
                        Granted = permission.Value.Result,
                        Strategy = permission.Key.DecisionStrategy.ToString(),
                        Scopes = scopes.Select(p => p.Name).ToList()
                    };

                    foreach (KeyValuePair<Policy, Boolean> policy in permission.Value.Policies) {
                        evaluationAnalysePermissionItem.Policies.Add(new EvaluationAnalysePolicyItem {
                            PolicyId = policy.Key.Id,
                            PolicyName = policy.Key.Name,
                            Granted = policy.Value
                        });
                    }

                    analyseItem.Permissions.Add(evaluationAnalysePermissionItem);
                }

            }

            return analyse;
        }

        public Task<Evaluation> BuildEvaluationAsync(EvaluatorRequest request, Resource filterResouce = null) {
            Evaluation result = new Evaluation();

            switch (request.Client.Options.DecisionStrategy) {
                case DecisionStrategy.Affirmative:
                    foreach (IGrouping<Resource, ResouceScopeResult> item in request.ResourceScopeResults
                        .Where(p => p.Granted == true && filterResouce == null || p.Resource.Id == filterResouce.Id)
                        .GroupBy(p => p.Resource, ResourceComparer.Instance)
                    ) {
                        result.Results.Add(new EvaluationItem {
                            RsId = item.Key.Id,
                            RsName = item.Key.Name,
                            Scopes = item.Select(p => p.Scope.Name).Distinct().ToList()
                        });
                    }
                    break;
                case DecisionStrategy.Unanimous:
                    foreach (IGrouping<Resource, ResouceScopeResult> item in request.ResourceScopeResults
                        .Where(p => filterResouce == null || p.Resource.Id == filterResouce.Id)
                        .GroupBy(p => p.Resource, ResourceComparer.Instance)
                    ) {
                        result.Results.Add(new EvaluationItem {
                            RsId = item.Key.Id,
                            RsName = item.Key.Name,
                            Scopes = item.Where(p => !item.Where(a => !a.Granted.Value).Select(m => m.Scope.Id).Contains(p.Scope.Id)).Select(p => p.Scope.Name).Distinct().ToList()
                        });
                    }
                    break;
            }


            return Task.FromResult(result);
        }

        private Task<IEnumerable<Resource>> GetResources(EvaluatorRequest request, Permission permission) {
            List<Resource> result = new List<Resource>();
            if (permission is ResourcePermission resourcePermission) {
                if (resourcePermission.Resource != null) {
                    result.Add(resourcePermission.Resource);
                } else {
                    Regex regex = resourcePermission.ResouceType?.WildcardToRegex();
                    if (regex != null) {
                        result.AddRange(request.Client.Resources.Where(p => regex.IsMatch(p.Type)));
                    }
                }
            } else if (permission is ScopePermission scopePermission) {
                if (scopePermission.Resource != null) {
                    result.Add(scopePermission.Resource);
                }
            }

            if (result.Count == 0) {
                //TODO(demarco): add an empty resouce that reprents no resources....
                if (true) { }
            }

            return Task.FromResult(result.AsEnumerable());
        }

        private Task<IEnumerable<Scope>> GetScopes(EvaluatorRequest request, Permission permission) {
            List<Scope> result = new List<Scope>();
            if (permission is ResourcePermission resourcePermission) {
                if (resourcePermission.Resource != null) {
                    result.AddRange(resourcePermission.Resource.Scopes);
                } else {
                    Regex regex = resourcePermission.ResouceType?.WildcardToRegex();
                    if (regex != null) {
                        result.AddRange(request.Client.Resources.Where(p => regex.IsMatch(p.Type)).SelectMany(p => p.Scopes).Distinct());
                    }
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
