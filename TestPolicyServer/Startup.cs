using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestPolicyServer;

namespace TestPolicyServer1 {
    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            //services.AddDbContext<PolicyDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            services.Configure<ForwardedHeadersOptions>(opt => {
                opt.ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                //TODO(demarco): Need to clarify that !!!
                Console.WriteLine("KnownNetworks: " + String.Join(", ", opt.KnownNetworks));
                Console.WriteLine("KnownProxies:" + String.Join(", ", opt.KnownProxies));
                opt.KnownNetworks.Clear();
                opt.KnownProxies.Clear();
            });

            //TODO(demarco): Change to protect api with identity server....
            //services.AddAuthentication("Cookies").AddCookie("Cookies");

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt => {
                    opt.Authority = "http://localhost:5000";
                    opt.RequireHttpsMetadata = false;
                    opt.Audience = "policy";
                }
            );

            services.AddPolicyServer(opt => {
                //opt.Caching.clientStoreExpiration = ..;
            })

            //    //.AddConfigurationStore(opt => {
            //    //    opt.ConfigureDbContext = ctx => ctx.UseSqlServer("", sql => sql.MigrationsAssembly(""));
            //    //})

                .AddInMemoryPolicies(Config.GetClients());

            //.AddclientStore<PolicyDbContext>()
            //.AddclientStore(opt => { opt.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(...)); })
            //.AddInMemoryPolicies(Config.GetPolicies())
            //.AddLocalPolicy(Configuration.GetSection("Policy"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseForwardedHeaders();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/policy");

            app.Use((context, next) => {
                Console.WriteLine("Headers: " + String.Join(", ", context.Request.Headers));
                Console.WriteLine("Host is " + context.Request.Host);
                Console.WriteLine("PathBase is " + context.Request.PathBase.Value);
                Console.WriteLine("Path is " + context.Request.Path.Value);
                return next();
            });

            app.UseAuthentication();

            app.UsePolicyServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
