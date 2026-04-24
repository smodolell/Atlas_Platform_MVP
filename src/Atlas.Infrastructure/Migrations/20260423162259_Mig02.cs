using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Atlas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Periodicidades_PeriodicidadId1",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_PeriodicidadId1",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PeriodicidadId1",
                table: "Productos");

            migrationBuilder.CreateTable(
                name: "AccessPointType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AccessPointTypeName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessPointType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ApplicationName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    Name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_AccessPoint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessPointTypeId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    AccessPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescPageElement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_AccessPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_AccessPoint_AccessPointType_AccessPointTypeId",
                        column: x => x.AccessPointTypeId,
                        principalTable: "AccessPointType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYS_AccessPoint_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYS_AccessPoint_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolAccessPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    AccessPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolAccessPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolAccessPoints_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolAccessPoints_SYS_AccessPoint_AccessPointId",
                        column: x => x.AccessPointId,
                        principalTable: "SYS_AccessPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccessPointType",
                columns: new[] { "Id", "AccessPointTypeName" },
                values: new object[,]
                {
                    { 0, "LeftMenu" },
                    { 1, "Page" },
                    { 2, "Element" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPointTypes_AccessPointTypeName",
                table: "AccessPointType",
                column: "AccessPointTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationName",
                table: "Applications",
                column: "ApplicationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Name",
                table: "Menus",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RolAccessPoints_AccessPointId",
                table: "RolAccessPoints",
                column: "AccessPointId");

            migrationBuilder.CreateIndex(
                name: "IX_RolAccessPoints_RolId_AccessPointId",
                table: "RolAccessPoints",
                columns: new[] { "RolId", "AccessPointId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPoint_AccessPointTypeId",
                table: "SYS_AccessPoint",
                column: "AccessPointTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPoint_ApplicationId",
                table: "SYS_AccessPoint",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_AccessPoint_MenuId",
                table: "SYS_AccessPoint",
                column: "MenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolAccessPoints");

            migrationBuilder.DropTable(
                name: "SYS_AccessPoint");

            migrationBuilder.DropTable(
                name: "AccessPointType");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.AddColumn<int>(
                name: "PeriodicidadId1",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_PeriodicidadId1",
                table: "Productos",
                column: "PeriodicidadId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Periodicidades_PeriodicidadId1",
                table: "Productos",
                column: "PeriodicidadId1",
                principalTable: "Periodicidades",
                principalColumn: "Id");
        }
    }
}
