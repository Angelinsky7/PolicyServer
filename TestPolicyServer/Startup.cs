using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using PolicyServer1.EntityFramework.Storage.Datas;
using PolicyServer1.EntityFramework.Storage.Mappers;
using TestPolicyServer;

namespace TestPolicyServer1 {
    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            //services.AddDbContext<PolicyDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                .AddJsonOptions(opt => {
                    opt.JsonSerializerOptions.WriteIndented = true;
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);


            services.Configure<ForwardedHeadersOptions>(opt => {
                opt.ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                opt.KnownNetworks.Clear();
                opt.KnownProxies.Clear();
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt => {
                    opt.Authority = "http://localhost:5000"; 
                    opt.RequireHttpsMetadata = false;
                    opt.Audience = "policy";
                }
            );

            //services.AddPolicyServer(opt => {
            //    //opt.Caching.clientStoreExpiration = ..;
            //}).AddInMemoryPolicies(Config.GetClients());







            if (false) {
                services.AddPolicyServer(opt => {
                }).AddInMemoryPolicies(Config.GetClients());
            } else {
                services.AddPolicyServer(opt => { })
                    .AddConfigurationStore(opt => {
                        opt.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name))
                            .EnableSensitiveDataLogging();
                    });
            }







            //    //.AddConfigurationStore(opt => {
            //    //    opt.ConfigureDbContext = ctx => ctx.UseSqlServer("", sql => sql.MigrationsAssembly(""));
            //    //})
            //.AddclientStore<PolicyDbContext>()
            //.AddclientStore(opt => { opt.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(...)); })
            //.AddInMemoryPolicies(Config.GetPolicies())
            //.AddLocalPolicy(Configuration.GetSection("Policy"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseForwardedHeaders();

            if (env.IsDevelopment()) {
                //app.InitializeDatabase();
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/policy");

            //app.Use((context, next) => {
            //    Console.WriteLine("Headers: " + String.Join(", ", context.Request.Headers));
            //    Console.WriteLine("Host is " + context.Request.Host);
            //    Console.WriteLine("PathBase is " + context.Request.PathBase.Value);
            //    Console.WriteLine("Path is " + context.Request.Path.Value);
            //    return next();
            //});

            //app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UsePolicyServer();

            //app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

    }
}
