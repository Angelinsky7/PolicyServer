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
    [Route("api/role")]
    [ApiController]
    public class RoleApiController : ControllerBase {

        private readonly IRoleStore _roleStore;

        public RoleApiController(IRoleStore roleStore) => _roleStore = roleStore;

        [HttpGet]
        public Task<ActionResult<IEnumerable<Role>>> Get() => Task.FromResult<ActionResult<IEnumerable<Role>>>(_roleStore.Query().ToList());

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(Guid id) {
            Role result = await _roleStore.GetAsync(id);

            if (result == null) {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Create(Role newItem) {
            Guid result = await _roleStore.CreateAsync(newItem);

            if (result == null) {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = result }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> Update(Guid id, Role newItem) {
            if (id != newItem.Id) { return BadRequest(); }

            await _roleStore.UpdateAsync(id, newItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> Delete(Guid id) {
            if (id == null) { return BadRequest(); }

            await _roleStore.RemoveAsync(id);

            return NoContent();
        }


    }
}