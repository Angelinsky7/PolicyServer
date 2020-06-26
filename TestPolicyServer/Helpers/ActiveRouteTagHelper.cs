using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TestPolicyServer.Helpers {
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class ActiveRouteTagHelper : TagHelper {
        private IDictionary<String, String> _routeValues;

        /// <summary>The name of the action method.</summary>
        /// <remarks>Must be <c>null</c> if <see cref="P:Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper.Route" /> is non-<c>null</c>.</remarks>
        [HtmlAttributeName("asp-action")]
        public String Action { get; set; }

        /// <summary>The name of the controller.</summary>
        /// <remarks>Must be <c>null</c> if <see cref="P:Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper.Route" /> is non-<c>null</c>.</remarks>
        [HtmlAttributeName("asp-controller")]
        public String Controller { get; set; }

        /// <summary>Additional parameters for the route.</summary>
        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<String, String> RouteValues {
            get {
                if (_routeValues == null) { _routeValues = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase); }
                return _routeValues;
            }
            set => _routeValues = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.ViewContext" /> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            base.Process(context, output);

            if (ShouldBeActive()) {
                MakeActive(output);
            }

            output.Attributes.RemoveAll("is-active-route");
        }

        private Boolean ShouldBeActive() {
            String currentController = ViewContext.RouteData.Values["Controller"].ToString();
            String currentAction = ViewContext.RouteData.Values["Action"].ToString();

            if (!String.IsNullOrWhiteSpace(Controller) && Controller.ToLower() != currentController.ToLower()) {
                return false;
            }

            if (!String.IsNullOrWhiteSpace(Action) && Action.ToLower() != currentAction.ToLower()) {
                return false;
            }

            foreach (KeyValuePair<String, String> routeValue in RouteValues) {
                if (!ViewContext.RouteData.Values.ContainsKey(routeValue.Key) ||
                    ViewContext.RouteData.Values[routeValue.Key].ToString() != routeValue.Value) {
                    return false;
                }
            }

            return true;
        }

        private void MakeActive(TagHelperOutput output) {
            TagHelperAttribute classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null) {
                classAttr = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttr);
            } else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf("active") < 0) {
                output.Attributes.SetAttribute("class", classAttr.Value == null
                    ? "active"
                    : classAttr.Value.ToString() + " active");
            }
        }
    }

}
