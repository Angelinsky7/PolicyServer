using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public interface IPolicyStore : IStore<Policy, Guid> {

        //Task<Policy> GetAsync(Int32 policyId);
        //Task<Int32> CreateAsync(Models.Policy newPolicy);
        //Task UpdateAsync(Int32 policyId, Models.Policy policy);
        //Task RemoveAsync(Int32 policyId);

    }
}
