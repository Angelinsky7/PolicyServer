using PolicyServer1.Configuration;
using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Validations.Default {
    public class DefaultRoleConfigurationValidator : IRoleConfigurationValidator {

        private readonly PolicyServerOptions _options;

        public DefaultRoleConfigurationValidator(PolicyServerOptions options) {
            _options = options;
        }

        public async Task ValidateAsync(ConfigurationValidationContext<Role> context) {
            //await ValidateRoleAsync(context);
            //if (!context.IsValid) { return; }

            await ValidateCycliqueRoleAsync(context);
            if (!context.IsValid) { return; }
        }

        protected virtual Task ValidateCycliqueRoleAsync(ConfigurationValidationContext<Role> context) {
            //TODO(demarco): Correct validation please
            //foreach (Role role in context.Item.Roles) {
            //    if (!CheckCircularReference(role)) {
            //        context.SetError($"Circular reference detected. Please avoid circular references.");
            //        return Task.CompletedTask;
            //    }
            //}
            return Task.CompletedTask;
        }

    }
}
