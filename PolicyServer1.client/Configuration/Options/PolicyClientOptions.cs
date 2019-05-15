using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Client.Configuration.Options {
    public class PolicyClientOptions {
        public CachingOptions Caching { get; set; } = new CachingOptions();

        public Boolean UseCurrentAuthentification { get; set; } = true;

        public String Authority { get; set; }
        public Boolean RequireHttpsMetadata { get; set; }
        public String ClientId { get; set; }
        //public String ClientSecret { get; set; }
        public Boolean SaveTokens { get; set; }

        public String ClientIdentifier { get; set; } = "client_id";
        public String SubIdentifier { get; set; } = "sub";
    }
}
