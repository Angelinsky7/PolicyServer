using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace TestPolicyServer.Quickstart.MVC {

    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = "oidc")]
    public class ScopeController : Controller {

        private readonly IScopeStore _scopeStore;

        public ScopeController(IScopeStore scopeStore) => _scopeStore = scopeStore;

        public async Task<IActionResult> Index(ListViewModel viewModel) {
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(viewModel.SortOrder) ? "name_desc" : "";
            ////ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //ViewData["CurrentFilter"] = viewModel.SearchString;

            return View("Index", await BuildViewModelAsync(viewModel));
        }

        public IActionResult Create() {
            Scope item = new Scope();
            return View(item);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("Id,Name,DisplayName,IconUri")] Scope item) {
            //if (id != movie.ID) {
            //    return NotFound();
            //}

            if (ModelState.IsValid) {
                try {
                    Guid guid = await _scopeStore.CreateAsync(item);
                } catch (DbUpdateConcurrencyException) {
                    //if (!(await ScopeExistsAsync(guid))) {
                    //    return NotFound();
                    //} else {
                    throw;
                    //}
                }
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(Guid id) {
            if (id == null) { return NotFound(); }
            Scope item = await _scopeStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id, [Bind("Id,Name,DisplayName,IconUri")] Scope item) {
            if (id != item.Id) { return NotFound(); }

            if (ModelState.IsValid) {
                try {
                    await _scopeStore.UpdateAsync(id, item);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await ScopeExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression d'un Scope", OkButton = "Delete")]
        public async Task<IActionResult> Delete(Guid id) {
            if (id == null) { return NotFound(); }
            Scope item = await _scopeStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (id == null) { return BadRequest(); }

            if (ModelState.IsValid) {
                try {
                    await _scopeStore.RemoveAsync(id);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await ScopeExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            Scope item = await _scopeStore.GetAsync(id);
            return View(item);
        }

        private async Task<ScopesViewModel> BuildViewModelAsync(ListViewModel viewModel) {
            IQueryable<Scope> query = _scopeStore.Query();

            switch (viewModel.SortOrder) {
                case "name_desc":
                    query = query.OrderByDescending(p => p.Name);
                    break;
            }

            if (!String.IsNullOrEmpty(viewModel.SearchString)) {
                String upperSearchString = viewModel.SearchString.ToUpper();
                query = query.Where(p => p.Name.ToUpper().Contains(upperSearchString) || p.DisplayName.ToUpper().Contains(upperSearchString));
            }

            List<Scope> scopes = await query.AsNoTracking().ToListAsync();

            List<Scope> list = new List<Scope>();
            foreach (Scope scope in scopes) {
                list.Add(scope);
            }

            return new ScopesViewModel {
                Count = list.Count,
                CurrentPage = 0,
                PageSize = 10,
                SortOrder = viewModel.SortOrder,
                SearchString = viewModel.SearchString,
                Scopes = list
            };
        }

        private async Task<Boolean> ScopeExistsAsync(Guid id) => (await _scopeStore.GetAsync(id)) != null;

    }
}
