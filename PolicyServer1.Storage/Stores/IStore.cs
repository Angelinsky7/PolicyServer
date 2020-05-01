using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Stores {
    public interface IStore<TEntity, IKey> {

        IQueryable<TEntity> Query();
        Task<TEntity> GetAsync(IKey id);
        Task<IKey> CreateAsync(TEntity item);
        Task UpdateAsync(IKey id, TEntity item);
        Task RemoveAsync(IKey id);

    }
}
