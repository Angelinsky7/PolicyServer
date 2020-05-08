using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PolicyServer1.EntityFramework.Storage.Interfaces;

namespace PolicyServer1.EntityFramework.Storage {
    public static class DbContextExtensions {

        public static void DetachEntites<TEntity>(this IPolicyDbContext context) where TEntity : class {
            IEnumerable<EntityEntry<TEntity>> entities = context.ChangeTracker.Entries<TEntity>().ToList();

            foreach (EntityEntry<TEntity> item in entities) {
                item.State = EntityState.Detached;
            }
        }

        public static async Task CheckExistingAndRemoveAsync<TEntity>(this IPolicyDbContext context, IEqualityComparer<TEntity> comparer = null) where TEntity : class {
            IEnumerable<TEntity> existingEntities = await context.Set<TEntity>().AsNoTracking().ToListAsync();
            IEnumerable<EntityEntry<TEntity>> entitiesToAdd = context.ChangeTracker.Entries<TEntity>().ToList();

            foreach (EntityEntry<TEntity> item in entitiesToAdd) {
                if (comparer != null ? existingEntities.Contains(item.Entity, comparer) : existingEntities.Contains(item.Entity)) {
                    item.State = EntityState.Unchanged;
                }
            }
        }

        public static async Task CheckExistingAndRemoveAsync<TEntity>(this IPolicyDbContext context, Func<TEntity, TEntity, Boolean> comparer) where TEntity : class {
            IEnumerable<TEntity> existingEntities = await context.Set<TEntity>().AsNoTracking().ToListAsync();
            IEnumerable<EntityEntry<TEntity>> entitiesToAdd = context.ChangeTracker.Entries<TEntity>().ToList();

            foreach (EntityEntry<TEntity> item in entitiesToAdd) {
                if (existingEntities.Any(p => comparer(p, item.Entity))) {
                    item.State = EntityState.Unchanged;
                }
            }
        }

    }
}
