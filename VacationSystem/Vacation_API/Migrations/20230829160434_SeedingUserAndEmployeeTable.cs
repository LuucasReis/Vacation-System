using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vacation_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingUserAndEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Name", "Occupancy", "Regime", "Salary", "entryDate", "leaveDate", "vacationDays" },
                values: new object[] { 1, "admin@gmail.com", "Admin", "PM", 0, 10100.0, new DateTime(2022, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "userName" },
                values: new object[] { 1, "admin@gmail.com", "Admin", "123", "PM", "admin@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
