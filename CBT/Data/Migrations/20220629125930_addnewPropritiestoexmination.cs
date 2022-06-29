using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBT.Data.Migrations
{
    public partial class addnewPropritiestoexmination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MCH",
                table: "Eximinations",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MCHC",
                table: "Eximinations",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "hemoglobin",
                table: "Eximinations",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MCH",
                table: "Eximinations");

            migrationBuilder.DropColumn(
                name: "MCHC",
                table: "Eximinations");

            migrationBuilder.DropColumn(
                name: "hemoglobin",
                table: "Eximinations");
        }
    }
}
