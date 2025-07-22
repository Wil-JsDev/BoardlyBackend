using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardly.Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTareaEmpleadoTablaYRelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TareaEmpleado",
                columns: table => new
                {
                    FkTareaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkEmpleadoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaEmpleado", x => new { x.FkTareaId, x.FkEmpleadoId });
                    table.ForeignKey(
                        name: "FK_TareaEmpleado_Empleado_FkEmpleadoId",
                        column: x => x.FkEmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "PkEmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareaEmpleado_Tarea_FkTareaId",
                        column: x => x.FkTareaId,
                        principalTable: "Tarea",
                        principalColumn: "PkTareaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TareaEmpleado_FkEmpleadoId",
                table: "TareaEmpleado",
                column: "FkEmpleadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TareaEmpleado");
        }
    }
}
