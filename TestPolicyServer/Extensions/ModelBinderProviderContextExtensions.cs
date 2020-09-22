using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TestPolicyServer.Extensions {
    public static class ModelBinderProviderContextExtensions {

        public static Boolean CheckModelBinderValidForContext<T>(this ModelBinderProviderContext context) {
            return context.Metadata.ModelType == typeof(T) && context.Metadata.Name != "item" && context.Metadata.PropertyName != "item";
        }

    }
}
