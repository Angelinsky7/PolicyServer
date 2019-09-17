using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Services {
    public interface IEvaluatorService {

        Task<EvaluatorRequest> EvaluateAsync(EvaluatorRequest request);
        Task<EvaluationAnalyse> BuildEvaluationAnalyseAsync(EvaluatorRequest request);
        Task<Evaluation> BuildEvaluationAsync(EvaluatorRequest request, Resource filterResouce = null);
        
    }
}
