using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PolicyServer1.Hosting;

namespace PolicyServer1.Endpoints.Results {
    internal class StatusCodeResult : IEndpointResult {

        public Int32 StatusCode { get; }

        public StatusCodeResult(HttpStatusCode statusCode) {
            StatusCode = (Int32)statusCode;
        }

        public StatusCodeResult(Int32 statusCode) {
            StatusCode = statusCode;
        }

        public Task ExecuteAsync(HttpContext context) {
            context.Response.StatusCode = StatusCode;
            return Task.CompletedTask;
        }
    }
}
