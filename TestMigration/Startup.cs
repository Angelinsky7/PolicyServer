using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolicyServer1.EntityFramework.Storage.Datas;
using PolicyServer1.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestMigration {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            const String connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=TestMigration;trusted_connection=yes;";
            String migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddPolicyServer()
                .AddConfigurationStore(opt => {
                    opt.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)).EnableSensitiveDataLogging(true);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            InitializeDatabase(app);
            TestCrudApp(app);

            app.Run(async (context) => {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void TestCrudApp(IApplicationBuilder app) {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                IClientStore clientStore = serviceScope.ServiceProvider.GetRequiredService<IClientStore>();
                IPermissionStore permissionStore = serviceScope.ServiceProvider.GetRequiredService<IPermissionStore>();
                IRoleStore roleStore = serviceScope.ServiceProvider.GetRequiredService<IRoleStore>();

                //String clientId = "test";
                //Int32 client_Id = -1;
                //Int32 policy_Id = -1;
                //String permissionId = "PermissionId01";
                //Int32 permission_Id = -1;
                //String RoleId = "Role01";
                //Int32 role_Id = -1;


                //PolicyServer1.Models.Client existsingClient = null;
                //PolicyServer1.Models.PolicyPermission existsingPermission = null;
                //List<PolicyServer1.Models.Permission> permissions = new List<PolicyServer1.Models.Permission>();
                //PolicyServer1.Models.PolicyRole existsingRole = null;


                //Task.Run(async () => {

                //    try {
                //        await clientStore.RemoveClientIdAsync(clientId);
                //    } catch (Exception) { }

                //    PolicyServer1.Models.Client newClient = new PolicyServer1.Models.Client {
                //        ClientId = clientId,
                //        ClientName = "ceci est un test",
                //        Description = "ceci est une description",
                //    };

                //    client_Id = await clientStore.CreateAsync(newClient);

                //    existsingClient = await clientStore.GetFromClientIdAsync(clientId);
                //    existsingClient.Description = "une autre descripiton";
                //    existsingClient.ClientUri = "http://localhost:50022";

                //    await clientStore.UpdateAsync(existsingClient.Id, existsingClient);

                //    policy_Id = existsingClient.Policy.Id;
                //}).Wait();

                //Task.Run(async () => {
                //    try {
                //        //await permissionStore.RemoveAsync((clientId, permissionId));
                //    } catch (Exception) { }

                //    PolicyServer1.Models.PolicyPermission newClientPermission = new PolicyServer1.Models.PolicyPermission {
                //        PolicyId = policy_Id,
                //        Name = permissionId,
                //        Description = "Cecei un un desdcription"
                //    };

                //    permission_Id = await permissionStore.CreateAsync(newClientPermission);

                //    existsingPermission = await permissionStore.GetAsync((policy_Id, permission_Id));
                //    existsingPermission.Description = "L'action de permission - 01";
                //    existsingPermission.IsRevoked = false;

                //    await permissionStore.UpdateAsync((client_Id, permission_Id), existsingPermission);

                //    if ((await permissionStore.GetByNameAsync(policy_Id, "Permission_0")) == null) {
                //        for (Int32 i = 0; i < 20; ++i) {
                //            await permissionStore.CreateAsync(new PolicyServer1.Models.PolicyPermission {
                //                PolicyId = policy_Id,
                //                Name = "Permission_" + i.ToString(),
                //                Description = "Description_" + i.ToString(),
                //            });
                //        }

                //        permissions = (await clientStore.GetAsync(existsingClient.Id)).Policy.Permissions;
                //    }
                //}).Wait();

                //Task.Run(async () => {
                //    try {
                //        //await permissionStore.RemoveAsync((clientId, permissionId));
                //    } catch (Exception) { }

                //    PolicyServer1.Models.PolicyRole newClientRole = new PolicyServer1.Models.PolicyRole {
                //        PolicyId = policy_Id,
                //        Name = RoleId,
                //        Description = "Cecei un un desdcription",
                //        IdentityRoles = {
                //            "id-role-01",
                //            "id-role-02"
                //        },
                //        Subjects = {
                //            "Stella",
                //            "Alice",
                //            "Bob"
                //        },
                //        Permissions = {
                //            existsingPermission
                //        }
                //    };

                //    role_Id = await roleStore.CreateAsync(newClientRole);

                //    existsingRole = await roleStore.GetAsync((policy_Id, role_Id));
                //    existsingRole.Description = "Le role de permission - 01";
                //    existsingRole.IdentityRoles.Remove("id-role-01");
                //    existsingRole.Subjects.Remove("Stella");
                //    existsingRole.Permissions.Add(permissions.Skip(5).First());
                //    existsingRole.Permissions.Add(permissions.Skip(6).First());
                //    existsingRole.Permissions.Add(permissions.Skip(7).First());
                //    existsingRole.Permissions.Add(permissions.Skip(8).First());
                //    //existsingRole.Permissions.Add(permissions.Skip(7).First());

                //    await roleStore.UpdateAsync((policy_Id, role_Id), existsingRole);

                //    existsingRole.Permissions.RemoveAt(0);

                //    await roleStore.UpdateAsync((policy_Id, role_Id), existsingRole);

                //    existsingRole.Permissions.First().IsRevoked = true;

                //    await roleStore.UpdateAsync((policy_Id, role_Id), existsingRole);
                //}).Wait();

                //Task.Run(async () => {

                //    PolicyServer1.Models.PolicyRole newClientRole = new PolicyServer1.Models.PolicyRole {
                //        PolicyId = policy_Id,
                //        Name = "childRole01",
                //        Description = "Cecei un un desdcription",
                //        IdentityRoles = {
                //            "id-role-02"
                //        },
                //        Subjects = {
                //            "Bob"
                //        },
                //        Permissions = {
                //              permissions.Skip(10).First()
                //        },
                //        Parents = {
                //            existsingRole
                //        }
                //    };

                //    newClientRole.Id = await roleStore.CreateAsync(newClientRole);

                //    PolicyServer1.Models.PolicyRole newClientRole2 = new PolicyServer1.Models.PolicyRole {
                //        PolicyId = policy_Id,
                //        Name = "childRole02",
                //        Description = "Cecei un un desdcription",
                //        IdentityRoles = {
                //            "id-role-02"
                //        },
                //        Subjects = {
                //            "Bob"
                //        },
                //        Permissions = {
                //              permissions.Skip(10).First()
                //        },
                //        Parents = {
                //            newClientRole
                //        }
                //    };

                //    newClientRole2.Id = await roleStore.CreateAsync(newClientRole2);

                //    PolicyServer1.Models.PolicyRole testChildRole02 = await roleStore.GetAsync((policy_Id, newClientRole2.Id));

                //}).Wait();

                //Task.Run(async () => {
                //    PolicyServer1.Models.PolicyRole test = await roleStore.GetAsync((policy_Id, existsingRole.Id + 1));
                //}).Wait();

            }
        }

        private void InitializeDatabase(IApplicationBuilder app) {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                serviceScope.ServiceProvider.GetRequiredService<PolicyDbContext>().Database.Migrate();

                using (PolicyDbContext context = serviceScope.ServiceProvider.GetRequiredService<PolicyDbContext>()) {
                    context.Database.Migrate();

                    Int32 policyId = 0;

                    //if (!context.Clients.Any()) {
                    //    context.Clients.Add(new PolicyServer1.EntityFramework.Storage.Entities.Client {
                    //        ClientId = "mvc",
                    //        ClientName = "MVC Client",
                    //        ClientUri = "http://localhost:5000",
                    //    });
                    //    context.SaveChanges();
                    //    policyId = context.Clients.FirstOrDefault(p => p.ClientId == "mvc").PolicyId ?? -1;
                    //}

                    //if (!context.Policies.Any()) {
                    //    PolicyServer1.EntityFramework.Storage.Entities.Policy newPolicy = new PolicyServer1.EntityFramework.Storage.Entities.Policy();
                    //    context.Policies.Add(newPolicy);
                    //    context.SaveChanges();

                    //    PolicyServer1.EntityFramework.Storage.Entities.Client client = context.Clients.Find(policyId);
                    //    client.PolicyId = newPolicy.Id;
                    //    context.SaveChanges();
                    //}

                    //if (!context.Permissions.Any()) {
                    //    context.Permissions.Add(new PolicyServer1.EntityFramework.Storage.Entities.Permission {
                    //        PolicyId = policyId,
                    //        Name = "Permission1",
                    //        Description = "Ceci est une permission - 1",
                    //    });
                    //    context.Permissions.Add(new PolicyServer1.EntityFramework.Storage.Entities.Permission {
                    //        PolicyId = policyId,
                    //        Name = "Permission2",
                    //        Description = "Ceci est une permission - 2",
                    //    });
                    //    context.Permissions.Add(new PolicyServer1.EntityFramework.Storage.Entities.Permission {
                    //        PolicyId = policyId,
                    //        Name = "Permission3",
                    //        Description = "Ceci est une permission - 3",
                    //    });
                    //    context.SaveChanges();
                    //}
                }

            }
        }
    }
}
