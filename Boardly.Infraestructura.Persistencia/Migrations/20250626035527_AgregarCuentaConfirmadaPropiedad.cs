using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardly.Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCuentaConfirmadaPropiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CuentaConfirmada",
                table: "Usuario",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuentaConfirmada",
                table: "Usuario");
        }
    }
}
