using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace TestPolicyServer.Quickstart {
    public static class IHtmlContentExtensions {
        public static String GetString(this IHtmlContent content) {
            using (StringWriter writer = new System.IO.StringWriter()) {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
