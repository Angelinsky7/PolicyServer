using System;
using System.Collections.Generic;
using PolicyServer1.Models;

namespace TestPolicyServer {
    public static class Config {

        public static List<Scope> GetScopes() {
            List<Scope> scopes = new List<Scope> {
                new Scope{ Name = "create", DisplayName = "Create" },
                new Scope{ Name = "edit", DisplayName = "Edit" },
                new Scope{ Name = "view", DisplayName = "View" },
                new Scope{ Name = "delete", DisplayName = "Delete" }
            };

            return scopes;
        }

        public static List<Role> GetRoles() {
            List<Role> roles = new List<Role> {
                new Role{ Name = "Administrator" },
                new Role{ Name = "User" },
                new Role{ Name = "AntiRole"},
            };

            roles.Add(new Role {
                Name = "Test Parent1",
                Parents = new Role[] {
                    roles[0]
                }
            });

            roles.Add(new Role {
                Name = "Test Parent2",
                Parents = new Role[] {
                    roles[3],
                    roles[1]
                }
            });

            roles.Add(new Role {
                Name = "Test Parent3",
                Parents = new Role[] {
                    roles[4],
                    roles[2]
                }
            });

            return roles;
        }

        public static List<Policy> GetPolicies(List<Role> roles) {
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
                    NotBefore = new DateTime(2019, 1, 1),
                    NotOnOrAfter = new DateTime(2019, 10, 10)
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
                new ClientPolicy {
                    Name = "Client policy",
                    Logic = PolicyLogic.Positive,
                    Description = "This is a test of a client policy",
                    Clients = new String[] {
                        "client1",
                        "client2"
                    }
                },
                new GroupPolicy {
                    Name = "Group Policy",
                    Description = "A test to create a group policy",
                    Logic = PolicyLogic.Positive,
                    Groups = new String[] {
                        "group1",
                        "group2"
                    },
                },
                new UserPolicy {
                    Name = "User policy",
                    Description = "A test of user policy",
                    Logic = PolicyLogic.Positive,
                    Users = new String[] {
                        "user1",
                        "user2"
                    }
                },
                new TimePolicy {
                    Name = "Time Policy 2",
                    Logic = PolicyLogic.Positive,
                    NotBefore = new DateTime(2019, 1, 1),
                    NotOnOrAfter = new DateTime(2019, 10, 10),
                    Hour = new TimePolicyRange {
                        From = 14,
                        To = 20
                    },
                    Minute = new TimePolicyRange {
                        From = 10,
                        To = 50
                    }
                },
            };

            policies.Add(new AggregatedPolicy {
                Name = "Aggregate policy",
                Description = "a test of an aggreate policy",
                Logic = PolicyLogic.Positive,
                DecisionStrategy = DecisionStrategy.Unanimous,
                Policies = new Policy[] {
                    policies[5],
                    policies[6]
                }
            });

            policies.Add(new AggregatedPolicy {
                Name = "Aggregate policy 2",
                Description = "a test of an aggreate policy",
                Logic = PolicyLogic.Positive,
                DecisionStrategy = DecisionStrategy.Unanimous,
                Policies = new Policy[] {
                    policies[8],
                    policies[1]
                }
            });

            return policies;
        }

        public static List<Resource> GetResouces(List<Scope> scopes) {
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
                },
                new Resource{
                    Name = "Resource With Uris",
                    DisplayName = "Copntact",
                    Type = "urn:mvc:resources:res1",
                    Scopes = {
                        scopes[0],
                        scopes[1],
                        scopes[2],
                        scopes[3],
                    },
                    Uris = new Uri[]{
                        new Uri("http://test1.uri.ch"),
                        new Uri("http://test2.uri.ch"),
                    },
                    IconUri = new Uri("http://test3.uri.ch"),
                }
            };
            return resources;
        }

        public static IEnumerable<Client> GetClients() {
            List<Scope> scopes = GetScopes();
            List<Role> roles = GetRoles();
            List<Policy> policies = GetPolicies(roles);
            List<Resource> resources = GetResouces(scopes);

            return new Client[] {
                new Client {
                    ClientId = "test001",
                    ClientName = "Test001",
                    Secrets = {
                        new Secret("test".Sha256())
                    },
                    ClientUri = "http://test.ch",
                    Description = "a description",
                    Enabled = true,
                    Options = new ClientOption {
                        AnalyseModeEnabled = true,
                        DecisionStrategy = DecisionStrategy.Unanimous,
                        PermissionSplitter = "#",
                        PolicyEnforcement = PolicyEnforcement.Enforcing
                    },
                    RequireClientSecret = true,
                    
                    Policies = {
                        policies[0]
                    },

                    Permissions = {
                        new ScopePermission {
                            Name = "Test Scope Permision 001",
                            Description = "a description of test",
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
                    }
                },
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
                            Name = "Remove create scope",
                            Resource = resources[0],
                            DecisionStrategy = DecisionStrategy.Affirmative,
                            Scopes = {
                                scopes[0]
                            },
                            Policies = {
                                policies[2]
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
                        },
                        new ResourcePermission {
                            Name = "Resource Permission",
                            Resource = resources[2],
                            DecisionStrategy = DecisionStrategy.Affirmative,
                            Description = "test of resource permsion",
                            ResouceType = "rsc:tyoe:1",
                            Policies = {
                                policies[9]
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
