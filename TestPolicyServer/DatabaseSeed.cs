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

namespace TestPolicyServer {
    public static class DatabaseSeed {

        public static void InitializeDatabase(this IApplicationBuilder app) {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                //serviceScope.ServiceProvider.GetRequiredService<PolicyServer1.EntityFramework.Storage.Datas.PolicyDbContext>().Database.Migrate();

                PolicyDbContext context = serviceScope.ServiceProvider.GetRequiredService<PolicyDbContext>();
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


                if (!context.Clients.Any()) {
                    foreach (PolicyServer1.Models.Client client in Config.GetClients()) {
                        Client entity = client.ToEntity();
                        entity.PrepareBeforeInsert();

                        context.Clients.Add(entity);
                    }
                    context.SaveChanges();
                }
            }
        }

    }
}
