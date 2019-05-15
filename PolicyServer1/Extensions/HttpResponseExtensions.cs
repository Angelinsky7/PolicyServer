using Microsoft.AspNetCore.Http;
using PolicyServer1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Extensions {
    public static class HttpResponseExtensions {

        public static void SetNoCache(this HttpResponse response) {
            if (!response.Headers.ContainsKey("Cache-Control")) {
                response.Headers.Add("Cache-Control", "no-store, no-cache, max-age=0");
            } else {
                response.Headers["Cache-Control"] = "no-store, no-cache, max-age=0";
            }

            if (!response.Headers.ContainsKey("Pragma")) {
                response.Headers.Add("Pragma", "no-cache");
            }
        }

        public static void SetNoCache(this HttpResponse response, Int32 maxAge) {
            if (maxAge == 0) {
                response.SetNoCache();
            } else if (maxAge > 0) {
                if (!response.Headers.ContainsKey("Cache-Control")) {
                    response.Headers.Add("Cache-Control", $"max-age={maxAge}");
                }
            }
        }

        public static async Task WriteHtmlAsync(this HttpResponse response, String html) {
            response.ContentType = "text/html; charset=UTF-8";
            await response.WriteAsync(html, Encoding.UTF8);
        }

        public static async Task WriteJsonAsync(this HttpResponse response, Object o, String contentType = null) {
            String json = ObjectSerializer.ToString(o);
            await response.WriteJsonAsync(json, contentType);
        }

        public static async Task WriteJsonAsync(this HttpResponse response, String json, String contentType = null) {
            response.ContentType = contentType ?? "application/json; charset=UTF-8";
            await response.WriteAsync(json);
        }

    }
}
