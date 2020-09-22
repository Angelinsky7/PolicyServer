﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PolicyServer1.Models;
using TestPolicyServer.Extensions;
using TestPolicyServer.Quickstart.ModelBinders;

namespace TestPolicyServer.Quickstart.ModelBinderProviders {
    public class PolicyModelBinderProvider : IModelBinderProvider {
        public IModelBinder GetBinder(ModelBinderProviderContext context) {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }
            if (context.CheckModelBinderValidForContext<Policy>()) {
                return new BinderTypeModelBinder(typeof(PolicyModelBinder));
            }
            return null;
        }
    }
}
