using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPolicyServer.Data.Migrations.PolicyServer._001
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
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    DecisionStrategy = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Logic = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
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
                    Description = table.Column<string>(maxLength: 200, nullable: false),
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
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secret", x => x.Id);
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
                name: "ClientPermission",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPermission", x => new { x.ClientId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ClientPermission_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientPermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AggregatedPolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DecisionStrategy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregatedPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AggregatedPolicy_Policy_Id",
                        column: x => x.Id,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPolicy",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPolicy", x => new { x.ClientId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_ClientPolicy_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientPolicy_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientPolicy_NameMissing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPolicy_NameMissing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPolicy_NameMissing_Policy_Id",
                        column: x => x.Id,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupPolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPolicy_Policy_Id",
                        column: x => x.Id,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPolicy",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionPolicy", x => new { x.PermissionId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_PermissionPolicy_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionPolicy_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePolicy_Policy_Id",
                        column: x => x.Id,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPolicy_Policy_Id",
                        column: x => x.Id,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientResource",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientResource", x => new { x.ClientId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_ClientResource_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourcePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResouceType = table.Column<string>(maxLength: 200, nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourcePermission_Permission_Id",
                        column: x => x.Id,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourcePermission_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ScopePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScopePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScopePermission_Permission_Id",
                        column: x => x.Id,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScopePermission_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRole",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRole", x => new { x.ClientId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ClientRole_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleRole",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRole", x => new { x.RoleId, x.ParentId });
                    table.ForeignKey(
                        name: "FK_RoleRole_Role_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientScope",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    ScopeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScope", x => new { x.ClientId, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_ClientScope_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientScope_Scope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceScope",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(nullable: false),
                    ScopeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceScope", x => new { x.ResourceId, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_ResourceScope_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceScope_Scope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientSecret",
                columns: table => new
                {
                    SecretId = table.Column<long>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecret", x => new { x.ClientId, x.SecretId });
                    table.ForeignKey(
                        name: "FK_ClientSecret_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientSecret_Secret_SecretId",
                        column: x => x.SecretId,
                        principalTable: "Secret",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimePolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NotBefore = table.Column<DateTime>(nullable: true),
                    NotOnOrAfter = table.Column<DateTime>(nullable: true),
                    DayOfMonthId = table.Column<long>(nullable: true),
                    MonthId = table.Column<long>(nullable: true),
                    YearId = table.Column<long>(nullable: true),
                    HourId = table.Column<long>(nullable: true),
                    MinuteId = table.Column<long>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimePolicy_TimePolicyRange_DayOfMonthId",
                        column: x => x.DayOfMonthId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimePolicy_TimePolicyRange_HourId",
                        column: x => x.HourId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimePolicy_Policy_Id",
                        column: x => x.Id,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimePolicy_TimePolicyRange_MinuteId",
                        column: x => x.MinuteId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimePolicy_TimePolicyRange_MonthId",
                        column: x => x.MonthId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimePolicy_TimePolicyRange_YearId",
                        column: x => x.YearId,
                        principalTable: "TimePolicyRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AggregatedPolicyPolicy",
                columns: table => new
                {
                    AggregatedPolicyId = table.Column<Guid>(nullable: false),
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregatedPolicyPolicy", x => new { x.AggregatedPolicyId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_AggregatedPolicyPolicy_AggregatedPolicy_AggregatedPolicyId",
                        column: x => x.AggregatedPolicyId,
                        principalTable: "AggregatedPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AggregatedPolicyPolicy_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientPolicy_NameMissingClient",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientPolicyId = table.Column<Guid>(nullable: false),
                    Client = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPolicy_NameMissingClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPolicy_NameMissingClient_ClientPolicy_NameMissing_ClientPolicyId",
                        column: x => x.ClientPolicyId,
                        principalTable: "ClientPolicy_NameMissing",
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
                        name: "FK_GroupPolicyGroup_GroupPolicy_GroupPolicyId",
                        column: x => x.GroupPolicyId,
                        principalTable: "GroupPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePolicyRole",
                columns: table => new
                {
                    RolePolicyId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePolicyRole", x => new { x.RolePolicyId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePolicyRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePolicyRole_RolePolicy_RolePolicyId",
                        column: x => x.RolePolicyId,
                        principalTable: "RolePolicy",
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
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPolicyUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPolicyUser_UserPolicy_UserPolicyId",
                        column: x => x.UserPolicyId,
                        principalTable: "UserPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScopePermissionScope",
                columns: table => new
                {
                    ScopePermissionId = table.Column<Guid>(nullable: false),
                    ScopeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScopePermissionScope", x => new { x.ScopePermissionId, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_ScopePermissionScope_Scope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScopePermissionScope_ScopePermission_ScopePermissionId",
                        column: x => x.ScopePermissionId,
                        principalTable: "ScopePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AggregatedPolicyPolicy_PolicyId",
                table: "AggregatedPolicyPolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_AggregatedPolicyPolicy_AggregatedPolicyId_PolicyId",
                table: "AggregatedPolicyPolicy",
                columns: new[] { "AggregatedPolicyId", "PolicyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientId",
                table: "Client",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientPermission_PermissionId",
                table: "ClientPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPermission_ClientId_PermissionId",
                table: "ClientPermission",
                columns: new[] { "ClientId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientPolicy_PolicyId",
                table: "ClientPolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPolicy_ClientId_PolicyId",
                table: "ClientPolicy",
                columns: new[] { "ClientId", "PolicyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientPolicy_NameMissingClient_ClientPolicyId",
                table: "ClientPolicy_NameMissingClient",
                column: "ClientPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientResource_ResourceId",
                table: "ClientResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientResource_ClientId_ResourceId",
                table: "ClientResource",
                columns: new[] { "ClientId", "ResourceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientRole_RoleId",
                table: "ClientRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRole_ClientId_RoleId",
                table: "ClientRole",
                columns: new[] { "ClientId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientScope_ScopeId",
                table: "ClientScope",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientScope_ClientId_ScopeId",
                table: "ClientScope",
                columns: new[] { "ClientId", "ScopeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientSecret_SecretId",
                table: "ClientSecret",
                column: "SecretId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSecret_ClientId_SecretId",
                table: "ClientSecret",
                columns: new[] { "ClientId", "SecretId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPolicyGroup_GroupPolicyId",
                table: "GroupPolicyGroup",
                column: "GroupPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicy_PolicyId",
                table: "PermissionPolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicy_PermissionId_PolicyId",
                table: "PermissionPolicy",
                columns: new[] { "PermissionId", "PolicyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resource_Name",
                table: "Resource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourcePermission_ResourceId",
                table: "ResourcePermission",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceScope_ScopeId",
                table: "ResourceScope",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceScope_ResourceId_ScopeId",
                table: "ResourceScope",
                columns: new[] { "ResourceId", "ScopeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceUri_ResouceId",
                table: "ResourceUri",
                column: "ResouceId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePolicyRole_RoleId",
                table: "RolePolicyRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePolicyRole_RolePolicyId_RoleId",
                table: "RolePolicyRole",
                columns: new[] { "RolePolicyId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleRole_ParentId",
                table: "RoleRole",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRole_RoleId_ParentId",
                table: "RoleRole",
                columns: new[] { "RoleId", "ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScopePermission_ResourceId",
                table: "ScopePermission",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScopePermissionScope_ScopeId",
                table: "ScopePermissionScope",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScopePermissionScope_ScopePermissionId_ScopeId",
                table: "ScopePermissionScope",
                columns: new[] { "ScopePermissionId", "ScopeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimePolicy_DayOfMonthId",
                table: "TimePolicy",
                column: "DayOfMonthId",
                unique: true,
                filter: "[DayOfMonthId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimePolicy_HourId",
                table: "TimePolicy",
                column: "HourId",
                unique: true,
                filter: "[HourId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimePolicy_MinuteId",
                table: "TimePolicy",
                column: "MinuteId",
                unique: true,
                filter: "[MinuteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimePolicy_MonthId",
                table: "TimePolicy",
                column: "MonthId",
                unique: true,
                filter: "[MonthId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimePolicy_YearId",
                table: "TimePolicy",
                column: "YearId",
                unique: true,
                filter: "[YearId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicyUser_UserPolicyId",
                table: "UserPolicyUser",
                column: "UserPolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AggregatedPolicyPolicy");

            migrationBuilder.DropTable(
                name: "ClientPermission");

            migrationBuilder.DropTable(
                name: "ClientPolicy");

            migrationBuilder.DropTable(
                name: "ClientPolicy_NameMissingClient");

            migrationBuilder.DropTable(
                name: "ClientResource");

            migrationBuilder.DropTable(
                name: "ClientRole");

            migrationBuilder.DropTable(
                name: "ClientScope");

            migrationBuilder.DropTable(
                name: "ClientSecret");

            migrationBuilder.DropTable(
                name: "GroupPolicyGroup");

            migrationBuilder.DropTable(
                name: "PermissionPolicy");

            migrationBuilder.DropTable(
                name: "ResourcePermission");

            migrationBuilder.DropTable(
                name: "ResourceScope");

            migrationBuilder.DropTable(
                name: "ResourceUri");

            migrationBuilder.DropTable(
                name: "RolePolicyRole");

            migrationBuilder.DropTable(
                name: "RoleRole");

            migrationBuilder.DropTable(
                name: "ScopePermissionScope");

            migrationBuilder.DropTable(
                name: "TimePolicy");

            migrationBuilder.DropTable(
                name: "UserPolicyUser");

            migrationBuilder.DropTable(
                name: "AggregatedPolicy");

            migrationBuilder.DropTable(
                name: "ClientPolicy_NameMissing");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Secret");

            migrationBuilder.DropTable(
                name: "GroupPolicy");

            migrationBuilder.DropTable(
                name: "RolePolicy");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Scope");

            migrationBuilder.DropTable(
                name: "ScopePermission");

            migrationBuilder.DropTable(
                name: "TimePolicyRange");

            migrationBuilder.DropTable(
                name: "UserPolicy");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Policy");
        }
    }
}
