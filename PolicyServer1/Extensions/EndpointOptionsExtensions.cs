using System;
using System.Collections.Generic;
using System.Text;
using PolicyServer1.Configuration.Options;
using PolicyServer1.Hosting;
using static PolicyServer1.Constants;

namespace PolicyServer1.Extensions {
    public static class EndpointOptionsExtensions {

        public static Boolean IsEndpointEnabled(this EndpointsOptions options, Endpoint endpoint) {
            switch (endpoint?.Name) {
                //case EndpointNames.Policy:
                //    return options.EnablePolicyEndpoint;
                case EndpointNames.Permission:
                    return options.EnablePermissionEndpoint;
                default:
                    return true;
            }
        }

    }
}
