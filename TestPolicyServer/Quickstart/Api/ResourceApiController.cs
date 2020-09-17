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
    [Route("api/resource")]
    [ApiController]
    public class ResourceApiController : ControllerBase {

        private readonly IResourceStore _resourceStore;

        public ResourceApiController(IResourceStore resourceStore) => _resourceStore = resourceStore;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> Get() => _resourceStore.Query().ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> Get(Guid id) {
            Resource result = await _resourceStore.GetAsync(id);

            if (result == null) {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Resource>> Create(Resource newItem) {
            Guid result = await _resourceStore.CreateAsync(newItem);

            if (result == null) {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = result }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Resource>> Update(Guid id, Resource newItem) {
            if (id != newItem.Id) { return BadRequest(); }

            await _resourceStore.UpdateAsync(id, newItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Resource>> Delete(Guid id) {
            if (id == null) { return BadRequest(); }

            await _resourceStore.RemoveAsync(id);

            return NoContent();
        }


    }
}