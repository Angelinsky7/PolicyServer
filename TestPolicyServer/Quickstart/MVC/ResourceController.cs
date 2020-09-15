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
    public class ResourceController : Controller {

        private readonly IResourceStore _resourceStore;

        public ResourceController(IResourceStore scopeStore) => _resourceStore = scopeStore;

        public async Task<IActionResult> Index(ListViewModel viewModel) {
            return View("Index", await BuildViewModelAsync(viewModel));
        }

        public IActionResult Create() {
            Resource item = new Resource();
            return View(item);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePostAsync([Bind("Id,Name,DisplayName,Type,Uris,Scopes,IconUri")] Resource item) {
            //if (id != movie.ID) {
            //    return NotFound();
            //}

            if (ModelState.IsValid) {
                try {
                    Guid guid = await _resourceStore.CreateAsync(item);
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
            Resource item = await _resourceStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPostAsync(Guid id, [Bind("Id,Name,DisplayName,Type,Uris,Scopes,IconUri")] Resource item) {
            if (id != item?.Id) { return NotFound(); }

            if (ModelState.IsValid) {
                try {
                    await _resourceStore.UpdateAsync(id, item);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await ResourceExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression d'une Resource", OkButton = "Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id) {
            if (id == null) { return NotFound(); }
            Resource item = await _resourceStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(Guid id) {
            if (id == null) { return BadRequest(); }

            if (ModelState.IsValid) {
                try {
                    await _resourceStore.RemoveAsync(id);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await ResourceExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            Resource item = await _resourceStore.GetAsync(id);
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression de plusieurs Resources", OkButton = "Delete")]
        public async Task<IActionResult> DeleteMultipleAsync([FromQuery] Guid[] guid) {
            if (guid == null) { return NotFound(); }
            List<Resource> items = new List<Resource>();
            foreach (Guid id in guid) {
                Resource item = await _resourceStore.GetAsync(id);
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
                        await _resourceStore.RemoveAsync(id);
                    }
                } catch (DbUpdateConcurrencyException) {
                    foreach (Guid id in guid) {
                        if (!(await ResourceExistsAsync(id))) {
                            return NotFound();
                        } else {
                            throw;
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            {
                List<Resource> items = new List<Resource>();
                foreach (Guid id in guid) {
                    Resource item = await _resourceStore.GetAsync(id);
                    if (item == null) { return NotFound(); }
                    items.Add(item);
                }
                return View(items.ToArray());
            }
        }

        private async Task<ResourcesViewModel> BuildViewModelAsync(ListViewModel viewModel) {
            IQueryable<Resource> query = _resourceStore.Query();

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

            List<Resource> scopes = await query.ToListAsync();

            List<Resource> list = new List<Resource>();
            foreach (Resource scope in scopes) {
                list.Add(scope);
            }

            return new ResourcesViewModel {
                Count = list.Count,
                TotalItems = countTotal,
                CurrentPage = viewModel.CurrentPage,
                PageSize = viewModel.PageSize,
                SortOrder = viewModel.SortOrder,
                SearchString = viewModel.SearchString,
                Resources = list
            };
        }

        private async Task<Boolean> ResourceExistsAsync(Guid id) => (await _resourceStore.GetAsync(id)) != null;

    }
}
