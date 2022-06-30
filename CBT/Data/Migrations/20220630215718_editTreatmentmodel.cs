using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBT.Data.Migrations
{
    public partial class editTreatmentmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Eximinations_eximinationId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_eximinationId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Exmination_id",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "eximinationId",
                table: "Treatments");

            migrationBuilder.RenameColumn(
                name: "patient_Id",
                table: "Treatments",
                newName: "orderOfCancer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "orderOfCancer",
                table: "Treatments",
                newName: "patient_Id");

            migrationBuilder.AddColumn<int>(
                name: "Exmination_id",
                table: "Treatments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "eximinationId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_eximinationId",
                table: "Treatments",
                column: "eximinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Eximinations_eximinationId",
                table: "Treatments",
                column: "eximinationId",
                principalTable: "Eximinations",
                principalColumn: "Id");
        }
    }
}
