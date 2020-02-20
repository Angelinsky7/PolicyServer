using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using Newtonsoft.Json.Linq;

namespace MvcClient.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Private() {
            String accessToken = await HttpContext.GetTokenAsync("access_token");
            ViewData["token"] = accessToken;

            return View();
        }

        [Authorize]
        public async Task<IActionResult> TestPolicy() {
            String accessToken = await HttpContext.GetTokenAsync("access_token");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            String content = await client.GetStringAsync("http://localhost:5001/connect/permission?clientId=mvc");

            ViewBag.Json = Newtonsoft.Json.JsonConvert.DeserializeObject(content).ToString();
            return View("Json");
        }

        [Authorize("CanDoDoctorThings")]
        public IActionResult PrivateForDoctors() {
            return View();
        }

        [Authorize(Roles = "TestRole1")]
        public IActionResult PrivateForRole() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
