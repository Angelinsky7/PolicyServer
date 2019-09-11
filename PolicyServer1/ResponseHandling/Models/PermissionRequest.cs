using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using PolicyServer1.Infrastructure;
using PolicyServer1.Models;

namespace PolicyServer1.ResponseHandling.Models {
    public class PermissionRequest : BaseRequest {

        public String ClientId { get; set; }
        public String AudienceId { get; set; }

        public ClaimsPrincipal User { get; set; }
        public PermissionRequestReponseMode ResponseMode { get; set; }
        public IEnumerable<String> Permissions { get; set; } = new HashSet<String>();

    }

    public enum PermissionRequestReponseMode {
        [EnumStringValue("decision")]
        Decision,
        [EnumStringValue("permissions")]
        Permissions,
        [EnumStringValue("analyse")]
        Analyse
    }

}
