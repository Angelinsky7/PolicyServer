using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public interface IPermissionStore : IStore<Permission, Guid> {

        //Task<PolicyPermission> GetAsync((Int32 policyId, Int32 permissionId) key);
        //Task<PolicyPermission> GetByNameAsync(Int32 policyId, String permissionName);
        //Task<Int32> CreateAsync(Models.PolicyPermission newPermission);
        //Task UpdateAsync((Int32 policyId, Int32 permissionId) key, Models.PolicyPermission permission);
        //Task RemoveAsync((Int32 policyId, Int32 permissionId) key);

        Task<Permission> GetByNameAsync(String name);

    }
}
