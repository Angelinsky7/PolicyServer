using System;

namespace PolicyServer1.Configuration {
    public class DiscoveryOptions {
        public Boolean ShowEndpoints { get; set; } = true;
        public Nullable<Int32> ResponseCacheInterval { get; set; } = null;
    }
}