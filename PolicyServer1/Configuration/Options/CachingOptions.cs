using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Configuration {
    public class CachingOptions {

        private static readonly TimeSpan Default = TimeSpan.FromMinutes(15);

        public TimeSpan ClientStoreExpiration { get; set; } = Default;
        public TimeSpan PolicyStoreExpiration { get; set; } = Default;
        public TimeSpan PermissionStoreExpiration { get; set; } = Default;
        public TimeSpan RoleStoreExpiration { get; set; } = Default;
        public TimeSpan UserPolicyServiceExpiration { get; set; } = Default;
        public TimeSpan ScopeStoreExpiration { get; set; } = Default;
        public TimeSpan ResourceStoreExpiration { get; set; } = Default;


    }
}
