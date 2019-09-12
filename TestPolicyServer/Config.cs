using System;
using System.Collections.Generic;
using PolicyServer1.Models;

namespace TestPolicyServer {
    public static class Config {

        public static IEnumerable<Client> GetClients() {
            List<Scope> scopes = new List<Scope> {
                new Scope{ Name = "create", DisplayName = "Create" },
                new Scope{ Name = "edit", DisplayName = "Edit" },
                new Scope{ Name = "view", DisplayName = "View" },
                new Scope{ Name = "delete", DisplayName = "Delete" }
            };

            List<Role> roles = new List<Role> {
                new Role{ Name = "Administrator" },
                new Role{ Name = "User" },
                new Role{ Name = "AntiRole"},
            };

            List<Policy> policies = new List<Policy> {
                new RolePolicy {
                    Name = "Activite Adminisator Policy",
                    Logic = PolicyLogic.Positive,
                    Roles = {
                        roles[0]
                    }
                },
                new TimePolicy {
                    Name = "Time Policy",
                    Logic = PolicyLogic.Positive,
                    NotBefore = DateTime.Now.AddMinutes(-10)
                },
                new RolePolicy {
                    Name = "Anti Adminisator Policy",
                    Logic = PolicyLogic.Negative,
                    Roles = {
                        roles[0]
                    }
                },
                new RolePolicy {
                    Name = "User Policy",
                    Logic = PolicyLogic.Positive,
                    Roles = {
                        roles[1]
                    }
                },
            };

            List<Resource> resources = new List<Resource> {
                new Resource{
                    Name = "Activite",
                    DisplayName = "Activié",
                    Type = "urn:mvc:resources:res1",
                    Scopes = {
                        scopes[0],
                        scopes[1],
                        scopes[2],
                    }
                },
                new Resource{
                    Name = "Contactes",
                    DisplayName = "Copntact",
                    Type = "urn:mvc:resources:res1",
                    Scopes = {
                        scopes[0],
                        scopes[1],
                        scopes[2],
                        scopes[3],
                    }
                }
            };

            return new Client[] {
                new Client {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    Secrets = {
                        new Secret("secret".Sha256())
                    },

                    Options = new ClientOption {
                        PolicyEnforcement = PolicyEnforcement.Enforcing,
                        DecisionStrategy = DecisionStrategy.Unanimous,
                        AnalyseModeEnabled = true
                    },

                    Scopes = scopes,
                    Resources = resources,
                    Roles = roles,
                    Policies = policies,

                    Permissions = {
                        new ScopePermission {
                            Name = "Activite Administrator Permission",
                            Resource = resources[0],
                            DecisionStrategy = DecisionStrategy.Unanimous,
                            Scopes = {
                                scopes[0],
                                scopes[1],
                            },
                            Policies = {
                                policies[0]
                            }
                        },
                        new ScopePermission {
                            Name = "Contact Time Administrator Permission 1",
                            Resource = resources[1],
                            DecisionStrategy = DecisionStrategy.Unanimous,
                            Scopes = {
                                scopes[0],
                                scopes[1],
                                scopes[3]
                            },
                            Policies = {
                                policies[0],
                                policies[1]
                            }
                        },
                        new ScopePermission {
                            Name = "Contact Administrator Permission 2",
                            Resource = resources[1],
                            DecisionStrategy = DecisionStrategy.Affirmative,
                            Scopes = {
                                scopes[0],
                                scopes[1],
                                scopes[3]
                            },
                            Policies = {
                                policies[0],
                                policies[1]
                            }
                        },
                        new ScopePermission {
                            Name = "Contact Administrator Permission 3",
                            Resource = resources[1],
                            DecisionStrategy = DecisionStrategy.Consensus,
                            Scopes = {
                                scopes[0],
                                scopes[1],
                                scopes[3]
                            },
                            Policies = {
                                policies[0],
                                policies[1]
                            }
                        },
                        new ScopePermission {
                            Name = "Contact Administrator Permission",
                            Resource = resources[1],
                            DecisionStrategy = DecisionStrategy.Unanimous,
                            Scopes = {
                                scopes[0],
                            },
                            Policies = {
                                policies[2]
                            }
                        },
                        new ScopePermission {
                            Name = "Contact User Permission",
                            Resource = resources[1],
                            DecisionStrategy = DecisionStrategy.Unanimous,
                            Scopes = {
                                scopes[2],
                            },
                            Policies = {
                                policies[3]
                            }
                        }
                    }

                    //Policy = new Policy {
                    //    Roles = {
                    //        new Role {
                    //            Name = "TestRole1",
                    //            Subjects = {
                    //                "inner",
                    //                "outer",
                    //                "0564eba0-4bd6-4dd4-9b03-8190aed55a30"
                    //            },
                    //            Permissions = {
                    //                new Permission{ Name= "Activite.Select" },
                    //                new Permission{ Name= "Activite.Add" },
                    //                new Permission{ Name= "Activite.Edit" },
                    //                new Permission{ Name= "Activite.Delete" },
                    //            },
                    //            Parents = {
                    //                new Role {
                    //                    Name = "Parent",
                    //                    Permissions = {
                    //                        new Permission{ Name = "Notification.See.Own" },
                    //                        new Permission{ Name = "Notification.Send", IsRevoked = true }
                    //                    },
                    //                    Parents = {
                    //                        new Role {
                    //                            Name = "Grand-Parent",
                    //                            Permissions = {
                    //                                new Permission{ Name = "Notification.See" },
                    //                                new Permission{ Name = "Notification.Send" },
                    //                                new Permission{ Name= "CanDoDoctorThings"}
                    //                            },
                    //                            //Parents = {
                    //                            //    new Role{ Name = "TestRole1" }
                    //                            //}
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        },
                    //        new Role {
                    //            Name = "TestRole2",
                    //            Subjects = {
                    //                "0564eba0-4bd6-4dd4-9b03-8190aed55a30"
                    //            },
                    //            Permissions = {
                    //                new Permission{ Name= "Activite.Delete", IsRevoked = true }
                    //            }
                    //        },
                    //        new Role {
                    //            Name = "TestRole2",
                    //            Subjects = {
                    //                "inner"
                    //            },
                    //            Permissions = {
                    //                new Permission{ Name= "Vehicule.Select" },
                    //                new Permission{ Name= "Vehicule.Add" },
                    //                new Permission{ Name= "Vehicule.Edit" },
                    //                new Permission{ Name= "Vehicule.Delete" }
                    //            }
                    //        },
                    //        new Role {
                    //            Name = "MonRole",
                    //            IdentityRoles = {
                    //                "LdapGroupe1"
                    //            }
                    //        }
                    //    }
                    //}
                }
            };
        }


    }
}
