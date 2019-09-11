using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Configuration;
using PolicyServer1.Models;

namespace PolicyServer1.Validations.Default {
    public class DefaultClientConfigurationValidator : IClientConfigurationValidator {

        private readonly PolicyServerOptions _options;
        private readonly IPolicyConfigurationValidator _policyConfigurationValidator;

        public DefaultClientConfigurationValidator(
            PolicyServerOptions options,
            IPolicyConfigurationValidator policyConfigurationValidator
        ) {
            _options = options;
            _policyConfigurationValidator = policyConfigurationValidator;
        }

        public async Task ValidateAsync(ConfigurationValidationContext<Client> context) {
            //TODO(demarco): Need to reactivate this !!!
            await ValidatePolicyAsync(context);
            if (!context.IsValid) { return; }

            //await ValidateScopeAsync(context);
            //if (!context.IsValid) { return; }

            //await ValidateSecretsAsync(context);
            //if (!context.IsValid) { return; }
        }

        protected virtual Task ValidatePolicyAsync(ConfigurationValidationContext<Client> context) {
            //TODO(demarco): Rework this part please !
            
            //if (context.Item.Policy == null) {
            //    context.SetError($"Policy is required, but no policy is configured.");
            //    return Task.CompletedTask;
            //}
            //ConfigurationValidationContext<Policy> policyContext = new ConfigurationValidationContext<Policy>(context.Item.Policy);
            //_policyConfigurationValidator.ValidateAsync(policyContext);

            //if (!policyContext.IsValid) { context.SetError(policyContext.ErrorMessage); }
            return Task.CompletedTask;
        }

        //protected virtual Task ValidateScopeAsync(ConfigurationValidationContext<Client> context) {
        //    if (context.Item.AllowedScopes.Count == 0) {
        //        context.SetError($"Client allowed scope is required, but no scope is configured.");
        //        return Task.CompletedTask;
        //    }
        //    return Task.CompletedTask;
        //}

        //protected virtual Task ValidateSecretsAsync(ConfigurationValidationContext<Client> context) {
        //    if (context.Item.RequireClientSecret && context.Item.Secrets.Count == 0) {
        //        context.SetError($"Client secret is required, but no client secret is configured.");
        //        return Task.CompletedTask;
        //    }

        //    return Task.CompletedTask;
        //}

    }
}
