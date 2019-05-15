using Microsoft.AspNetCore.Builder;
using PolicyServer1.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder {
    public static class PolicyClientApplicatiobBuilderExtensions {

        public static IApplicationBuilder UsePolicyClient(this IApplicationBuilder app) {

            app.UseMiddleware<PolicyClientMiddleware>();

            return app;
        }

    }
}
