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
    [Route("api/policy")]
    [ApiController]
    public class PolicyApiController : ControllerBase {

        private readonly IPolicyStore _policyStore;

        public PolicyApiController(IPolicyStore resourceStore) => _policyStore = resourceStore;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> Get() => _policyStore.Query().ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<Policy>> Get(Guid id) {
            Policy result = await _policyStore.GetAsync(id);

            if (result == null) {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Policy>> Create(Policy newItem) {
            Guid result = await _policyStore.CreateAsync(newItem);

            if (result == null) {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = result }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Policy>> Update(Guid id, Policy newItem) {
            if (id != newItem.Id) { return BadRequest(); }

            await _policyStore.UpdateAsync(id, newItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Policy>> Delete(Guid id) {
            if (id == null) { return BadRequest(); }

            await _policyStore.RemoveAsync(id);

            return NoContent();
        }


    }
}