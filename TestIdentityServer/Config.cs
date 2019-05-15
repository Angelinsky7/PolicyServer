using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServerAspNetIdentity {
    public static class Config {

        public static IEnumerable<IdentityResource> GetIdentityResources() {
            return new IdentityResource[] {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResource (
                //    name: CustomScopes.Scope,
                //    displayName: "Infodev Test001 Scope",
                //    claimTypes: new[]{ CustomClaimTypes.Permission }
                //)
            };
        }

        public static IEnumerable<ApiResource> GetApis() {
            return new ApiResource[] {
                new ApiResource("api1", "My API"),
                new ApiResource("policy", "Policy Server", new [] {
                    IdentityModel.JwtClaimTypes.Id,
                    IdentityModel.JwtClaimTypes.Subject,
                    IdentityModel.JwtClaimTypes.Profile,
                    IdentityModel.JwtClaimTypes.Email,
                    IdentityModel.JwtClaimTypes.Name,
                    IdentityModel.JwtClaimTypes.Scope,
                    IdentityModel.JwtClaimTypes.Role
                })
            };
        }

        public static IEnumerable<Client> GetClients() {
            return new Client[] {
                //new Client {
                //    ClientId = "client",
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets = {
                //        new Secret("secret".Sha256())
                //    },
                //    AllowedScopes = { "api1" }
                //},
                //new Client {
                //    ClientId = "ro.client",
                //    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                //    ClientSecrets = {
                //        new Secret("secret".Sha256())
                //    },
                //    AllowedScopes = { "api1" }
                //},
                new Client {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    //AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "http://localhost:5005/signin-oidc" },
                    //FrontChannelLogoutUri = "http://localhost:5005/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5005/signout-callback.oidc" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                        "policy"
                        //CustomScopes.Scope
                    },
                    AllowOfflineAccess = true
                },
                //new Client {
                //    ClientId = "js",
                //    ClientName = "JavaScript Client",
                //    AllowedGrantTypes = GrantTypes.Code,
                //    //AllowedGrantTypes = GrantTypes.Implicit,
                //    RequirePkce = true,
                //    RequireClientSecret = false,
                //    //AllowAccessTokensViaBrowser = true,

                //    RedirectUris =           { "http://localhost:5003/callback.html" },
                //    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                //    AllowedCorsOrigins =     { "http://localhost:5003" },

                //    //RedirectUris = {
                //    //    "http://localhost:5002/index.html",
                //    //    "http://localhost:5002/callback.html",
                //    //    "http://localhost:5002/silent.html",
                //    //    "http://localhost:5002/popup.html",
                //    //},

                //    AllowedScopes = {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        "api1",
                //        //CustomScopes.Scope
                //    }
                //}
            };
        }

    }
}