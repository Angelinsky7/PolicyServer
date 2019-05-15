using PolicyServer1.Configuration.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PolicyServer1.Configuration {
    public class PolicyServerOptions {

        public CachingOptions Caching { get; set; } = new CachingOptions();
        public EndpointsOptions Endpoints { get; set; } = new EndpointsOptions();
        public DiscoveryOptions Discovery { get; set; } = new DiscoveryOptions();

        public String ClientIdentifier { get; set; } = "client_id";
        public String IssuerUri { get; set; }
        public String PublicOrigin { get; set; }

    }
}
