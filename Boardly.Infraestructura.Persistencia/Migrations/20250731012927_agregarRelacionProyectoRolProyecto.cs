using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardly.Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class agregarRelacionProyectoRolProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FkProyectoId",
                table: "RolProyecto",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RolProyecto_FkProyectoId",
                table: "RolProyecto",
                column: "FkProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolProyecto_Proyecto_FkProyectoId",
                table: "RolProyecto",
                column: "FkProyectoId",
                principalTable: "Proyecto",
                principalColumn: "PkProyectoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolProyecto_Proyecto_FkProyectoId",
                table: "RolProyecto");

            migrationBuilder.DropIndex(
                name: "IX_RolProyecto_FkProyectoId",
                table: "RolProyecto");

            migrationBuilder.DropColumn(
                name: "FkProyectoId",
                table: "RolProyecto");
        }
    }
}
