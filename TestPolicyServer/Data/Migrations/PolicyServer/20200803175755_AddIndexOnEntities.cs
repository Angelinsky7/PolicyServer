using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPolicyServer.Data.Migrations.PolicyServer
{
    public partial class AddIndexOnEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_Scope_Name",
                table: "Scope",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Name",
                table: "Permission",
                column: "Name",
                unique: true);

            migrationBuilder.AlterColumn<string>(
                name: "PolicyType",
                table: "Policy",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_Name_PolicyType",
                table: "Policy",
                columns: new[] { "Name", "PolicyType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Policy_Name_PolicyType",
                table: "Policy");

            migrationBuilder.AlterColumn<string>(
                name: "PolicyType",
                table: "Policy",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.DropIndex(
                name: "IX_Scope_Name",
                table: "Scope");

            migrationBuilder.DropIndex(
                name: "IX_Role_Name",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Permission_Name",
                table: "Permission");

        }
    }
}
