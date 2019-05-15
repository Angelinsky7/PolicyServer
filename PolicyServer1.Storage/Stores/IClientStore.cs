using PolicyServer1.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public interface IClientStore {

        Task<Client> GetAsync(Int32 clientId);
        Task<Client> GetFromClientIdAsync(String clientId);

        Task<Int32> CreateAsync(Models.Client newClient);
        Task UpdateAsync(Int32 clientId, Models.Client client);
        Task RemoveAsync(Int32 cliendId);
        Task RemoveClientIdAsync(String cliendId);
        
    }
}
