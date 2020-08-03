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

        public static IEnumerable<ApiResource> GetApiResources() {
            return new ApiResource[] {
                new ApiResource("api1", "My API") {
                    Scopes = {
                        "api1"
                    }
                },
                new ApiResource("policy", "Policy Server", new [] {
                    IdentityModel.JwtClaimTypes.Id,
                    IdentityModel.JwtClaimTypes.Subject,
                    IdentityModel.JwtClaimTypes.Profile,
                    IdentityModel.JwtClaimTypes.Email,
                    IdentityModel.JwtClaimTypes.Name,
                    IdentityModel.JwtClaimTypes.Scope,
                    IdentityModel.JwtClaimTypes.Role
                }){
                    Scopes = {
                        "policy"
                    },
                    ApiSecrets = {
                        new Secret("secret".Sha256())
                    }
                },
                new ApiResource("policy-admin", "Policy Server Administration panel", new [] {
                    IdentityModel.JwtClaimTypes.Id,
                    IdentityModel.JwtClaimTypes.Subject,
                    IdentityModel.JwtClaimTypes.Profile,
                    IdentityModel.JwtClaimTypes.Email,
                    IdentityModel.JwtClaimTypes.Name,
                    IdentityModel.JwtClaimTypes.Scope,
                    IdentityModel.JwtClaimTypes.Role
                }){
                    Scopes = {
                        "policy-admin"
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes() {
            return new List<ApiScope> {
                new ApiScope("api1", "My read API"),
                new ApiScope("policy", "My Policy manage"),
                new ApiScope("policy-admin", "My Policy admin"),
                //new ApiScope("policy", "Policy Server", new [] {
                //    IdentityModel.JwtClaimTypes.Id,
                //    IdentityModel.JwtClaimTypes.Subject,
                //    IdentityModel.JwtClaimTypes.Profile,
                //    IdentityModel.JwtClaimTypes.Email,
                //    IdentityModel.JwtClaimTypes.Name,
                //    IdentityModel.JwtClaimTypes.Scope,
                //    IdentityModel.JwtClaimTypes.Role
                //}),
                //new ApiScope("policy-admin", "Policy Server Administration panel", new [] {
                //    IdentityModel.JwtClaimTypes.Id,
                //    IdentityModel.JwtClaimTypes.Subject,
                //    IdentityModel.JwtClaimTypes.Profile,
                //    IdentityModel.JwtClaimTypes.Email,
                //    IdentityModel.JwtClaimTypes.Name,
                //    IdentityModel.JwtClaimTypes.Scope,
                //    IdentityModel.JwtClaimTypes.Role
                //})
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

                    //AllowedGrantTypes = GrantTypes.Code, 
                    //AllowedGrantTypes = GrantTypes.Hybrid,
                    //AllowedGrantTypes = GrantTypes.Code,
                    AllowedGrantTypes = {
                        GrantType.AuthorizationCode,
                        GrantType.ClientCredentials
                    },
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
                    AllowOfflineAccess = true,
                    RequirePkce = true,
                    //AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenType = AccessTokenType.Reference
                    
                    //TODO(demarco): please remove this in prod
                    //RequireConsent = false
                },

                new Client {
                    ClientId = "policy",
                    ClientName = "Policy Client",

                    AllowedGrantTypes = {
                        GrantType.AuthorizationCode,
                        GrantType.ClientCredentials,
                    },
                    //AllowedGrantTypes = GrantTypes.Hybrid,
                    //AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "http://localhost:5001/signin-oidc" }, 
                    //FrontChannelLogoutUri = "http://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback.oidc" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "policy-admin"
                        //CustomScopes.Scope
                    },
                    AllowOfflineAccess = true,
                    //AccessTokenType = AccessTokenType.Jwt
                    AccessTokenType = AccessTokenType.Reference

                    //TODO(demarco): please remove this in prod
                    //RequireConsent = false
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