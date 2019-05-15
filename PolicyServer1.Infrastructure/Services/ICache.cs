using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Services {
    public interface ICache<T> where T : class {

        Task<T> GetAsync(String key);
        Task SetAsync(String key, T item, TimeSpan expiration);

    }
}
