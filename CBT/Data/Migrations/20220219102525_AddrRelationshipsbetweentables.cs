using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBT.Data.Migrations
{
    public partial class AddrRelationshipsbetweentables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gendre = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gendre = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    doctor_id = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Eximination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RBCS = table.Column<float>(type: "real", nullable: false),
                    PLT = table.Column<float>(type: "real", nullable: false),
                    WBES = table.Column<float>(type: "real", nullable: false),
                    Result = table.Column<float>(type: "real", nullable: false),
                    Createddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    patient_Id = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eximination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eximination_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EximinationTreatments",
                columns: table => new
                {
                    EximinationsId = table.Column<int>(type: "int", nullable: false),
                    TreatmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EximinationTreatments", x => new { x.EximinationsId, x.TreatmentsId });
                    table.ForeignKey(
                        name: "FK_EximinationTreatments_Eximination_EximinationsId",
                        column: x => x.EximinationsId,
                        principalTable: "Eximination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EximinationTreatments_Treatment_TreatmentsId",
                        column: x => x.TreatmentsId,
                        principalTable: "Treatment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eximination_PatientId",
                table: "Eximination",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_EximinationTreatments_TreatmentsId",
                table: "EximinationTreatments",
                column: "TreatmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DoctorId",
                table: "Patient",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EximinationTreatments");

            migrationBuilder.DropTable(
                name: "Eximination");

            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
