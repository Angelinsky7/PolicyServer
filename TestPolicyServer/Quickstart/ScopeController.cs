using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace TestPolicyServer.Quickstart {

    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = "oidc")]
    public class ScopeController : Controller {

        private readonly IScopeStore _scopeStore;

        public ScopeController(IScopeStore scopeStore) => _scopeStore = scopeStore;

        [HttpGet]
        public async Task<IActionResult> Index() => View("Index", await BuildViewModelAsync());

        private async Task<ScopesViewModel> BuildViewModelAsync() {
            List<Scope> scopes = _scopeStore.Query().ToList();

            List<Scope> list = new List<Scope>();
            foreach (Scope scope in scopes) {
                list.Add(scope);
            }

            return new ScopesViewModel {
                Scopes = list
            };
        }

    }
}
