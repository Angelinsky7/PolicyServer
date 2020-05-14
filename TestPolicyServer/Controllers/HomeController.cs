using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestPolicyServer.Controllers {


    [Authorize(AuthenticationSchemes = "oidc")]
    public class HomeController : Controller {

        [AllowAnonymous]
        public IActionResult Index() {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task Login(String returnUrl = "/") {
            await HttpContext.ChallengeAsync("oidc", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Logout() {
            return SignOut("Cookies", "oidc");
        }

    }
}