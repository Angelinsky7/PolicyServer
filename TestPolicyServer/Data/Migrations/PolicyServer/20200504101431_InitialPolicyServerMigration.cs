﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPolicyServer.Data.Migrations.PolicyServer
{
    public partial class InitialPolicyServerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(nullable: false),
                    ClientName = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    ClientUri = table.Column<string>(maxLength: 2000, nullable: true),
                    PolicyEnforcement = table.Column<int>(nullable: false),
                    DecisionStrategy = table.Column<int>(nullable: false),
                    AnalyseModeEnabled = table.Column<bool>(nullable: false),
                    PermissionSplitter = table.Column<string>(maxLength: 2, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 200, nullable: true),
                    IconUri = table.Column<string>(maxLength: 2000, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scope",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: false),
                    IconUri = table.Column<string>(maxLength: 2000, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimePolicyRange",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePolicyRange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Secret",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<string>(maxLength: 100, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    SecretType = table.Column<string>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secret_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MmClientResource",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmClientResource", x => new { x.ClientId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_MmClientResource_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MmClientResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    DecisionStrategy = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    PermissionType = table.Column<string>(nullable: false),
                    ResouceType = table.Column<string>(nullable: true),
                    ResourceId = table.Column<Guid>(nullable: true),
                    ScopePermission_ResourceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permission_Resource_ScopePermission_ResourceId",
                        column: x => x.ScopePermission_ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceUri",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResouceId = table.Column<Guid>(nullable: false),
                    Uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceUri_Resource_ResouceId",
                        column: x => x.ResouceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MmClientRole",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmClientRole", x => new { x.ClientId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MmClientRole_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MmClientRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MmRoleRole",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmRoleRole", x => new { x.RoleId, x.ParentId });
                    table.ForeignKey(
                        name: "FK_MmRoleRole_Role_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MmRoleRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MmClientScope",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    ScopeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmClientScope", x => new { x.ClientId, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_MmClientScope_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MmClientScope_Scope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MmResourceScope",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(nullable: false),
                    ScopeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmResourceScope", x => new { x.ResourceId, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_MmResourceScope_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MmResourceScope_Scope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Logic = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    PolicyType = table.Column<string>(nullable: false),
                    DecisionStrategy = table.Column<int>(nullable: true),
                    NotBefore = table.Column<DateTime>(nullable: true),
                    NotOnOrAfter = table.Column<DateTime>(nullable: true),
                    DayOfMonthId = table.Column<long>(nullable: true),
                    MonthId = table.Column<long>(nullable: true),
                    YearId = table.Column<long>(nullable: true),
                    HourId = table.Column<long>(nullable: true),
                    MinuteId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policy_TimePolicyRange_DayOfMonthId",
                        column: x => x.DayOfMonthId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_TimePolicyRange_HourId",
                        column: x => x.HourId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_TimePolicyRange_MinuteId",
                        column: x => x.MinuteId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_TimePolicyRange_MonthId",
                        column: x => x.MonthId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_TimePolicyRange_YearId",
                        column: x => x.YearId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MmClientPermission",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmClientPermission", x => new { x.ClientId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_MmClientPermission_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MmClientPermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MmScopePermissionScope",
                columns: table => new
                {
                    ScopePermissionId = table.Column<Guid>(nullable: false),
                    ScopeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmScopePermissionScope", x => new { x.ScopePermissionId, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_MmScopePermissionScope_Scope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MmScopePermissionScope_Permission_ScopePermissionId",
                        column: x => x.ScopePermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPolicyClient",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientPolicyId = table.Column<Guid>(nullable: false),
                    Client = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPolicyClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPolicyClient_Policy_ClientPolicyId",
                        column: x => x.ClientPolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupPolicyGroup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupPolicyId = table.Column<Guid>(nullable: false),
                    Group = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPolicyGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPolicyGroup_Policy_GroupPolicyId",
                        column: x => x.GroupPolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MmAggregatedPolicyPolicy",
                columns: table => new
                {
                    AggregatedPolicyId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmAggregatedPolicyPolicy", x => new { x.AggregatedPolicyId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_MmAggregatedPolicyPolicy_Policy_AggregatedPolicyId",
                        column: x => x.AggregatedPolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MmAggregatedPolicyPolicy_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MmClientPolicy",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmClientPolicy", x => new { x.ClientId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_MmClientPolicy_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MmClientPolicy_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MmPermissionPolicy",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmPermissionPolicy", x => new { x.PermissionId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_MmPermissionPolicy_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MmPermissionPolicy_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MmRolePolicyRole",
                columns: table => new
                {
                    RolePolicyId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmRolePolicyRole", x => new { x.RolePolicyId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MmRolePolicyRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MmRolePolicyRole_Policy_RolePolicyId",
                        column: x => x.RolePolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPolicyUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserPolicyId = table.Column<Guid>(nullable: false),
                    User = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPolicyUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPolicyUser_Policy_UserPolicyId",
                        column: x => x.UserPolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientId",
                table: "Client",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientPolicyClient_ClientPolicyId",
                table: "ClientPolicyClient",
                column: "ClientPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPolicyGroup_GroupPolicyId",
                table: "GroupPolicyGroup",
                column: "GroupPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_MmAggregatedPolicyPolicy_PolicyId",
                table: "MmAggregatedPolicyPolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_MmAggregatedPolicyPolicy_AggregatedPolicyId_PolicyId",
                table: "MmAggregatedPolicyPolicy",
                columns: new[] { "AggregatedPolicyId", "PolicyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmClientPermission_PermissionId",
                table: "MmClientPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MmClientPermission_ClientId_PermissionId",
                table: "MmClientPermission",
                columns: new[] { "ClientId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmClientPolicy_PolicyId",
                table: "MmClientPolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_MmClientPolicy_ClientId_PolicyId",
                table: "MmClientPolicy",
                columns: new[] { "ClientId", "PolicyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmClientResource_ResourceId",
                table: "MmClientResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_MmClientResource_ClientId_ResourceId",
                table: "MmClientResource",
                columns: new[] { "ClientId", "ResourceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmClientRole_RoleId",
                table: "MmClientRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MmClientRole_ClientId_RoleId",
                table: "MmClientRole",
                columns: new[] { "ClientId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmClientScope_ScopeId",
                table: "MmClientScope",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_MmClientScope_ClientId_ScopeId",
                table: "MmClientScope",
                columns: new[] { "ClientId", "ScopeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmPermissionPolicy_PolicyId",
                table: "MmPermissionPolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_MmPermissionPolicy_PermissionId_PolicyId",
                table: "MmPermissionPolicy",
                columns: new[] { "PermissionId", "PolicyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmResourceScope_ScopeId",
                table: "MmResourceScope",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_MmResourceScope_ResourceId_ScopeId",
                table: "MmResourceScope",
                columns: new[] { "ResourceId", "ScopeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmRolePolicyRole_RoleId",
                table: "MmRolePolicyRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MmRolePolicyRole_RolePolicyId_RoleId",
                table: "MmRolePolicyRole",
                columns: new[] { "RolePolicyId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmRoleRole_ParentId",
                table: "MmRoleRole",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MmRoleRole_RoleId_ParentId",
                table: "MmRoleRole",
                columns: new[] { "RoleId", "ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MmScopePermissionScope_ScopeId",
                table: "MmScopePermissionScope",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_MmScopePermissionScope_ScopePermissionId_ScopeId",
                table: "MmScopePermissionScope",
                columns: new[] { "ScopePermissionId", "ScopeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ResourceId",
                table: "Permission",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ScopePermission_ResourceId",
                table: "Permission",
                column: "ScopePermission_ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_DayOfMonthId",
                table: "Policy",
                column: "DayOfMonthId",
                unique: true,
                filter: "[DayOfMonthId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_HourId",
                table: "Policy",
                column: "HourId",
                unique: true,
                filter: "[HourId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_MinuteId",
                table: "Policy",
                column: "MinuteId",
                unique: true,
                filter: "[MinuteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_MonthId",
                table: "Policy",
                column: "MonthId",
                unique: true,
                filter: "[MonthId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_YearId",
                table: "Policy",
                column: "YearId",
                unique: true,
                filter: "[YearId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_Name",
                table: "Resource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceUri_ResouceId",
                table: "ResourceUri",
                column: "ResouceId");

            migrationBuilder.CreateIndex(
                name: "IX_Secret_ClientId",
                table: "Secret",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicyUser_UserPolicyId",
                table: "UserPolicyUser",
                column: "UserPolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientPolicyClient");

            migrationBuilder.DropTable(
                name: "GroupPolicyGroup");

            migrationBuilder.DropTable(
                name: "MmAggregatedPolicyPolicy");

            migrationBuilder.DropTable(
                name: "MmClientPermission");

            migrationBuilder.DropTable(
                name: "MmClientPolicy");

            migrationBuilder.DropTable(
                name: "MmClientResource");

            migrationBuilder.DropTable(
                name: "MmClientRole");

            migrationBuilder.DropTable(
                name: "MmClientScope");

            migrationBuilder.DropTable(
                name: "MmPermissionPolicy");

            migrationBuilder.DropTable(
                name: "MmResourceScope");

            migrationBuilder.DropTable(
                name: "MmRolePolicyRole");

            migrationBuilder.DropTable(
                name: "MmRoleRole");

            migrationBuilder.DropTable(
                name: "MmScopePermissionScope");

            migrationBuilder.DropTable(
                name: "ResourceUri");

            migrationBuilder.DropTable(
                name: "Secret");

            migrationBuilder.DropTable(
                name: "UserPolicyUser");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Scope");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "TimePolicyRange");
        }
    }
}