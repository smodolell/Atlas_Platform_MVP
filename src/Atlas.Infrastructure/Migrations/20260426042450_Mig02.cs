using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    SocioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaHoraEntrada = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    FechaHoraSalida = table.Column<DateTime>(type: "datetime2(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asistencias_Planes_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencias_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_FechaHoraEntrada",
                table: "Asistencias",
                column: "FechaHoraEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_PlanId",
                table: "Asistencias",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_Socio_FechaEntrada",
                table: "Asistencias",
                columns: new[] { "SocioId", "FechaHoraEntrada" });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_SocioId",
                table: "Asistencias",
                column: "SocioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");
        }
    }
}
