using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestPolicyServer.Quickstart {

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ViewLayoutModalAttribute : ActionFilterAttribute {

        public String Title { get; set; }
        public String OkButton { get; set; }
        public String CancelButton { get; set; }
        public String LayoutName { get; }

        public ViewLayoutModalAttribute(String layoutName, String title = "Dialog", String okButton = "Ok", String cancelButton = "Cancel") {
            LayoutName = layoutName;
            Title = title;
            OkButton = okButton;
            CancelButton = cancelButton;
        }

        public override void OnActionExecuted(ActionExecutedContext context) {
            if (context.Controller is Controller controller) {
                if (controller.Request.Query.ContainsKey("modal") && Boolean.Parse(controller.Request.Query["modal"])) {
                    controller.ViewData["ModalTitle"] = Title;
                    controller.ViewData["OkButton"] = OkButton;
                    controller.ViewData["CancelButton"] = CancelButton;
                    controller.ViewData["Layout"] = LayoutName;
                }
            }
        }
    }

}
