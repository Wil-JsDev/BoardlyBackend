using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardly.Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionActividadProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FkProyectoId",
                table: "Actividad",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_FkProyectoId",
                table: "Actividad",
                column: "FkProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividad_Proyecto_FkProyectoId",
                table: "Actividad",
                column: "FkProyectoId",
                principalTable: "Proyecto",
                principalColumn: "PkProyectoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividad_Proyecto_FkProyectoId",
                table: "Actividad");

            migrationBuilder.DropIndex(
                name: "IX_Actividad_FkProyectoId",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "FkProyectoId",
                table: "Actividad");
        }
    }
}
