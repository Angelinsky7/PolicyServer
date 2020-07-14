using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestPolicyServer.Quickstart {
    public static class DropdownHelpers {

        public static IHtmlContent Dropdown(this IHtmlHelper helper, Func<dynamic, IHtmlContent> template) {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.InnerHtml.Append(template("test").ToString());
            return tagBuilder;
        }

    }
}
