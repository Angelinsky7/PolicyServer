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


#if RESET_DATABASE
                context.Clients.RemoveRange(context.Clients);
                context.Resources.RemoveRange(context.Resources);
                context.Roles.RemoveRange(context.Roles);
                context.Scopes.RemoveRange(context.Scopes);
                context.Permissions.RemoveRange(context.Permissions);
                context.Policies.RemoveRange(context.Policies);
                context.SaveChanges();
#endif

                if (!context.Clients.Any()) {
                    foreach (PolicyServer1.Models.Client client in Config.GetClients()) {
                        //_ = clientStore.CreateAsync(client).Result;

                        Client entity = client.ToEntity();
                        
                        Boolean shouldNotBeNull = entity.Resources.FirstOrDefault().Resource.Scopes.FirstOrDefault().Resource != null;
                        Boolean shouldBeSame = entity.Scopes.First().Scope.CheckId == entity.Resources.First().Resource.Scopes.First().Scope.CheckId;

                        context.Clients.Add(entity);
                    }
                    context.SaveChanges();
                }
            }
        }

    }
}
