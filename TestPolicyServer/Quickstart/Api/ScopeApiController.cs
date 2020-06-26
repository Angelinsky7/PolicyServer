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
    [Route("api/scope")]
    [ApiController]
    public class ScopeApiController : ControllerBase {

        private readonly IScopeStore _scopeStore;

        public ScopeApiController(IScopeStore scopeStore) => _scopeStore = scopeStore;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scope>>> Get() => await _scopeStore.Query().AsNoTracking().ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Scope>> Get(Guid id) {
            Scope result = await _scopeStore.GetAsync(id);

            if (result == null) {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Scope>> Create(Scope newItem) {
            Guid result = await _scopeStore.CreateAsync(newItem);

            if (result == null) {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = result }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Scope>> Update(Guid id, Scope newItem) {
            if (id != newItem.Id) { return BadRequest(); }

            await _scopeStore.UpdateAsync(id, newItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Scope>> Delete(Guid id) {
            if (id == null) { return BadRequest(); }

            await _scopeStore.RemoveAsync(id);

            return NoContent();
        }


    }
}