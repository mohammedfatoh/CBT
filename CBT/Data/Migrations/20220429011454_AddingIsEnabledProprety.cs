using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBT.Data.Migrations
{
    public partial class AddingIsEnabledProprety : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                schema: "Security",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                schema: "Security",
                table: "Users");
        }
    }
}
