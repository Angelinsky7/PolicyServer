using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestPolicyServer.Quickstart.ViewComponents {
    public class FlashMessageErrorViewComponent : ViewComponent {

        public Task<IViewComponentResult> InvokeAsync() {
            return Task.FromResult<IViewComponentResult>(View());
        }

    }

    public class FlashMessageErrorInfoComponent : ViewComponent {

        public Task<IViewComponentResult> InvokeAsync() {
            return Task.FromResult<IViewComponentResult>(View());
        }

    }

}
