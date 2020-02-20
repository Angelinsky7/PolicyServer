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

                    Console.WriteLine("Waiting database to be up....");
                    //context.Database.EnsureCreated();
                    context.Database.Migrate();
                    Console.WriteLine("Database migrated");

                    if (context.Roles.SingleOrDefault(p => p.Id == "1") == null) {
                        context.Roles.Add(new IdentityRole { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
                        context.Roles.Add(new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" });
                        context.SaveChanges();
                        Console.WriteLine("Roles Created");
                    }
                    
                    UserManager<ApplicationUser> userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    ApplicationUser alice = userMgr.FindByNameAsync("alice").Result;
                    if (alice == null) {
                        alice = new ApplicationUser {
                            Id = "0564eba0-4bd6-4dd4-9b03-8190aed55a30",
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

                        context.UserRoles.Add(new IdentityUserRole<String> { UserId = alice.Id, RoleId = "1" });
                        context.UserRoles.Add(new IdentityUserRole<String> { UserId = alice.Id, RoleId = "2" });
                        context.SaveChanges();

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

                    //ApplicationUser alex = userMgr.FindByNameAsync("alex").Result;
                    //if (alex == null) {
                    //    alex = new ApplicationUser {
                    //        UserName = "alex"
                    //    };
                    //    IdentityResult result = userMgr.CreateAsync(alex, "Pass123$").Result;
                    //    if (!result.Succeeded) {
                    //        throw new Exception(result.Errors.First().Description);
                    //    }

                    //    System.Collections.Generic.List<Claim> claimsInfodevmassive = new System.Collections.Generic.List<Claim>();
                    //    for (Int32 i = 0; i < 1000; ++i) {
                    //        claimsInfodevmassive.Add(new Claim(CustomClaimTypes.Permission, Permissions.Users.Add + i.ToString()));
                    //    }

                    //    result = userMgr.AddClaimsAsync(alex, new Claim[]{
                    //        new Claim(JwtClaimTypes.Name, "Alex Dark"),
                    //        new Claim(JwtClaimTypes.GivenName, "Alex"),
                    //        new Claim(JwtClaimTypes.FamilyName, "Dark"),
                    //        new Claim(JwtClaimTypes.Email, "AlexDark@email.com"),
                    //        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    //        new Claim(JwtClaimTypes.WebSite, "http://alex.com"),
                    //        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    //        new Claim("location", "somewhere")
                    //    }.Union(claimsInfodevmassive)).Result;

                    //    if (!result.Succeeded) {
                    //        throw new Exception(result.Errors.First().Description);
                    //    }
                    //    Console.WriteLine("alex created");
                    //} else {
                    //    Console.WriteLine("alex already exists");
                    //}


                }
            }
        }
    }
}
