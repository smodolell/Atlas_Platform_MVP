using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlanSesionId",
                table: "Asistencias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanesHorario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesHorario", x => x.Id);
                    table.CheckConstraint("CK_PlanHorario_HoraFin", "HoraFin > HoraInicio");
                    table.ForeignKey(
                        name: "FK_PlanesHorario_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanesHorario_Planes_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanesSesion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    PlanHorarioId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesSesion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanesSesion_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanesSesion_PlanesHorario_PlanHorarioId",
                        column: x => x.PlanHorarioId,
                        principalTable: "PlanesHorario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanesSesion_Planes_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_PlanSesionId",
                table: "Asistencias",
                column: "PlanSesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_NombreApellido",
                table: "Empleados",
                columns: new[] { "Nombre", "Apellido" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanHorario_EmpleadoId",
                table: "PlanesHorario",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanHorario_PlanEmpleadoDia",
                table: "PlanesHorario",
                columns: new[] { "PlanId", "EmpleadoId", "DiaSemana" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanSesion_EmpleadoId",
                table: "PlanesSesion",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanSesion_Estado",
                table: "PlanesSesion",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_PlanSesion_FechaEstadoPlan",
                table: "PlanesSesion",
                columns: new[] { "Fecha", "Estado", "PlanId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanSesion_PlanFecha",
                table: "PlanesSesion",
                columns: new[] { "PlanId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanSesion_PlanHorarioId",
                table: "PlanesSesion",
                column: "PlanHorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_PlanesSesion_PlanSesionId",
                table: "Asistencias",
                column: "PlanSesionId",
                principalTable: "PlanesSesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_PlanesSesion_PlanSesionId",
                table: "Asistencias");

            migrationBuilder.DropTable(
                name: "PlanesSesion");

            migrationBuilder.DropTable(
                name: "PlanesHorario");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Asistencias_PlanSesionId",
                table: "Asistencias");

            migrationBuilder.DropColumn(
                name: "PlanSesionId",
                table: "Asistencias");
        }
    }
}
