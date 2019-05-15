using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PolicyServer1.ResponseHandling.Models;

namespace PolicyServer1.ResponseHandling {
    public interface IPermissionRequestParser {

        Task<PermissionRequest> ParseAsync(HttpContext context);

    }
}
