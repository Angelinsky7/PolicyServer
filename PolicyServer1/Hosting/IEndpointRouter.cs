using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PolicyServer1.Hosting {
    public interface IEndpointRouter {
        IEndpointHandler Find(HttpContext context);
    }
}
