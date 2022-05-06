using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBT.Data.Migrations
{
    public partial class changeNameofSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                newName: "UserTokens",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRoles",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                newName: "UserLogins",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                newName: "UserClaims",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roles",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "Roleclaims ",
                newName: "Roleclaims ",
                newSchema: "Security");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "Security",
                newName: "UserTokens");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Security",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "Security",
                newName: "UserLogins");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "Security",
                newName: "UserClaims");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Security",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Roleclaims ",
                schema: "Security",
                newName: "Roleclaims ");
        }
    }
}
