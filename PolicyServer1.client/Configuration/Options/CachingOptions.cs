using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Client.Configuration {
    public class CachingOptions {

        private static readonly TimeSpan Default = TimeSpan.FromMinutes(15);

        public TimeSpan LocalUserPermissionExpiration { get; set; } = Default;
        public TimeSpan ServerUserPermissionExpiration { get; set; } = Default;

    }
}
