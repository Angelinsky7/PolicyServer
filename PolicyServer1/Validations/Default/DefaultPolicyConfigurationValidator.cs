using PolicyServer1.Configuration;
using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Validations.Default {
    public class DefaultPolicyConfigurationValidator : IPolicyConfigurationValidator {

        private readonly PolicyServerOptions _options;

        public DefaultPolicyConfigurationValidator(PolicyServerOptions options) {
            _options = options;
        }

        public async Task ValidateAsync(ConfigurationValidationContext<Policy> context) {
            //await ValidateRoleAsync(context);
            //if (!context.IsValid) { return; }

            await ValidateCycliqueRoleAsync(context);
            if (!context.IsValid) { return; }
        }

        protected virtual Task ValidateRoleAsync(ConfigurationValidationContext<Policy> context) {
            //TODO(demarco): Correct validation please
            //if (context.Item.Name.Count == 0) {
            //    context.SetError($"At least on Role is required, but no role is configured.");
            //    return Task.CompletedTask;
            //}
            return Task.CompletedTask;


        }

        protected virtual Task ValidateCycliqueRoleAsync(ConfigurationValidationContext<Policy> context) {
            //TODO(demarco): Correct validation please
            //foreach (Role role in context.Item.Roles) {
            //    if (!CheckCircularReference(role)) {
            //        context.SetError($"Circular reference detected. Please avoid circular references.");
            //        return Task.CompletedTask;
            //    }
            //}
            return Task.CompletedTask;
        }

        protected virtual Boolean CheckCircularReference(Role role, List<String> visitedRole = null) {
            if (visitedRole == null) { visitedRole = new List<String>(); }

            if (visitedRole.Contains(role.Name)) {
                return false;
            }
            visitedRole.Add(role.Name);
            foreach (Role parent in role.Parents) {
                if (!CheckCircularReference(parent, visitedRole)) {
                    return false;
                }
            }
            return true;
        }

    }
}
