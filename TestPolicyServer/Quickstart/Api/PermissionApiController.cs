using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolicyServer1.Models;
using PolicyServer1.Stores;

namespace TestPolicyServer.Quickstart.Api {

    //[Authorize(AuthenticationSchemes = "oidc")]
    [Route("api/permission")]
    [ApiController]
    public class PermissionApiController : ControllerBase {

        private readonly IPermissionStore _permissionStore;

        public PermissionApiController(IPermissionStore resourceStore) => _permissionStore = resourceStore;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> Get() => _permissionStore.Query().ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> Get(Guid id) {
            Permission result = await _permissionStore.GetAsync(id);

            if (result == null) {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Permission>> Create(Permission newItem) {
            Guid result = await _permissionStore.CreateAsync(newItem);

            if (result == null) {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = result }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Permission>> Update(Guid id, Permission newItem) {
            if (id != newItem.Id) { return BadRequest(); }

            await _permissionStore.UpdateAsync(id, newItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Permission>> Delete(Guid id) {
            if (id == null) { return BadRequest(); }

            await _permissionStore.RemoveAsync(id);

            return NoContent();
        }


    }
}