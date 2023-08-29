using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vacation_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingTablesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    entryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Occupancy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Regime = table.Column<int>(type: "int", nullable: false),
                    leaveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    vacationDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistVacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startVacationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    finishVacationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    Occupancy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistVacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistVacations_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "myDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupancy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EligibleDays = table.Column<int>(type: "int", nullable: false),
                    restantDays = table.Column<double>(type: "float", nullable: false),
                    combinedDays = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_myDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_myDetails_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistVacations_employeeId",
                table: "HistVacations",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_myDetails_employeeId",
                table: "myDetails",
                column: "employeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistVacations");

            migrationBuilder.DropTable(
                name: "myDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
