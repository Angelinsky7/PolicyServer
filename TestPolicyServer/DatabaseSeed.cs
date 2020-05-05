#define RESET_DATABASE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolicyServer1.EntityFramework.Storage;
using PolicyServer1.EntityFramework.Storage.Datas;
using PolicyServer1.EntityFramework.Storage.Entities;
using PolicyServer1.EntityFramework.Storage.Mappers;
using PolicyServer1.Stores;

namespace TestPolicyServer {
    public static class DatabaseSeed {

        public static void InitializeDatabase(this IApplicationBuilder app) {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                //serviceScope.ServiceProvider.GetRequiredService<PolicyServer1.EntityFramework.Storage.Datas.PolicyDbContext>().Database.Migrate();

                PolicyDbContext context = serviceScope.ServiceProvider.GetRequiredService<PolicyDbContext>();
                IClientStore clientStore = serviceScope.ServiceProvider.GetRequiredService<IClientStore>();

                context.Database.Migrate();

                //if (!context.Scopes.Any()) {
                //    foreach (PolicyServer1.Models.Scope scope in Config.GetScopes()) {
                //        PolicyServer1.EntityFramework.Storage.Entities.Scope entity = scope.ToEntity();
                //        context.Scopes.Add(entity);
                //    }
                //    context.SaveChanges();
                //}

                //var scopes = Config.GetScopes();
                //var firstScope = scopes.First().ToEntity();
                //var resources = Config.GetResouces(scopes);
                //var firstResouce = resources.First().ToEntity();

                if (context.Clients.Any()) {
                    PolicyServer1.Models.Client entityTest001 = clientStore.GetFromClientIdAsync("test001").Result;
                    PolicyServer1.Models.Client entityMvc = clientStore.GetFromClientIdAsync("mvc").Result;
                }

#if RESET_DATABASE
                context.Clients.RemoveRange(context.Clients);
                context.SaveChanges();
                context.Permissions.RemoveRange(context.Permissions);
                context.SaveChanges();
                context.Policies.RemoveRange(context.Policies.Where(p => p is AggregatedPolicy).Include(p => (p as AggregatedPolicy).Policies).ThenInclude(p => p.Policy));
                context.SaveChanges();
                context.Policies.RemoveRange(context.Policies);
                context.SaveChanges();
                context.Resources.RemoveRange(context.Resources);
                context.SaveChanges();
                context.Roles.RemoveRange(context.Roles.Include(p => p.Parents).ThenInclude(p => p.Parent));
                context.SaveChanges();
                context.Roles.RemoveRange(context.Roles);
                context.SaveChanges();
                context.Scopes.RemoveRange(context.Scopes);
                context.SaveChanges();
#endif

                if (!context.Clients.Any()) {
                    foreach (PolicyServer1.Models.Client client in Config.GetClients().Take(1)) {
                        //_ = clientStore.CreateAsync(client).Result;

                        Client entity = client.ToEntity();

                        if (entity.ClientId == "mvc") {
                            Boolean shouldNotBeNull = entity.Resources.FirstOrDefault().Resource.Scopes.FirstOrDefault().Resource != null;
                            Boolean shouldBeSame = entity.Scopes.First().Scope.CheckId == entity.Resources.First().Resource.Scopes.First().Scope.CheckId;
                        }

                        context.Clients.Add(entity);
                    }
                    context.SaveChanges();
                }
            }
        }

    }
}
