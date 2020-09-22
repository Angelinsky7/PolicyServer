using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace TestPolicyServer.Quickstart.ModelBinders {

    public class PolicyModelBinder : IModelBinder {

        private readonly IPolicyStore _policyStore;

        public PolicyModelBinder(IPolicyStore policyStore) => _policyStore = policyStore;

        public async Task BindModelAsync(ModelBindingContext bindingContext) {
            if (bindingContext == null) { throw new ArgumentNullException(nameof(bindingContext)); }
            String modelName = bindingContext.ModelName;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None) { return; }
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            String value = valueProviderResult.FirstValue;
            if (String.IsNullOrEmpty(value)) { return; }
            if (!Guid.TryParse(value, out Guid guid)) { return; }

            Policy model = await _policyStore.GetAsync(guid);
            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }

}
