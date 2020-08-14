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
    public class PermissionController : Controller {

        private readonly IPermissionStore _permissionStore;

        public PermissionController(IPermissionStore scopeStore) => _permissionStore = scopeStore;

        public async Task<IActionResult> Index(ListViewModel viewModel) {
            return View("Index", await BuildViewModelAsync(viewModel));
        }

        //TODO(demarco): We need to decide how we do this... inheritance questionn.
        //               The problem is that permission is abstract and must stay like that
        //               So we need to either create 2 Create entry point, 2 Controller or to have viewModel to handle all possibilites

        public IActionResult CreateResource() {
            Permission item = new ResourcePermission();
            return View(item);
        }

        public IActionResult CreateScope() {
            Permission item = new ScopePermission();
            return View(item);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePostAsync([Bind("Id,Name,DisplayName,Type,Uris,Scopes,IconUri")] Permission item) {
            //if (id != movie.ID) {
            //    return NotFound();
            //}

            if (ModelState.IsValid) {
                try {
                    Guid guid = await _permissionStore.CreateAsync(item);
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
            Permission item = await _permissionStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPostAsync(Guid id, [Bind("Id,Name,DisplayName,Type,Uris,Scopes,IconUri")] Permission item) {
            if (id != item.Id) { return NotFound(); }

            if (ModelState.IsValid) {
                try {
                    await _permissionStore.UpdateAsync(id, item);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await PermissionExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression d'une Permission", OkButton = "Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id) {
            if (id == null) { return NotFound(); }
            Permission item = await _permissionStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(Guid id) {
            if (id == null) { return BadRequest(); }

            if (ModelState.IsValid) {
                try {
                    await _permissionStore.RemoveAsync(id);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await PermissionExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            Permission item = await _permissionStore.GetAsync(id);
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression de plusieurs Permissions", OkButton = "Delete")]
        public async Task<IActionResult> DeleteMultipleAsync([FromQuery] Guid[] guid) {
            if (guid == null) { return NotFound(); }
            List<Permission> items = new List<Permission>();
            foreach (Guid id in guid) {
                Permission item = await _permissionStore.GetAsync(id);
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
                        await _permissionStore.RemoveAsync(id);
                    }
                } catch (DbUpdateConcurrencyException) {
                    foreach (Guid id in guid) {
                        if (!(await PermissionExistsAsync(id))) {
                            return NotFound();
                        } else {
                            throw;
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            {
                List<Permission> items = new List<Permission>();
                foreach (Guid id in guid) {
                    Permission item = await _permissionStore.GetAsync(id);
                    if (item == null) { return NotFound(); }
                    items.Add(item);
                }
                return View(items.ToArray());
            }
        }

        private async Task<PermissionsViewModel> BuildViewModelAsync(ListViewModel viewModel) {
            IQueryable<Permission> query = _permissionStore.Query();

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
                query = query.Where(p => p.Name.ToUpper().Contains(upperSearchString));
            }

            Int32 countTotal = query.Count();
             
            Int32 itemsToSkip = (viewModel.CurrentPage - 1) * viewModel.PageSize;
            query = query.Skip(itemsToSkip).Take(viewModel.PageSize);

            List<Permission> permissions = await query.ToListAsync();

            List<Permission> list = new List<Permission>();
            foreach (Permission permission in permissions) {
                list.Add(permission);
            }

            return new PermissionsViewModel {
                Count = list.Count,
                TotalItems = countTotal,
                CurrentPage = viewModel.CurrentPage,
                PageSize = viewModel.PageSize,
                SortOrder = viewModel.SortOrder,
                SearchString = viewModel.SearchString,
                Permissions = list
            };
        }

        private async Task<Boolean> PermissionExistsAsync(Guid id) => (await _permissionStore.GetAsync(id)) != null;

    }
}
