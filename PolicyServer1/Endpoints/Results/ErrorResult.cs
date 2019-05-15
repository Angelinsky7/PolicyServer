using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PolicyServer1.Extensions;
using PolicyServer1.Hosting;
using PolicyServer1.Infrastructure;

namespace PolicyServer1.Endpoints.Results {
    internal class ErrorResult : IEndpointResult {

        public String _error;
        public String _errorDescription;

        public ErrorResult(String error, String errorDescription = null) {
            _error = error;
            _errorDescription = errorDescription;
        }

        public async Task ExecuteAsync(HttpContext context) {
            context.Response.StatusCode = 400;
            context.Response.SetNoCache();

            ResultDto dto = new ResultDto {
                Error = _error,
                Error_description = _errorDescription
            };

            JObject jobject = ObjectSerializer.ToJObject(dto);
            await context.Response.WriteJsonAsync(jobject);
        }

        internal class ResultDto {
            public String Error { get; set; }
            public String Error_description { get; set; }
        }

    }
}
