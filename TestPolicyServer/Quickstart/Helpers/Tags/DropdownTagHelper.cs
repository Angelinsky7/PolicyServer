using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TestPolicyServer.Quickstart {

    [HtmlTargetElement("dropdown")]
    public class DropdownTagHelper : TagHelper {

        private readonly IHtmlHelper _htmlHelper;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public DropdownTagHelper(IHtmlHelper htmlHelper) => _htmlHelper = htmlHelper;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            (_htmlHelper as IViewContextAware).Contextualize(ViewContext);
            IHtmlContent content = await _htmlHelper.PartialAsync("~/Views/Shared/Components/Dropdown/Default.cshtml");
            output.Content.SetHtmlContent(content);
        }
    }
}
