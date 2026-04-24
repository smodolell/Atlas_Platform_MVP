using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Atlas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Periodicidades",
                columns: new[] { "Id", "Activa", "NomPeriodicidad" },
                values: new object[,]
                {
                    { 1, true, "Diaria" },
                    { 2, true, "Semanal" },
                    { 3, true, "Mensual" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Periodicidades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Periodicidades",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Periodicidades",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
