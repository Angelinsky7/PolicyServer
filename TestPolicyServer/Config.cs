using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace TestPolicyServer {
    public static class Config {

        public static IEnumerable<Client> GetClients() {
            return new Client[] {
                new Client {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    //AllowedScopes = {
                    //    "api1"
                    //},
                    //Secrets = {
                    //    new Secret("secret".Sha256())
                    //},
                    Policy = new Policy {
                        Roles = {
                            new Role {
                                Name = "TestRole1",
                                Subjects = {
                                    "inner",
                                    "outer",
                                    "0564eba0-4bd6-4dd4-9b03-8190aed55a30"
                                },
                                Permissions = {
                                    new Permission{ Name= "Activite.Select" },
                                    new Permission{ Name= "Activite.Add" },
                                    new Permission{ Name= "Activite.Edit" },
                                    new Permission{ Name= "Activite.Delete" },
                                },
                                Parents = {
                                    new Role {
                                        Name = "Parent",
                                        Permissions = {
                                            new Permission{ Name = "Notification.See.Own" },
                                            new Permission{ Name = "Notification.Send", IsRevoked = true }
                                        },
                                        Parents = {
                                            new Role {
                                                Name = "Grand-Parent",
                                                Permissions = {
                                                    new Permission{ Name = "Notification.See" },
                                                    new Permission{ Name = "Notification.Send" },
                                                    new Permission{ Name= "CanDoDoctorThings"}
                                                },
                                                //Parents = {
                                                //    new Role{ Name = "TestRole1" }
                                                //}
                                            }
                                        }
                                    }
                                }
                            },
                            new Role {
                                Name = "TestRole2",
                                Subjects = {
                                    "0564eba0-4bd6-4dd4-9b03-8190aed55a30"
                                },
                                Permissions = {
                                    new Permission{ Name= "Activite.Delete", IsRevoked = true }
                                }
                            },
                            new Role {
                                Name = "TestRole2",
                                Subjects = {
                                    "inner"
                                },
                                Permissions = {
                                    new Permission{ Name= "Vehicule.Select" },
                                    new Permission{ Name= "Vehicule.Add" },
                                    new Permission{ Name= "Vehicule.Edit" },
                                    new Permission{ Name= "Vehicule.Delete" }
                                }
                            },
                            new Role {
                                Name = "MonRole",
                                IdentityRoles = {
                                    "LdapGroupe1"
                                }
                            }
                        }
                    }
                }
            };
        }


    }
}
