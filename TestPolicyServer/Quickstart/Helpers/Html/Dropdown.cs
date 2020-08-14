using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestPolicyServer.Quickstart {
    public static class DropdownHelpers {

        public static async Task<IHtmlContent> DropdownAsync(this IHtmlHelper helper, Func<dynamic, IHtmlContent> templateButton, Func<Int32, IHtmlContent> templateContent, String buttonClass = "hover:bg-blue-600") {
            IHtmlContent content = await helper.PartialAsync("~/Views/Shared/Components/Dropdown/DefaultHelper.cshtml", new DropdownModel {
                ButtonClass = buttonClass,
                ButtonTemplate = templateButton(null).GetString(),
                ContentTemplate = templateContent(1).GetString()
            });
            return content;
        }

    }
}
