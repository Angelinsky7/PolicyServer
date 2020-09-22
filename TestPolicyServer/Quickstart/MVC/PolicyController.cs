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
using TestPolicyServer.Extensions;

namespace TestPolicyServer.Quickstart.MVC {

    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = "oidc")]
    public class PolicyController : Controller {

        private readonly IPolicyStore _policyStore;

        public PolicyController(IPolicyStore policyStore) => _policyStore = policyStore;

        public async Task<IActionResult> Index(ListViewModel viewModel) => View("Index", await BuildViewModelAsync(viewModel));

        #region RolePolicy

        public async Task<IActionResult> CreateRolePolicyAsync() {
            RolePolicy item = new RolePolicy();

            ViewBag.Logics = await GetLogicsAsSelectListAsync(item.Logic);
            //ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        [HttpPost, ActionName("CreateRolePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRolePolicyPostAsync([Bind("Id,Name,Description,Logic,Roles")] RolePolicy item) {
            //if (id != movie.ID) {
            //    return NotFound();
            //}

            if (ModelState.IsValid) {
                try {
                    Guid guid = await _policyStore.CreateAsync(item);
                } catch (DbUpdateConcurrencyException) {
                    //if (!(await ScopeExistsAsync(guid))) {
                    //    return NotFound();
                    //} else {
                    throw;
                    //}
                }
                return RedirectToAction("Index");
            }

            ViewBag.Logics = await GetLogicsAsSelectListAsync(item.Logic);
            //ViewBag.RolePolicys = await GetRolePolicysAsSelectListAsync(item.RolePolicy);
            //ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        public async Task<IActionResult> EditRolePolicyAsync(Guid id) {
            if (id == null) { return NotFound(); }
            if (!(await _policyStore.GetAsync(id) is RolePolicy item)) { return NotFound(); }

            ViewBag.Logics = await GetLogicsAsSelectListAsync(item.Logic);
            //ViewBag.RolePolicys = await GetRolePolicysAsSelectListAsync(item.RolePolicy);
            //ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        [HttpPost, ActionName("EditRolePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRolePolicyPostAsync(Guid id, [Bind("Id,Name,Description,Logic,Roles")] RolePolicy item) {
            if (id != item.Id) { return NotFound(); }

            //item.RolePolicy = item?.RolePolicy?.Id != null ? await _resourceStore.GetAsync(item.RolePolicy.Id) : null;

            if (ModelState.IsValid) {
                try {
                    await _policyStore.UpdateAsync(id, item);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await PolicyExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.Logics = await GetLogicsAsSelectListAsync(item.Logic);
            //ViewBag.RolePolicys = await GetRolePolicysAsSelectListAsync(item.RolePolicy);
            //ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        #endregion

        #region TimePolicy

        public async Task<IActionResult> CreateTimePolicyAsync() {
            TimePolicy item = new TimePolicy();

            ViewBag.Logics = await GetLogicsAsSelectListAsync(item.Logic);
            //ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        [HttpPost, ActionName("CreateTimePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTimePolicyPostAsync([Bind("Id,Name,Description,Logic,NotBefore,NotOnOrAfter,DayOfMonth,Month,Year,Hour,Minute")] TimePolicy item) {
            //if (id != movie.ID) {
            //    return NotFound();
            //}

            if (ModelState.IsValid) {
                try {
                    Guid guid = await _policyStore.CreateAsync(item);
                } catch (DbUpdateConcurrencyException) {
                    //if (!(await ScopeExistsAsync(guid))) {
                    //    return NotFound();
                    //} else {
                    throw;
                    //}
                }
                return RedirectToAction("Index");
            }

            ViewBag.Logics = await GetLogicsAsSelectListAsync(item.Logic);
            //ViewBag.TimePolicys = await GetTimePolicysAsSelectListAsync(item.TimePolicy);
            //ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        public async Task<IActionResult> EditTimePolicyAsync(Guid id) {
            if (id == null) { return NotFound(); }
            if (!(await _policyStore.GetAsync(id) is TimePolicy item)) { return NotFound(); }

            ViewBag.Logics = await GetLogicsAsSelectListAsync(item.Logic);
            //ViewBag.TimePolicys = await GetTimePolicysAsSelectListAsync(item.TimePolicy);
            //ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        [HttpPost, ActionName("EditTimePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTimePolicyPostAsync(Guid id, [Bind("Id,Name,Description,Logic,NotBefore,NotOnOrAfter,DayOfMonth,Month,Year,Hour,Minute")] TimePolicy item) {
            if (id != item.Id) { return NotFound(); }

            //item.TimePolicy = item?.TimePolicy?.Id != null ? await _resourceStore.GetAsync(item.TimePolicy.Id) : null;

            if (ModelState.IsValid) {
                try {
                    await _policyStore.UpdateAsync(id, item);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await PolicyExistsAsync(id))) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.Logics = await GetLogicsAsSelectListAsync(item.Logic);
            //ViewBag.TimePolicys = await GetTimePolicysAsSelectListAsync(item.TimePolicy);
            //ViewBag.DecisionStrategies = await GetDecisionStrategyAsSelectListAsync(item.DecisionStrategy);

            return View(item);
        }

        #endregion


        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreatePostAsync([Bind("Id,Name,Description,Parents")] Policy item) {
        //    //if (id != movie.ID) {
        //    //    return NotFound();
        //    //}

        //    if (ModelState.IsValid) {
        //        try {
        //            Guid guid = await _policyStore.CreateAsync(item);
        //        } catch (DbUpdateConcurrencyException) {
        //            //if (!(await PolicyExistsAsync(guid))) {
        //            //    return NotFound();
        //            //} else {
        //            throw;
        //            //}
        //        } catch (DbUpdateException ex) {
        //            String error = CheckHandleError(ex);
        //            if (error != null) {
        //                ModelState.AddModelError("flash.error", error);
        //                return View(item);
        //            } else {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(item);
        //}

        //public async Task<IActionResult> EditAsync(Guid id) {
        //    if (id == null) { return NotFound(); }
        //    Policy item = await _policyStore.GetAsync(id);
        //    if (item == null) { return NotFound(); }
        //    return View(item);
        //}

        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditPostAsync(Guid id, [Bind("Id,Name,Description,Parents")] Policy item) {
        //    if (id != item.Id) { return NotFound(); }

        //    if (ModelState.IsValid) {
        //        try {
        //            await _policyStore.UpdateAsync(id, item);
        //        } catch (DbUpdateConcurrencyException) {
        //            if (!(await PolicyExistsAsync(id))) {
        //                return NotFound();
        //            } else {
        //                throw;
        //            }
        //        } catch (DbUpdateException ex) {
        //            String error = CheckHandleError(ex);
        //            if (error != null) {
        //                ModelState.AddModelError("flash.error", error);
        //                return View(item);
        //            } else {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(item);
        //}





        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression d'une Policy", OkButton = "Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id) {
            if (id == null) { return NotFound(); }
            Policy item = await _policyStore.GetAsync(id);
            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(Guid id) {
            if (id == null) { return BadRequest(); }

            if (ModelState.IsValid) {
                try {
                    await _policyStore.RemoveAsync(id);
                } catch (DbUpdateConcurrencyException) {
                    if (!(await PolicyExistsAsync(id))) {
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

            Policy item = await _policyStore.GetAsync(id);
            return View(item);
        }

        [ViewLayoutModal("~/Views/Shared/_Modal.cshtml", Title = "Suppression de plusieurs Policies", OkButton = "Delete")]
        public async Task<IActionResult> DeleteMultipleAsync([FromQuery] Guid[] guid) {
            if (guid == null) { return NotFound(); }
            List<Policy> items = new List<Policy>();
            foreach (Guid id in guid) {
                Policy item = await _policyStore.GetAsync(id);
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
                        await _policyStore.RemoveAsync(id);
                    }
                } catch (DbUpdateConcurrencyException) {
                    foreach (Guid id in guid) {
                        if (!(await PolicyExistsAsync(id))) {
                            return NotFound();
                        } else {
                            throw;
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            {
                List<Policy> items = new List<Policy>();
                foreach (Guid id in guid) {
                    Policy item = await _policyStore.GetAsync(id);
                    if (item == null) { return NotFound(); }
                    items.Add(item);
                }
                return View(items.ToArray());
            }
        }

        private async Task<PoliciesViewModel> BuildViewModelAsync(ListViewModel viewModel) {
            IQueryable<Policy> query = _policyStore.Query();

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

            List<Policy> policys = query.ToList();
            //List<Policy> policys = queryBug.ToList();

            List<Policy> list = new List<Policy>();
            foreach (Policy policy in policys) {
                list.Add(policy);
            }

            return new PoliciesViewModel {
                Count = list.Count,
                TotalItems = countTotal,
                CurrentPage = viewModel.CurrentPage,
                PageSize = viewModel.PageSize,
                SortOrder = viewModel.SortOrder,
                SearchString = viewModel.SearchString,
                Policies = list
            };
        }

        private async Task<Boolean> PolicyExistsAsync(Guid id) => (await _policyStore.GetAsync(id)) != null;

        private String CheckHandleError(Exception ex) {
            return SqlErrorManager.Instance.FormatError(ex);
        }

        private Task<SelectList> GetLogicsAsSelectListAsync(PolicyLogic policyLogic = PolicyLogic.Positive) => Task.FromResult(new SelectList(EnumExtensions.GetAsList<PolicyLogic>(), policyLogic));

    }
}
