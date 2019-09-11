using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public interface IStore<TEntity, IKey> {

        IQueryable<TEntity> Get();
        Task<TEntity> GetAsync(IKey id);
        Task<TEntity> CreateAsync(TEntity item);
        Task<TEntity> UpdateAsync(IKey id, TEntity item);
        Task<TEntity> RemoveAsync(IKey id);

    }
}
