using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestPolicyServer.Quickstart.ViewComponents {
    public class PaginationViewComponent : ViewComponent {

        public Task<IViewComponentResult> InvokeAsync(ListViewModel model) {
            return Task.FromResult<IViewComponentResult>(View(model));
        }

    }

}
