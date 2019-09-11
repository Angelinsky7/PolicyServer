using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PolicyServer1.Models {
    public interface IEvaluatorRequest {

        Client Client { get; }
        ClaimsPrincipal User { get; }

    }
}
