using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBT.Data.Migrations
{
    public partial class Fatoh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Patient",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_doctor_id",
                table: "Patient",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserId",
                table: "Patient",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Eximination_patient_Id",
                table: "Eximination",
                column: "patient_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eximination_Patient_patient_Id",
                table: "Eximination",
                column: "patient_Id",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Doctor_doctor_id",
                table: "Patient",
                column: "doctor_id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Users_UserId",
                table: "Patient",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eximination_Patient_patient_Id",
                table: "Eximination");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Doctor_doctor_id",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Users_UserId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_doctor_id",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_UserId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Eximination_patient_Id",
                table: "Eximination");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patient");
        }
    }
}
