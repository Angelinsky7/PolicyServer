using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PolicyServer1.Hosting {
    public interface IEndpointHandler {

        Task<IEndpointResult> ProcessAsync(HttpContext context);

    }
}
