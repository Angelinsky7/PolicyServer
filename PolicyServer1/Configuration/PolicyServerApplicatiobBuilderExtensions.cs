using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using PolicyServer1.Hosting;

namespace Microsoft.AspNetCore.Builder {
    public static class PolicyServerApplicatiobBuilderExtensions {

        public static IApplicationBuilder UsePolicyServer(this IApplicationBuilder app) {

            app.UseMiddleware<BaseUrlMiddleware>();

            app.UseMiddleware<PolicyServerMiddleware>();
           
            return app;
        }

    }
}
