using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestPolicyServer.Quickstart.ModelBinderProviders;

namespace Microsoft.Extensions.DependencyInjection {
    public static class MvcOptionsExtensions {

        public static void AddPolicyServerModelBinders(this MvcOptions options) {
            options.ModelBinderProviders.Insert(0, new ResourceModelBinderProvider());
            options.ModelBinderProviders.Insert(0, new PolicyModelBinderProvider());
            options.ModelBinderProviders.Insert(0, new ScopeModelBinderProvider());
            options.ModelBinderProviders.Insert(0, new RoleModelBinderProvider());
        }

    }
}
