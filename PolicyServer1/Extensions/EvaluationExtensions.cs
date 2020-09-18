using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolicyServer1.Models;

namespace PolicyServer1.Extensions {
    public static class EvaluationExtensions {

        public static void RemoveRessourceWithoutScope(this Evaluation evaluation) {
            foreach (EvaluationItem item in evaluation.Results.ToList()) {
                if (item.Scopes == null || !item.Scopes.Any()) {
                    evaluation.Results.Remove(item);
                }
            }
        }

        public static IEnumerable<EvaluationItem> OnlyWithScopes(this ICollection<EvaluationItem> src) => src.Where(p => p.Scopes != null && p.Scopes.Any());

    }
}
