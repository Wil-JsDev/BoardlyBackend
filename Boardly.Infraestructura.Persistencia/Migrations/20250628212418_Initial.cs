using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardly.Infraestructura.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:actividad_prioridad", "baja,media,alta")
                .Annotation("Npgsql:Enum:estado_actividad", "pendiente,proceso,revision,completado")
                .Annotation("Npgsql:Enum:estado_empresa", "activo,inactivo")
                .Annotation("Npgsql:Enum:estado_proyecto", "en_proceso,finalizado")
                .Annotation("Npgsql:Enum:estado_tarea", "pendiente,en_proceso,en_revision,finalizada")
                .Annotation("Npgsql:Enum:estado_usuario", "activo,inactivo");

            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    PkActividadId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Prioridad = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKActividadId", x => x.PkActividadId);
                });

            migrationBuilder.CreateTable(
                name: "RolProyecto",
                columns: table => new
                {
                    PkRolProyectoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKRolProyectoId", x => x.PkRolProyectoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    PkUsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    NombreUsuario = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Contrasena = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CuentaConfirmada = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FotoPerfil = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKUsuarioId", x => x.PkUsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "ActividadDependencia",
                columns: table => new
                {
                    FkActividadId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkDependeDeActividadId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadDependencia", x => new { x.FkActividadId, x.FkDependeDeActividadId });
                    table.ForeignKey(
                        name: "FK_ActividadDependencia_Actividad_FkActividadId",
                        column: x => x.FkActividadId,
                        principalTable: "Actividad",
                        principalColumn: "PkActividadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActividadDependencia_Actividad_FkDependeDeActividadId",
                        column: x => x.FkDependeDeActividadId,
                        principalTable: "Actividad",
                        principalColumn: "PkActividadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ceo",
                columns: table => new
                {
                    PkCeoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkUsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKCeoId", x => x.PkCeoId);
                    table.ForeignKey(
                        name: "FK_Ceo_Usuario_FkUsuarioId",
                        column: x => x.FkUsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "PkUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Codigo",
                columns: table => new
                {
                    PkCodigoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkUsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Valor = table.Column<string>(type: "text", nullable: false),
                    Expiracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Creado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Usado = table.Column<bool>(type: "boolean", nullable: true),
                    Revocado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKCodigoId", x => x.PkCodigoId);
                    table.ForeignKey(
                        name: "FK_Codigo_Usuario_FkUsuarioId",
                        column: x => x.FkUsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "PkUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    PkEmpleadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkUsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Roles = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKEmpleadoId", x => x.PkEmpleadoId);
                    table.ForeignKey(
                        name: "FK_Empleado_Usuario_FkUsuarioId",
                        column: x => x.FkUsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "PkUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    PkEmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkEmpleadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkCeoId = table.Column<Guid>(type: "uuid", nullable: true),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKEmpresaId", x => x.PkEmpresaId);
                    table.ForeignKey(
                        name: "FK_Empresa_Ceo_FkCeoId",
                        column: x => x.FkCeoId,
                        principalTable: "Ceo",
                        principalColumn: "PkCeoId");
                    table.ForeignKey(
                        name: "FK_Empresa_Empleado_FkEmpleadoId",
                        column: x => x.FkEmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "PkEmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    PkProyectoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FkEmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKProyectoId", x => x.PkProyectoId);
                    table.ForeignKey(
                        name: "FK_Proyecto_Empresa_FkEmpresaId",
                        column: x => x.FkEmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "PkEmpresaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoProyectoRol",
                columns: table => new
                {
                    FkEmpleadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkProyectoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkRolProyectoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoProyectoRol", x => new { x.FkEmpleadoId, x.FkProyectoId });
                    table.ForeignKey(
                        name: "FK_EmpleadoProyectoRol_Empleado_FkEmpleadoId",
                        column: x => x.FkEmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "PkEmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoProyectoRol_Proyecto_FkProyectoId",
                        column: x => x.FkProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "PkProyectoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoProyectoRol_RolProyecto_FkRolProyectoId",
                        column: x => x.FkRolProyectoId,
                        principalTable: "RolProyecto",
                        principalColumn: "PkRolProyectoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarea",
                columns: table => new
                {
                    PkTareaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkProyectoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaCompletada = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FkActividadId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKTareaId", x => x.PkTareaId);
                    table.ForeignKey(
                        name: "FK_Tarea_Actividad_FkActividadId",
                        column: x => x.FkActividadId,
                        principalTable: "Actividad",
                        principalColumn: "PkActividadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarea_Proyecto_FkProyectoId",
                        column: x => x.FkProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "PkProyectoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    PkComentarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkTareaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    Adjunto = table.Column<string>(type: "text", nullable: true),
                    FkUsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKComentarioId", x => x.PkComentarioId);
                    table.ForeignKey(
                        name: "FK_Comentario_Tarea_FkTareaId",
                        column: x => x.FkTareaId,
                        principalTable: "Tarea",
                        principalColumn: "PkTareaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentario_Usuario_FkUsuarioId",
                        column: x => x.FkUsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "PkUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    PkNotificacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkUsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkTareaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    Leida = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKNotificacionId", x => x.PkNotificacionId);
                    table.ForeignKey(
                        name: "FK_Notificacion_Tarea_FkTareaId",
                        column: x => x.FkTareaId,
                        principalTable: "Tarea",
                        principalColumn: "PkTareaId");
                    table.ForeignKey(
                        name: "FK_Notificacion_Usuario_FkUsuarioId",
                        column: x => x.FkUsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "PkUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TareaDependencia",
                columns: table => new
                {
                    FkTareaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkDependeDeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaDependencia", x => new { x.FkTareaId, x.FkDependeDeId });
                    table.ForeignKey(
                        name: "FK_TareaDependencia_Tarea_FkDependeDeId",
                        column: x => x.FkDependeDeId,
                        principalTable: "Tarea",
                        principalColumn: "PkTareaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareaDependencia_Tarea_FkTareaId",
                        column: x => x.FkTareaId,
                        principalTable: "Tarea",
                        principalColumn: "PkTareaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TareaUsuario",
                columns: table => new
                {
                    FkTareaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FkUsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaUsuario", x => new { x.FkTareaId, x.FkUsuarioId });
                    table.ForeignKey(
                        name: "FK_TareaUsuario_Tarea_FkTareaId",
                        column: x => x.FkTareaId,
                        principalTable: "Tarea",
                        principalColumn: "PkTareaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareaUsuario_Usuario_FkUsuarioId",
                        column: x => x.FkUsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "PkUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActividadDependencia_FkDependeDeActividadId",
                table: "ActividadDependencia",
                column: "FkDependeDeActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_Ceo_FkUsuarioId",
                table: "Ceo",
                column: "FkUsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Codigo_FkUsuarioId",
                table: "Codigo",
                column: "FkUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_FkTareaId",
                table: "Comentario",
                column: "FkTareaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_FkUsuarioId",
                table: "Comentario",
                column: "FkUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_FkUsuarioId",
                table: "Empleado",
                column: "FkUsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProyectoRol_FkProyectoId",
                table: "EmpleadoProyectoRol",
                column: "FkProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProyectoRol_FkRolProyectoId",
                table: "EmpleadoProyectoRol",
                column: "FkRolProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_FkCeoId",
                table: "Empresa",
                column: "FkCeoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_FkEmpleadoId",
                table: "Empresa",
                column: "FkEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_FkTareaId",
                table: "Notificacion",
                column: "FkTareaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_FkUsuarioId",
                table: "Notificacion",
                column: "FkUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_FkEmpresaId",
                table: "Proyecto",
                column: "FkEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_FkActividadId",
                table: "Tarea",
                column: "FkActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_FkProyectoId",
                table: "Tarea",
                column: "FkProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_TareaDependencia_FkDependeDeId",
                table: "TareaDependencia",
                column: "FkDependeDeId");

            migrationBuilder.CreateIndex(
                name: "IX_TareaUsuario_FkUsuarioId",
                table: "TareaUsuario",
                column: "FkUsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActividadDependencia");

            migrationBuilder.DropTable(
                name: "Codigo");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "EmpleadoProyectoRol");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "TareaDependencia");

            migrationBuilder.DropTable(
                name: "TareaUsuario");

            migrationBuilder.DropTable(
                name: "RolProyecto");

            migrationBuilder.DropTable(
                name: "Tarea");

            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Ceo");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
