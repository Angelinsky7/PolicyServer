using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TestPolicyServer.Quickstart.TagHelpers {

    [HtmlTargetElement("view-layout-modal")]
    public class ViewLayoutModalTagHelper : TagHelper {

        public String Layout { get; set; }
        public String Display { get; set; } = "flex";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            var content = await output.GetChildContentAsync();
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("style", $"display: {(String.IsNullOrEmpty(Layout) ? Display : "none")}");
            output.PreContent.SetHtmlContent(content);
        }

    }
}
