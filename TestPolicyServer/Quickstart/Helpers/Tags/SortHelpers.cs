using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TestPolicyServer.Quickstart {
    public static class SortHelper {

        public static String NextSortOrder(String order) {
            String result = "";
            switch (order ?? String.Empty) {
                case "":
                    result = "name_asc";
                    break;
                case "name_asc":
                    result = "name_desc";
                    break;
            }
            return result;
        }

    }

    [HtmlTargetElement("sort-helper")]
    public class SortTagHelper : TagHelper {

        public String SortOrder { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            if (!String.IsNullOrEmpty(SortOrder)) {
                String content = "";
                switch (SortOrder) {
                    case "name_asc":
                        content = "<use xlink:href=\"#arrow-up\"></use>";
                        break;
                    case "name_desc":
                        content = "<use xlink:href=\"#arrow-down\"></use>";
                        break;
                }
                output.TagName = "svg";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.Add("class", "ml-2 h-3 w-3");
                output.PreContent.SetHtmlContent(content);
            }
        }
    }

}
