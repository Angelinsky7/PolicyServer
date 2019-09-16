using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PolicyServer1.Models;
using PolicyServer1.ResponseHandling.Models;
using PolicyServer1.Services;
using PolicyServer1.Stores;

namespace PolicyServer1.ResponseHandling.Default {
    public class DefaultPermissionResponseGenerator : IPermissionResponseGenerator {

        private readonly IClientStore _clientStore;
        private readonly IEvaluatorService _evaluatorService;
        private readonly ILogger _logger;

        public DefaultPermissionResponseGenerator(
            IClientStore clientStore,
            IEvaluatorService evaluatorService,
            ILogger<DefaultPermissionResponseGenerator> logger
        ) {
            _clientStore = clientStore;
            _evaluatorService = evaluatorService;
            _logger = logger;
        }

        public async Task<Dictionary<String, Object>> ProcessAsync(PermissionRequest permissionRequest) {
            _logger.LogDebug("Creating permission response");

            Client client = await _clientStore.GetFromClientIdAsync(permissionRequest.ClientId);

            if (client == null) {
                _logger.LogWarning("No client found with the clientId given: {clienId}", permissionRequest.ClientId);

                return new Dictionary<String, Object> {
                    {"ErrorMessage", "client is not valid. See the log." }
                };
            }

            EvaluatorRequest request = new EvaluatorRequest() {
                Client = client,
                User = permissionRequest.User,
                Permissions = permissionRequest.Permissions
            };

            await _evaluatorService.EvaluateAsync(request);
            Evaluation evaluation = BuildEvaluation(request);

            _logger.LogDebug($"EvaluatorRequest Cache: { Newtonsoft.Json.JsonConvert.SerializeObject(request.Cache)}");
            _logger.LogDebug($"EvaluatorRequest Result: { Newtonsoft.Json.JsonConvert.SerializeObject(request.EvaluatorResults)}");

            switch (permissionRequest.ResponseMode) {
                case PermissionRequestReponseMode.Decision: {
                        Dictionary<String, Object> result = new Dictionary<String, Object> {
                            //{ nameof(client.ClientId),                      client.ClientId },
                            // { nameof(evaluation.Result),                    evaluation.Result },
                            //{ nameof(evaluation.LastPolicyChangeDate),      evaluation.LastPolicyChangeDate },
                            //{ nameof(evaluation.Roles),                     evaluation.Roles },
                            //{ nameof(evaluation.Permissions),               evaluation.Permissions }
                        };

                        return result;
                    }
                case PermissionRequestReponseMode.Permissions: {

                        // Permission : "SomeOne(Policy) cando Something(Scope) on Resource(Resource)"

                        // EvaluationResult evaluation = await _evaluatorService.EvaluateResultAsync(permissionRequest.User, client, permissionRequest.Permissions);

                        // _logger.LogDebug($"EvaluationResult: { Newtonsoft.Json.JsonConvert.SerializeObject(evaluation)}");

                        Dictionary<String, Object> result = new Dictionary<String, Object> {
                            //{ nameof(client.ClientId),                        client.ClientId },
                            { nameof(evaluation.Results),                     evaluation.Results },
                            //{ nameof(evaluation.LastPolicyChangeDate),      evaluation.LastPolicyChangeDate },
                            //{ nameof(evaluation.Roles),                     evaluation.Roles },
                            //{ nameof(evaluation.Permissions),               evaluation.Permissions }
                        };

                        return result;
                    }
                case PermissionRequestReponseMode.Analyse: {
                        if (!client.Options.AnalyseModeEnabled) {
                            return new Dictionary<String, Object> {
                                {"ErrorMessage", "client does not permit Analyse. See the log." }
                            };
                        }

                        EvaluationAnalyse analyse = await _evaluatorService.BuildEvaluationAnalyseAsync(request);

                        //TODO(demarco): i would like to have a real debug view of the data....

                        Dictionary<String, Object> result = new Dictionary<String, Object> {
                             { nameof(analyse),                             analyse },
                        };

                        return result;
                    }
            }

            return null;
        }

        private Evaluation BuildEvaluation(EvaluatorRequest request) {
            Evaluation result = new Evaluation();

            //TODO(demarco) this being here have issue with the analyse part... we must replicate it and i don't like it
            switch (request.Client.Options.DecisionStrategy) {
                case DecisionStrategy.Affirmative:
                    foreach (IGrouping<Resource, ResouceScopeResult> item in request.ResourceScopeResults
                        .Where(p => p.Granted == true)
                        .GroupBy(p => p.Resource)
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
                        .GroupBy(p => p.Resource)
                    ) {
                        result.Results.Add(new EvaluationItem {
                            RsId = item.Key.Id,
                            RsName = item.Key.Name,
                            Scopes = item.Where(p => !item.Where(a => !a.Granted.Value).Select(m => m.Scope.Id).Contains(p.Scope.Id)).Select(p => p.Scope.Name).Distinct().ToList()
                        });
                    }
                    break;
            }


            return result;
        }

    }
}
