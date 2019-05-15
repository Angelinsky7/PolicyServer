using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Configuration.Options {
    public class EndpointsOptions {

        public Boolean EnableDiscoveryEndpoint { get; set; } = true;
        public Boolean EnablePolicyEndpoint { get; set; } = true;
        public Boolean EnablePermissionEndpoint { get; set; } = true;

    }
}
