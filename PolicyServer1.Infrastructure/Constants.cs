using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1 {
    internal static class Constants {

        public const String PolicyServerName = "PolicyServer1";
        public const String PolicyServerAuthentificationType = PolicyServerName;
        public const String DefaultHashAlgorithm = "SHA256";

        public static class Policy {
            public const String Role = "role";
            public const String Permission = "permission";
            public const String Identity = PolicyServerName + "Middleware";
        }

        public static class EndpointNames {
            public const String Discovery = "Discovery";
            //public const String Policy = "Policy";
            public const String Permission = "Permission";
        }

        public static class ProtocolRoutePaths {
            public const String PathPrefix = "connect";

            public const String DiscoveryConfiguration = ".well-known/policy-configuration";
            //public const String Policy = PathPrefix + "/policy";
            public const String Permission = PathPrefix + "/permission";

            public static readonly String[] CorsPaths = {
                DiscoveryConfiguration,
                //Policy,
                Permission
            };
        }

        public static class EnvironmentKeys {
            public const String PolicyServerBasePath = "polsvr:PolicyServerBasePath";
        }

    }
}
