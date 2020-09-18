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
    public class RoleController : Controller {

        private readonly IRoleStore _roleStore;

        public RoleController(IRoleStore roleStore) => _roleStore = roleStore;

        public async Task<IActionResult> Index(ListViewModel viewModel) => View("Index", await BuildViewModelAsync(viewModel));

        public IActionResult Create() {
            Role item = new Role();
            return View(item);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePostAsync([Bind("Id,Name,Description,Parents")] Role item) {
            //if (id != movie.ID) {
            //    return NotFound();
            //}

            if (ModelState.IsValid) {
                try {
                    Guid guid = await _roleStore.CreateAsync(item);
                } catch (DbUpdateConcurrencyException) {
                    //if (!(await RoleExistsAsync(guid))) {
                    //    return NotFound();
                    //} else {
                    throw;
                    //}
                } catch (DbUpdateException ex) {
                    String error = CheckHandleError(ex);
                    if (error != null) {
                        ModelState.AddModelError("flash.error", error);
                        return View(item);
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> EditAsync(Guid id) {
            if (id == null) { return NotFound(); }
            Role item = await _roleStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPostAsync(Guid id, [Bind("Id,Name,Description,Parents")] Role item) {
            if (id != item.Id) { return NotFound(); }

            if (ModelState.IsValid) {
                try {
                    await _roleStore.UpdateAsync(id, item);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await RoleExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                } catch (DbUpdateException ex) {
                    String error = CheckHandleError(ex);
                    if (error != null) {
                        ModelState.AddModelError("flash.error", error);
                        return View(item);
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression d'un Role", OkButton = "Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id) {
            if (id == null) { return NotFound(); }
            Role item = await _roleStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(Guid id) {
            if (id == null) { return BadRequest(); }

            if (ModelState.IsValid) {
                try {
                    await _roleStore.RemoveAsync(id);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await RoleExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                } catch (DbUpdateException ex) {
                    String error = CheckHandleError(ex);
                    if (error != null) {
                        ModelState.AddModelError("flash.error", error);
                        TempData["flash.error"] = error;
                        return RedirectToAction("index");
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            Role item = await _roleStore.GetAsync(id);
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression de plusieurs Roles", OkButton = "Delete")]
        public async Task<IActionResult> DeleteMultipleAsync([FromQuery] Guid[] guid) {
            if (guid == null) { return NotFound(); }
            List<Role> items = new List<Role>();
            foreach (Guid id in guid) {
                Role item = await _roleStore.GetAsync(id);
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
                        await _roleStore.RemoveAsync(id);
                    }
                } catch (DbUpdateConcurrencyException) {
                    foreach (Guid id in guid) {
                        if (!(await RoleExistsAsync(id))) {
                            return NotFound();
                        } else {
                            throw;
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            {
                List<Role> items = new List<Role>();
                foreach (Guid id in guid) {
                    Role item = await _roleStore.GetAsync(id);
                    if (item == null) { return NotFound(); }
                    items.Add(item);
                }
                return View(items.ToArray());
            }
        }

        private async Task<RolesViewModel> BuildViewModelAsync(ListViewModel viewModel) {
            IQueryable<Role> query = _roleStore.Query();

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
                query = query.Where(p => p.Name.ToUpper().Contains(upperSearchString) || p.Description.ToUpper().Contains(upperSearchString));
            }

            Int32 countTotal = query.Count();

            Int32 itemsToSkip = (viewModel.CurrentPage - 1) * viewModel.PageSize;
            //TODO(ddemarco): I really don't want to do that here....
            query = query.Skip(itemsToSkip).Take(viewModel.PageSize);
            //var queryBug = query.ToList().Skip(itemsToSkip).Take(viewModel.PageSize);

            List<Role> roles = query.ToList();
            //List<Role> roles = queryBug.ToList();

            List<Role> list = new List<Role>();
            foreach (Role role in roles) {
                list.Add(role);
            }

            return new RolesViewModel {
                Count = list.Count,
                TotalItems = countTotal,
                CurrentPage = viewModel.CurrentPage,
                PageSize = viewModel.PageSize,
                SortOrder = viewModel.SortOrder,
                SearchString = viewModel.SearchString,
                Roles = list
            };
        }

        private async Task<Boolean> RoleExistsAsync(Guid id) => (await _roleStore.GetAsync(id)) != null;

        private String CheckHandleError(Exception ex) {
            return SqlErrorManager.Instance.FormatError(ex);
        }
    }
}
