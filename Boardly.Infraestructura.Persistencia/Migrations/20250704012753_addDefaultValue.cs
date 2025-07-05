using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardly.Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:actividad_prioridad", "baja,media,alta")
                .OldAnnotation("Npgsql:Enum:estado_actividad", "pendiente,proceso,revision,completado")
                .OldAnnotation("Npgsql:Enum:estado_empresa", "activo,inactivo")
                .OldAnnotation("Npgsql:Enum:estado_proyecto", "en_proceso,finalizado")
                .OldAnnotation("Npgsql:Enum:estado_tarea", "pendiente,en_proceso,en_revision,finalizada")
                .OldAnnotation("Npgsql:Enum:estado_usuario", "activo,inactivo");

            migrationBuilder.AlterColumn<bool>(
                name: "Usado",
                table: "Codigo",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:actividad_prioridad", "baja,media,alta")
                .Annotation("Npgsql:Enum:estado_actividad", "pendiente,proceso,revision,completado")
                .Annotation("Npgsql:Enum:estado_empresa", "activo,inactivo")
                .Annotation("Npgsql:Enum:estado_proyecto", "en_proceso,finalizado")
                .Annotation("Npgsql:Enum:estado_tarea", "pendiente,en_proceso,en_revision,finalizada")
                .Annotation("Npgsql:Enum:estado_usuario", "activo,inactivo");

            migrationBuilder.AlterColumn<bool>(
                name: "Usado",
                table: "Codigo",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
