using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using PolicyServer1.Models;

namespace PolicyServer1.ResponseHandling.Models {
    public class PermissionRequest : BaseRequest {

        public String ClientId { get; set; }
        public ClaimsPrincipal User { get; set; }

    }
}
