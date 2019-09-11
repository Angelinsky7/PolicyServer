using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PolicyServer1.ResponseHandling.Models;

namespace PolicyServer1.ResponseHandling {

    [Obsolete]
    public interface IPolicyRequestParser {

        Task<PolicyRequest> ParseAsync(HttpContext context);

    }
}
