using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Configuration;
using PolicyServer1.Models;

namespace PolicyServer1.Validations.Default {
    public class DefaultPermissionConfigurationValidator : IPermissionConfigurationValidator {

        private readonly PolicyServerOptions _options;

        public DefaultPermissionConfigurationValidator(PolicyServerOptions options) {
            _options = options;
        }

        public Task ValidateAsync(ConfigurationValidationContext<Permission> context) {

            if (context.Item.Id == null) {
                context.SetError($"Id is required");
                return Task.CompletedTask;
            }

            if (context.Item.Name == null) {
                context.SetError($"Name is required");
                return Task.CompletedTask;
            }

            if (context.Item.Policies == null || context.Item.Policies.Count == 0) {
                context.SetError($"At least one policiy in the policies is required");
                return Task.CompletedTask;
            }

            if (context.Item is ScopePermission scopePermission) {
                return ValidateScopePermissionAsync(context, scopePermission);
            } else if (context.Item is ResourcePermission resourcePermission) {
                return ValidateResourcePermissionAsync(context, resourcePermission);
            }

            return Task.CompletedTask;
        }

        protected virtual Task ValidateScopePermissionAsync(ConfigurationValidationContext<Permission> context, ScopePermission scopePermission) {

            if (scopePermission.Scopes == null || scopePermission.Scopes.Count == 0) {
                context.SetError($"At least one scope in the scopes is required");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        protected virtual Task ValidateResourcePermissionAsync(ConfigurationValidationContext<Permission> context, ResourcePermission resourcePermission) {

            if (resourcePermission.Resource == null || resourcePermission.ResouceType == null) {
                context.SetError($"At resource or resouceType is required");
                return Task.CompletedTask;
            }

            if (resourcePermission.Resource != null && resourcePermission.ResouceType != null) {
                context.SetError($"You cannot have resource and resouceType specified");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

    }
}
