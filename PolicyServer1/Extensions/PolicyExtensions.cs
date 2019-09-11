using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Models {

    public static class PolicyExtensions {

        //private const String SEP = "|||";

        //public static void CalculateHash(this Policy policy) {
        //    StringBuilder sb = new StringBuilder();

        //    //TODO(demarco): Rework this part please !
        //    //sb.Append($"{policy.LastPolicyChangeDate}{SEP}");

        //    //foreach (Role role in policy.Roles) {
        //    //    AddRole(sb, role);
        //    //}

        //    //policy.Hash = sb.ToString().Sha256();
        //}

        //private static void AddRole(StringBuilder sb, Role role) {
        //    //TODO(demarco): Rework this part please !

        //    //sb.Append($"{role.Name}{SEP}");
        //    //sb.Append($"{String.Join(SEP, role.IdentityRoles)}{SEP}");
        //    //sb.Append($"{String.Join(SEP, role.Subjects)}{SEP}");

        //    //foreach (Role parent in role.Parents) {
        //    //    AddRole(sb, parent);
        //    //}
        //}

        //private static void AddPermission(StringBuilder sb, Permission permission) {
        //    //TODO(demarco): Rework this part please !
        //    //sb.Append($"{permission.Name}{SEP}{permission.IsRevoked}");
        //}


        public static Task<EvaluatorDecision> EvaluateAsync(this Policy policy, EvaluatorRequest request) {
            policy.EvaluateAsync((IEvaluatorRequest)request);

            return Task.FromResult(EvaluatorDecision.Refuse);
        }


    }
}
