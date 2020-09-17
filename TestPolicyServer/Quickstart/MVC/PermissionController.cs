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
using TestPolicyServer.Extensions;
using TestPolicyServer.Quickstart.ModelBinders;

namespace TestPolicyServer.Quickstart.MVC {

    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = "oidc")]
    public class PermissionController : Controller {

        private readonly IPermissionStore _permissionStore;
        private readonly IResourceStore _resourceStore;
        private readonly IScopeStore _scopeStore;

        public PermissionController(IPermissionStore permissionStore, IResourceStore resourceStore, IScopeStore scopeStore) {
            _permissionStore = permissionStore;
            _resourceStore = resourceStore;
            _scopeStore = scopeStore;
        }

        public async Task<IActionResult> Index(ListViewModel viewModel) => View("Index", await BuildViewModelAsync(viewModel));

        //TODO(demarco): We need to decide how we do this... inheritance questionn.
        //               The problem is that permission is abstract and must stay like that
        //               So we need to either create 2 Create entry point, 2 Controller or to have viewModel to handle all possibilites

        public async Task<IActionResult> CreateResourceAsync() {
            ResourcePermission item = new ResourcePermission();

            ViewBag.Resources = await GetResourcesAsSelectListAsync(item.Resource);
            ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        public async Task<IActionResult> CreateScope() {
            ScopePermission item = new ScopePermission();

            ViewBag.Resources = await GetResourcesAsSelectListAsync(item.Resource);
            //ViewBag.Scopes = await GetScopesAsSelectListAsync(item.Scopes);
            ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        [HttpPost, ActionName("CreateResource")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResourcePostAsync([Bind("Id,Name,Description,DecisionStrategy,Policies,ResouceType,Resource")] ResourcePermission item) {
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

            ViewBag.Resources = await GetResourcesAsSelectListAsync(item.Resource);
            ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        [HttpPost, ActionName("CreateScope")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateScopePostAsync([Bind("Id,Name,Description,DecisionStrategy,Policies,ResouceType,Resource,Scopes")] ScopePermission item) {
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

            ViewBag.Resources = await GetResourcesAsSelectListAsync(item.Resource);
            //ViewBag.Scopes = await GetScopesAsSelectListAsync(item.Scopes);
            ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }



        public async Task<IActionResult> EditResourceAsync(Guid id) {
            if (id == null) { return NotFound(); }
            if (!(await _permissionStore.GetAsync(id) is ResourcePermission item)) { return NotFound(); }

            ViewBag.Resources = await GetResourcesAsSelectListAsync(item.Resource);
            ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        [HttpPost, ActionName("EditResource")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditResourcePostAsync(Guid id, [Bind("Id,Name,Description,DecisionStrategy,Policies,ResouceType,Resource")] ResourcePermission item) {
            if (id != item.Id) { return NotFound(); }

            //item.Resource = item?.Resource?.Id != null ? await _resourceStore.GetAsync(item.Resource.Id) : null;

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

            ViewBag.Resources = await GetResourcesAsSelectListAsync(item.Resource);
            ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }


        public async Task<IActionResult> EditScopeAsync(Guid id) {
            if (id == null) { return NotFound(); }
            if (!(await _permissionStore.GetAsync(id) is ScopePermission item)) { return NotFound(); }

            ViewBag.Resources = await GetResourcesAsSelectListAsync(item.Resource);
            //ViewBag.Scopes = await GetScopesAsSelectListAsync(item.Scopes);
            ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        [HttpPost, ActionName("EditScope")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditScopePostAsync(Guid id, [Bind("Id,Name,Description,DecisionStrategy,Policies,ResouceType,Resource,Scopes")] ScopePermission item) {
            if (id != item.Id) { return NotFound(); }

            //item.Resource = item?.Resource?.Id != null ? await _resourceStore.GetAsync(item.Resource.Id) : null;

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

            ViewBag.Resources = await GetResourcesAsSelectListAsync(item.Resource);
            ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

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

            //TODO(demarco): we should use the ToListAsync();
            //List<Permission> permissions = await query.ToListAsync();
            List<Permission> permissions = query.ToList();

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



        //private async Task<IEnumerable<Resource>> GetResourcesAsync() => await _resourceStore.Query().OrderBy(p => p.DisplayName).ToListAsync();

        private async Task<IEnumerable<Resource>> GetResourcesAsync() {
            IOrderedQueryable<Resource> result = _resourceStore.Query().OrderBy(p => p.DisplayName);
            return result.ToList();
        }

        //private async Task<IEnumerable<Scope>> GetScopesAsync() => await _scopeStore.Query().OrderBy(p => p.DisplayName).ToListAsync();

        private async Task<SelectList> GetResourcesAsSelectListAsync(Resource resource = null) => new SelectList(await GetResourcesAsync(), "Id", "DisplayName", resource?.Id);
        //private async Task<SelectList> GetScopesAsSelectListAsync(IEnumerable<Scope> scopes = null) => new SelectList(await GetScopesAsync(), "Id", "DisplayName", scopes?.Select(p => p.Id));
        private Task<SelectList> GetDecisionStrategyAsSelectListAsync(DecisionStrategy decisionStrategy = DecisionStrategy.Affirmative) => Task.FromResult(new SelectList(EnumExtensions.GetAsList<DecisionStrategy>(), decisionStrategy));

    }
}
