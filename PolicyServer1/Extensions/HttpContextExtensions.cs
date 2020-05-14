using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PolicyServer1.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolicyServer1.Extensions {
    public static class HttpContextExtensions {

        public static String GetPolicyServerBaseUrl(this HttpContext context) {
            return context.GetPolicyServerHost() + context.GetPolicyServerBasePath();
        }

        public static String GetPolicyServerHost(this HttpContext context) {
            HttpRequest request = context.Request;
            return request.Scheme + "://" + request.Host.ToUriComponent();
        }

        public static String GetPolicyServerBasePath(this HttpContext context) {
            return context.Items[Constants.EnvironmentKeys.PolicyServerBasePath] as String;
        }

        public static void SetPolicyServerBasePath(this HttpContext context, String value) {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.Items[Constants.EnvironmentKeys.PolicyServerBasePath] = value;
        }

        public static String GetPolicyServerOrigin(this HttpContext context) {
            HttpRequest request = context.Request;
            return request.Scheme + "://" + request.Host.Value;
        }

        public static void SetPolicyServerOrigin(this HttpContext context, String value) {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (value == null) throw new ArgumentNullException(nameof(value));

            String[] split = value.Split(new[] { "://" }, StringSplitOptions.RemoveEmptyEntries);

            HttpRequest request = context.Request;
            request.Scheme = split.First();
            request.Host = new HostString(split.Last());
        }

        public static String GetPolicyServerIssuerUri(this HttpContext context) {
            if (context == null) throw new ArgumentNullException(nameof(context));

            PolicyServerOptions options = context.RequestServices.GetRequiredService<PolicyServerOptions>();
            String uri = options.IssuerUri;
            if (uri.IsMissing()) {
                uri = context.GetPolicyServerOrigin() + context.GetPolicyServerBasePath();
                if (uri.EndsWith("/")) uri = uri.Substring(0, uri.Length - 1);
                uri = uri.ToLowerInvariant();
            }

            return uri;
        }

    }
}
