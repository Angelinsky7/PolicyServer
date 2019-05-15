using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public interface IRoleStore {

        Task<PolicyRole> GetAsync((Int32 policyId, Int32 roleId) key);
        Task<Int32> CreateAsync(Models.PolicyRole newRole);
        Task UpdateAsync((Int32 policyId, Int32 roleId) key, Models.PolicyRole role);
        Task RemoveAsync((Int32 policyId, Int32 roleId) key);

    }
}
