using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PolicyServer1.Models;
using TestPolicyServer.Quickstart.ModelBinders;

namespace TestPolicyServer.Quickstart.ModelBinderProviders {
    public class ResourceModelBinderProvider : IModelBinderProvider {
        public IModelBinder GetBinder(ModelBinderProviderContext context) {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }
            if (context.Metadata.ModelType == typeof(Resource) &&
                !String.IsNullOrEmpty(context.Metadata.PropertyName) &&
                context.Metadata.PropertyName != "item") {
                return new BinderTypeModelBinder(typeof(ResourceModelBinder));
            }
            return null;
        }
    }
}
