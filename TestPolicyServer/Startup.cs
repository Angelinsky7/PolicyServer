using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

namespace TestPolicyServer {
    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            //services.AddDbContext<PolicyDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddMvc()
            //    .AddJsonOptions(opt => {
            //        opt.JsonSerializerOptions.WriteIndented = true;
            //        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //    })
            //    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.Configure<ForwardedHeadersOptions>(opt => {
                opt.ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                opt.KnownNetworks.Clear();
                opt.KnownProxies.Clear();
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(opt => {
                opt.DefaultScheme = "Bearer";
                //opt.DefaultScheme = "Cookies";
                opt.DefaultChallengeScheme = "oidc";
            })
                .AddJwtBearer("Bearer", opt => {
                    opt.Authority = "http://localhost:5000";
                    opt.RequireHttpsMetadata = false;
                    opt.Audience = "policy";
                })
                .AddCookie("Cookies", opt => {
                    opt.Cookie.Name = "client-policy-admin";
                    //opt.Cookie.Domain = "localhost.policy-admin"; 
                })
                .AddOpenIdConnect("oidc", opt => {
                    opt.SignInScheme = "Cookies";
                    
                    opt.Authority = "http://localhost:5000"; 
                    opt.RequireHttpsMetadata = false;

                    opt.ClientId = "policy";
                    opt.ClientSecret = "secret";
                    opt.ResponseType = "code id_token";

                    opt.SaveTokens = true;
                    opt.GetClaimsFromUserInfoEndpoint = true;
                    //opt.UseTokenLifetime = false;

                    //opt.Scope.Add("profile");
                    //opt.Scope.Add("api1");
                    opt.Scope.Add("policy-admin");
                    opt.Scope.Add("offline_access");
                    //opt.ClaimActions.MapJsonKey("website", "website");
                    //opt.ClaimActions.MapJsonKey(CustomClaimTypes.Permission, CustomClaimTypes.Permission);

                    opt.SignedOutCallbackPath = "/signout-callback.oidc";
                    opt.SignedOutRedirectUri = "/";

                    opt.Events = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents {
                        OnRemoteFailure = (context) => {
                            context.Response.Redirect("/");
                            context.HandleResponse();
                            return Task.CompletedTask;
                        }
                    };
                }
            );



            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", opt => {
            //        opt.Authority = "http://localhost:5000"; 
            //        opt.RequireHttpsMetadata = false;
            //        opt.Audience = "policy";
            //    }
            //);

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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseForwardedHeaders();

            if (env.IsDevelopment()) {
                //app.InitializeDatabase();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
                endpoints.MapDefaultControllerRoute();
            });
        }

    }
}
