using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardly.Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeCompanyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Empleado_FkEmpleadoId",
                table: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_FkEmpleadoId",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "FkEmpleadoId",
                table: "Empresa");

            migrationBuilder.AddColumn<Guid>(
                name: "FkEmpresaId",
                table: "Empleado",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_FkEmpresaId",
                table: "Empleado",
                column: "FkEmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleado_Empresa_FkEmpresaId",
                table: "Empleado",
                column: "FkEmpresaId",
                principalTable: "Empresa",
                principalColumn: "PkEmpresaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleado_Empresa_FkEmpresaId",
                table: "Empleado");

            migrationBuilder.DropIndex(
                name: "IX_Empleado_FkEmpresaId",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "FkEmpresaId",
                table: "Empleado");

            migrationBuilder.AddColumn<Guid>(
                name: "FkEmpleadoId",
                table: "Empresa",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_FkEmpleadoId",
                table: "Empresa",
                column: "FkEmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Empleado_FkEmpleadoId",
                table: "Empresa",
                column: "FkEmpleadoId",
                principalTable: "Empleado",
                principalColumn: "PkEmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
