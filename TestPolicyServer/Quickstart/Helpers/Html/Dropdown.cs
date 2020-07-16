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

        public static async Task<IHtmlContent> DropdownAsync(this IHtmlHelper helper, Func<dynamic, IHtmlContent> templateButton, Func<Int32, IHtmlContent> templateContent) {

            //return new HtmlString(tagBuilder.GetString());

            //var listHtml = new HtmlContentBuilder();
            //listHtml.AppendHtml("<ol><li>");
            //listHtml.AppendHtml(helper.ActionLink("foo", "bar", "example"));
            //listHtml.AppendHtml("</li></ol>");
            //return listHtml;

            IHtmlContent content = await helper.PartialAsync("~/Views/Shared/Components/Dropdown/DefaultHelper.cshtml", new DropdownModel {
                ButtonTemplate = templateButton(null).GetString(),
                ContentTemplate = templateContent(1).GetString()
            });
            return content;

            //var result = new HtmlContentBuilder();
            //result.AppendHtml("<ul>");
            //result.AppendHtml(new TagBuilder("div").InnerHtml.SetHtmlContent(template(null)));

            //for (Int32 i = 0; i < 4; ++i) {
            //    TagBuilder tagBuilder = new TagBuilder("span");
            //    tagBuilder.AddCssClass($"test_{i}");
            //    tagBuilder.InnerHtml.SetHtmlContent(template2(i));
            //    result.AppendHtml(tagBuilder);
            //}
            //result.AppendHtml("</ul>");

            //return result;
        }

    }
}
