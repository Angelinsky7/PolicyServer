using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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

        public async Task<IActionResult> Index(ListViewModel viewModel) => View("Index", await BuildViewModelAsync(viewModel));

        public IActionResult Create() {
            Scope item = new Scope();
            return View(item);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePostAsync([Bind("Id,Name,DisplayName,IconUri")] Scope item) {
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

        public async Task<IActionResult> EditAsync(Guid id) {
            if (id == null) { return NotFound(); }
            Scope item = await _scopeStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPostAsync(Guid id, [Bind("Id,Name,DisplayName,IconUri")] Scope item) {
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
        public async Task<IActionResult> DeleteAsync(Guid id) {
            if (id == null) { return NotFound(); }
            Scope item = await _scopeStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(Guid id) {
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
                } catch (DbUpdateException ex) {
                    String error = CheckHandleError(ex);
                    if (error != null) {
                        //TODO(demarco): I want to show a flash message wgen comming back from this showing the error message
                        return BadRequest(error);
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            Scope item = await _scopeStore.GetAsync(id);
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression de plusieurs Scopes", OkButton = "Delete")]
        public async Task<IActionResult> DeleteMultipleAsync([FromQuery] Guid[] guid) {
            if (guid == null) { return NotFound(); }
            List<Scope> items = new List<Scope>();
            foreach (Guid id in guid) {
                Scope item = await _scopeStore.GetAsync(id);
                if (item == null) { return NotFound(); }
                items.Add(item);
            }
            return View("DeleteMultiple", items.ToArray());
        }

        [HttpPost, ActionName("DeleteMultiple")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMultipleConfirmedAsync([FromForm] Guid[] guid) {
            if (guid == null) { return BadRequest(); }

            if (ModelState.IsValid) {
                try {
                    foreach (Guid id in guid) {
                        await _scopeStore.RemoveAsync(id);
                    }
                } catch (DbUpdateConcurrencyException) {
                    foreach (Guid id in guid) {
                        if (!(await ScopeExistsAsync(id))) {
                            return NotFound();
                        } else {
                            throw;
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            {
                List<Scope> items = new List<Scope>();
                foreach (Guid id in guid) {
                    Scope item = await _scopeStore.GetAsync(id);
                    if (item == null) { return NotFound(); }
                    items.Add(item);
                }
                return View(items.ToArray());
            }
        }

        private async Task<ScopesViewModel> BuildViewModelAsync(ListViewModel viewModel) {
            IQueryable<Scope> query = _scopeStore.Query();

            switch (viewModel.SortOrder) {
                case "name_desc":
                    query = query.OrderByDescending(p => p.Name);
                    break;
                case "name_asc":
                    query = query.OrderBy(p => p.Name);
                    break;
            }

            if (!String.IsNullOrEmpty(viewModel.SearchString)) {
                String upperSearchString = viewModel.SearchString.ToUpper();
                query = query.Where(p => p.Name.ToUpper().Contains(upperSearchString) || p.DisplayName.ToUpper().Contains(upperSearchString));
            }

            Int32 countTotal = query.Count();

            Int32 itemsToSkip = (viewModel.CurrentPage - 1) * viewModel.PageSize;
            query = query.Skip(itemsToSkip).Take(viewModel.PageSize);

            List<Scope> scopes = await query.AsNoTracking().ToListAsync();

            List<Scope> list = new List<Scope>();
            foreach (Scope scope in scopes) {
                list.Add(scope);
            }

            return new ScopesViewModel {
                Count = list.Count,
                TotalItems = countTotal,
                CurrentPage = viewModel.CurrentPage,
                PageSize = viewModel.PageSize,
                SortOrder = viewModel.SortOrder,
                SearchString = viewModel.SearchString,
                Scopes = list
            };
        }

        private async Task<Boolean> ScopeExistsAsync(Guid id) => (await _scopeStore.GetAsync(id)) != null;

        private String CheckHandleError(Exception ex) {
            //TODO(demarco): We want to generalise this behavior.... autoload injection and check automatically (maybe all activated first then disable some)
            DbUpdateException dbUpdateEx = ex as DbUpdateException;
            SqlException sqlEx = dbUpdateEx?.InnerException as SqlException;
            if (sqlEx != null) {
                if (sqlEx.Number == SqlErrorDeleteReferenceConstraint.SqlServerErrorNumber) {

                    String valError = SqlErrorDeleteReferenceConstraint.Formatter(sqlEx, dbUpdateEx.Entries);
                    if (valError != null) {
                        //TODO(demarco): I want to show a flash message wgen comming back from this showing the error message
                        ModelState.AddModelError("refDelete", valError);
                        return valError;
                    }
                    //else check for other SQL errors
                }
            }
            return null;
        }
    }
}
