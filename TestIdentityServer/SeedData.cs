// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerAspNetIdentity {
    public class SeedData {

        public static void EnsureSeedData(String connectionString) {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
               //options.UseSqlite(connectionString));
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (ServiceProvider serviceProvider = services.BuildServiceProvider()) {
                using (IServiceScope scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                    ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    UserManager<ApplicationUser> userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    ApplicationUser alice = userMgr.FindByNameAsync("alice").Result;
                    if (alice == null) {
                        alice = new ApplicationUser {
                            UserName = "alice"
                        };
                        IdentityResult result = userMgr.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded) {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;
                        if (!result.Succeeded) {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Console.WriteLine("alice created");
                    } else {
                        Console.WriteLine("alice already exists");
                    }

                    ApplicationUser bob = userMgr.FindByNameAsync("bob").Result;
                    if (bob == null) {
                        bob = new ApplicationUser {
                            UserName = "bob"
                        };
                        IdentityResult result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded) {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim("location", "somewhere")
                    }).Result;
                        if (!result.Succeeded) {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Console.WriteLine("bob created");
                    } else {
                        Console.WriteLine("bob already exists");
                    }

                    ApplicationUser alex = userMgr.FindByNameAsync("alex").Result;
                    if (alex == null) {
                        alex = new ApplicationUser {
                            UserName = "alex",
                        };
                        IdentityResult result = userMgr.CreateAsync(alex, "Pass123$").Result;
                        if (!result.Succeeded) {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alex, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alex Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alex"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AlexSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alex.com"),
                            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim("location", "somewhere")
                        }).Result;

                        if (!result.Succeeded) {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRolesAsync(alex, new String[]{
                            "Administrator",
                            "User"
                        }).Result;

                        if (!result.Succeeded) {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Console.WriteLine("alex created");
                    } else {
                        Console.WriteLine("alex already exists");
                    }

                }
            }
        }
    }
}
