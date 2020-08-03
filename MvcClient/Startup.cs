using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(opt => {
                opt.DefaultScheme = "Cookies";
                opt.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies", opt => {
                //opt.SessionStore = new MemoryCacheTicketStore();
                //opt.Cookie.Domain = "localhost.mvc";
                opt.Cookie.Name = "client-mvc";
            }).AddOpenIdConnect("oidc", opt => {
                opt.SignInScheme = "Cookies";

                opt.Authority = "http://localhost:5000";
                opt.RequireHttpsMetadata = false;

                opt.ClientId = "mvc";
                opt.ClientSecret = "secret";
                //opt.ResponseType = "code id_token";
                opt.ResponseType = "code";
                //opt.Prompt = "login";
                
                opt.SaveTokens = true;
                opt.UsePkce = true;
                opt.GetClaimsFromUserInfoEndpoint = true;
                //opt.UseTokenLifetime = false;

                //opt.Scope.Add("profile");
                //opt.Scope.Add("api1");
                opt.Scope.Add("policy");
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
            });


            /*
             * 
             * To test this !!!!
             * http://localhost:5001/connect/permission?response_mode=analyse&client_id=mvc
             * 
             */

            //TODO(demarco): What i want
            services.AddPolicyClient(opt => {
                opt.Authority = "http://localhost:5001";
                opt.ClientId = "mvc";
                opt.RequireHttpsMetadata = false;
                opt.PermissionSplitter = "#";

                //opt.ClientId = "mvc";
                //opt.ClientSecret = "secret";

                opt.SaveTokens = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(); 

            app.UseRouting();

            app.UseAuthentication();
            app.UsePolicyClient();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseMvc(routes => {
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
