using Boardly.Dominio.Enum;
using Boardly.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Contexto;

public class BoardlyContexto: DbContext
{
    public BoardlyContexto(DbContextOptions<BoardlyContexto> options) : base(options){}
    

        #region Modelos
        
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Ceo> Ceo { get; set; }
        public DbSet<Codigo> Codigo { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<Tarea> Tarea { get; set; }
        public DbSet<TareaUsuario> TareaUsuario { get; set; }
        public DbSet<TareaEmpleado> TareaEmpleado { get; set; }
        public DbSet<TareaDependencia> TareaDependencia { get; set; }
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<ActividadDependencia> ActividadDependencia { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<EmpleadoProyectoRol> EmpleadoProyectoRol { get; set; }
        public DbSet<RolProyecto> RolProyecto { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region Tablas

            modelBuilder.Entity<Usuario>()
                .ToTable("Usuario");

            modelBuilder.Entity<Empleado>()
                .ToTable("Empleado");

            modelBuilder.Entity<Ceo>()
                .ToTable("Ceo");

            modelBuilder.Entity<Codigo>()
                .ToTable("Codigo");

            modelBuilder.Entity<Empresa>()
                .ToTable("Empresa");

            modelBuilder.Entity<Proyecto>()
                .ToTable("Proyecto");

            modelBuilder.Entity<Tarea>()
                .ToTable("Tarea");

            modelBuilder.Entity<TareaUsuario>()
                .ToTable("TareaUsuario");

            modelBuilder.Entity<TareaDependencia>()
                .ToTable("TareaDependencia");
        
            modelBuilder.Entity<TareaEmpleado>()
                .ToTable("TareaEmpleado");
            
            modelBuilder.Entity<Actividad>()
                .ToTable("Actividad");

            modelBuilder.Entity<ActividadDependencia>()
                .ToTable("ActividadDependencia");

            modelBuilder.Entity<Comentario>()
                .ToTable("Comentario");

            modelBuilder.Entity<Notificacion>()
                .ToTable("Notificacion");
            
            modelBuilder.Entity<EmpleadoProyectoRol>()
                .ToTable("EmpleadoProyectoRol");

            modelBuilder.Entity<RolProyecto>()
                .ToTable("RolProyecto");

            
            #endregion

            #region PrimaryKey

            modelBuilder.Entity<Usuario>()
                .HasKey(e => e.UsuarioId)
                .HasName("PKUsuarioId");

            modelBuilder.Entity<Empleado>()
                .HasKey(e => e.EmpleadoId)
                .HasName("PKEmpleadoId");

            modelBuilder.Entity<Ceo>()
                .HasKey(e => e.CeoId)
                .HasName("PKCeoId");

            modelBuilder.Entity<Codigo>()
                .HasKey(c => c.CodigoId)
                .HasName("PKCodigoId");

            modelBuilder.Entity<Empresa>()
                .HasKey(e => e.EmpresaId)
                .HasName("PKEmpresaId");

            modelBuilder.Entity<Proyecto>()
                .HasKey(p => p.ProyectoId)
                .HasName("PKProyectoId");

            modelBuilder.Entity<Tarea>()
                .HasKey(t => t.TareaId)
                .HasName("PKTareaId");

            modelBuilder.Entity<TareaUsuario>()
                .HasKey(tu => new { tu.TareaId, tu.UsuarioId });

            modelBuilder.Entity<TareaDependencia>()
                .HasKey(td => new { td.TareaId, td.DependeDeId });

            modelBuilder.Entity<Actividad>()
                .HasKey(a => a.ActividadId)
                .HasName("PKActividadId");

            modelBuilder.Entity<ActividadDependencia>()
                .HasKey(ad => new { ad.ActividadId, ad.DependeDeActividadId });

            modelBuilder.Entity<TareaEmpleado>()
                .HasKey(te => new { te.TareaId, te.EmpleadoId });
            
            modelBuilder.Entity<Comentario>()
                .HasKey(c => c.ComentarioId)
                .HasName("PKComentarioId");

            modelBuilder.Entity<Notificacion>()
                .HasKey(n => n.NotificacionId)
                .HasName("PKNotificacionId");
            
            modelBuilder.Entity<EmpleadoProyectoRol>()
                .HasKey(epr => new { epr.EmpleadoId, epr.ProyectoId });

            modelBuilder.Entity<RolProyecto>()
                .HasKey(rp => rp.RolProyectoId)
                .HasName("PKRolProyectoId");
            
            #endregion

            #region ForeignKeys

            modelBuilder.Entity<Codigo>()
                .Property(c => c.UsuarioId)
                .HasColumnName("FkUsuarioId");
            
            modelBuilder.Entity<Empleado>()
                .Property(e => e.EmpresaId)
                .HasColumnName("FkEmpresaId");

            modelBuilder.Entity<Notificacion>()
                .Property(n => n.UsuarioId)
                .HasColumnName("FkUsuarioId");

            modelBuilder.Entity<Notificacion>()
                .Property(n => n.TareaId)
                .HasColumnName("FkTareaId");

            modelBuilder.Entity<Empleado>()
                .Property(e => e.UsuarioId)
                .HasColumnName("FkUsuarioId");

            modelBuilder.Entity<Ceo>()
                .Property(c => c.UsuarioId)
                .HasColumnName("FkUsuarioId");

            modelBuilder.Entity<Empresa>()
                .Property(e => e.CeoId)
                .HasColumnName("FkCeoId");

            modelBuilder.Entity<Proyecto>()
                .Property(p => p.EmpresaId)
                .HasColumnName("FkEmpresaId");

            modelBuilder.Entity<Tarea>()
                .Property(t => t.ProyectoId)
                .HasColumnName("FkProyectoId");

            modelBuilder.Entity<Tarea>()
                .Property(t => t.ActividadId)
                .HasColumnName("FkActividadId");

            modelBuilder.Entity<TareaUsuario>()
                .Property(tu => tu.TareaId)
                .HasColumnName("FkTareaId");

            modelBuilder.Entity<TareaUsuario>()
                .Property(tu => tu.UsuarioId)
                .HasColumnName("FkUsuarioId");
            
            modelBuilder.Entity<TareaEmpleado>()
                .Property(te => te.TareaId)
                .HasColumnName("FkTareaId");
            
            modelBuilder.Entity<TareaEmpleado>()
                .Property(te => te.EmpleadoId)
                .HasColumnName("FkEmpleadoId");

            modelBuilder.Entity<TareaDependencia>()
                .Property(td => td.TareaId)
                .HasColumnName("FkTareaId");

            modelBuilder.Entity<TareaDependencia>()
                .Property(td => td.DependeDeId)
                .HasColumnName("FkDependeDeId");

            modelBuilder.Entity<ActividadDependencia>()
                .Property(ad => ad.ActividadId)
                .HasColumnName("FkActividadId");

            modelBuilder.Entity<ActividadDependencia>()
                .Property(ad => ad.DependeDeActividadId)
                .HasColumnName("FkDependeDeActividadId");

            modelBuilder.Entity<Comentario>()
                .Property(c => c.TareaId)
                .HasColumnName("FkTareaId");

            modelBuilder.Entity<Comentario>()
                .Property(c => c.UsuarioId)
                .HasColumnName("FkUsuarioId");

            modelBuilder.Entity<EmpleadoProyectoRol>()
                .Property(epr => epr.EmpleadoId)
                .HasColumnName("FkEmpleadoId");

            modelBuilder.Entity<EmpleadoProyectoRol>()
                .Property(epr => epr.ProyectoId)
                .HasColumnName("FkProyectoId");

            modelBuilder.Entity<EmpleadoProyectoRol>()
                .Property(epr => epr.RolProyectoId)
                .HasColumnName("FkRolProyectoId");

            modelBuilder.Entity<RolProyecto>()
                .Property(rp => rp.RolProyectoId)
                .HasColumnName("PkRolProyectoId");
            
            modelBuilder.Entity<RolProyecto>()
                .Property(rp => rp.ProyectoId)
                .HasColumnName("FkProyectoId");

            modelBuilder.Entity<Actividad>()
                .Property(a => a.ProyectoId)
                .HasColumnName("FkProyectoId");
            
            #endregion

            #region Relaciones

            // Usuario → Codigo
            modelBuilder.Entity<Codigo>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Codigos)
                .HasForeignKey(c => c.UsuarioId)
                .IsRequired();
            
            // Usuario → Notificacion
            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Usuario)
                .WithMany(u => u.Notificaciones)
                .HasForeignKey(n => n.UsuarioId)
                .IsRequired();
            
            // Tarea → Notificacion 
            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Tarea)
                .WithMany(t => t.Notificaciones)
                .HasForeignKey(n => n.TareaId)
                .IsRequired(false);
            
            // Usuario → Comentario
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.UsuarioId)
                .IsRequired();
            
            // Tarea → Comentario
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Tarea)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.TareaId)
                .IsRequired();
            
            // Usuario → TareaUsuario
            modelBuilder.Entity<TareaUsuario>()
                .HasOne(tu => tu.Usuario)
                .WithMany(u => u.TareasUsuario)
                .HasForeignKey(tu => tu.UsuarioId)
                .IsRequired();
            
            // Tarea → TareaUsuario
            modelBuilder.Entity<TareaUsuario>()
                .HasOne(tu => tu.Tarea)
                .WithMany(t => t.TareasUsuario)
                .HasForeignKey(tu => tu.TareaId)
                .IsRequired();
            
            // Tarea -> TareaEmpleado
            modelBuilder.Entity<TareaEmpleado>()
                .HasOne(te => te.Tarea)
                .WithMany(t => t.TareasEmpleado)
                .HasForeignKey(te => te.TareaId)
                .IsRequired();
            
            // Empleado -> TareaEmpleado
            modelBuilder.Entity<TareaEmpleado>()
                .HasOne(te => te.Empleado)
                .WithMany(e => e.TareasEmpleado)
                .HasForeignKey(te => te.EmpleadoId)
                .IsRequired();
            
            // Proyecto → Tarea
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Proyecto)
                .WithMany(p => p.Tareas)
                .HasForeignKey(t => t.ProyectoId)
                .IsRequired();
            
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Actividad)
                .WithMany(a => a.Tareas)
                .HasForeignKey(t => t.ActividadId)
                .IsRequired();

            
            // Usuario → Empleado
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Usuario)
                .WithOne(u => u.Empleado)
                .HasForeignKey<Empleado>(e => e.UsuarioId)
                .IsRequired();

            // Usuario → Ceo
            modelBuilder.Entity<Ceo>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Ceo)
                .HasForeignKey<Ceo>(c => c.UsuarioId)
                .IsRequired();
            
            // Empleado → Empresa
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Empresa)               
                .WithMany(emp => emp.Empleados)        
                .HasForeignKey(e => e.EmpresaId)     
                .IsRequired();  
            
            // Ceo → Empresa
            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.Ceo)
                .WithMany(c => c.Empresas)
                .HasForeignKey(e => e.CeoId)
                .IsRequired(false);
            
            // Empresa → Proyecto
            modelBuilder.Entity<Proyecto>()
                .HasOne(p => p.Empresa)
                .WithMany(e => e.Proyectos)
                .HasForeignKey(p => p.EmpresaId)
                .IsRequired();
            
            // TareaDependencia
            modelBuilder.Entity<TareaDependencia>()
                .HasOne(td => td.Tarea)
                .WithMany(t => t.Dependencias)
                .HasForeignKey(td => td.TareaId)
                .IsRequired();
            modelBuilder.Entity<TareaDependencia>()
                .HasOne(td => td.DependeDe)
                .WithMany(t => t.Dependientes)
                .HasForeignKey(td => td.DependeDeId)
                .IsRequired();

            // ActividadDependencia 
            modelBuilder.Entity<ActividadDependencia>()
                .HasOne(ad => ad.Actividad)
                .WithMany(a => a.Dependencias)
                .HasForeignKey(ad => ad.ActividadId)
                .IsRequired();

            modelBuilder.Entity<ActividadDependencia>()
                .HasOne(ad => ad.DependeDeActividad)
                .WithMany(a => a.Dependientes)
                .HasForeignKey(ad => ad.DependeDeActividadId)
                .IsRequired();
            
            modelBuilder.Entity<EmpleadoProyectoRol>()
                .HasOne(epr => epr.Empleado)
                .WithMany(e => e.EmpleadosProyectoRol)
                .HasForeignKey(epr => epr.EmpleadoId)
                .IsRequired();

            modelBuilder.Entity<EmpleadoProyectoRol>()
                .HasOne(epr => epr.Proyecto)
                .WithMany(p => p.EmpleadosProyectoRol)
                .HasForeignKey(epr => epr.ProyectoId)
                .IsRequired();

            modelBuilder.Entity<EmpleadoProyectoRol>()
                .HasOne(epr => epr.RolProyecto)
                .WithMany(rp => rp.EmpleadosProyectoRol)
                .HasForeignKey(epr => epr.RolProyectoId)
                .IsRequired();

            modelBuilder.Entity<Proyecto>()
                .HasMany(p => p.Actividades)
                .WithOne(a => a.Proyecto)
                .HasForeignKey(p => p.ProyectoId)
                .IsRequired();
            
            modelBuilder.Entity<RolProyecto>()
                .HasOne(rp => rp.Proyecto)
                .WithMany(p => p.RolesProyecto)
                .HasForeignKey(rp => rp.ProyectoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
            
            #region Usuario

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.UsuarioId)
                    .HasColumnName("PkUsuarioId")
                    .IsRequired();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(30);

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FechaCreacion)
                    .IsRequired();

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FotoPerfil)
                    .HasMaxLength(255);

                entity.Property(e => e.FechaRegistro);

                entity.Property(e => e.FechaActualizacion);
            });

            #endregion

            #region Codigo

            modelBuilder.Entity<Codigo>(entity =>
            {
                entity.Property(e => e.CodigoId)
                    .HasColumnName("PkCodigoId")
                    .IsRequired();

                entity.Property(e => e.UsuarioId)
                    .IsRequired();

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .HasColumnType("text");
                
                entity.Property(e => e.TipoCodigo)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Expiracion)
                    .IsRequired();

                entity.Property(e => e.Creado)
                    .IsRequired();

                entity.Property(e => e.Revocado)
                    .IsRequired();
            });

            #endregion

            #region Empleado

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.Property(e => e.EmpleadoId)
                    .HasColumnName("PkEmpleadoId")
                    .IsRequired();

                entity.Property(e => e.UsuarioId)
                    .IsRequired();
            });

            #endregion

            #region Ceo

            modelBuilder.Entity<Ceo>(entity =>
            {
                entity.Property(e => e.CeoId)
                    .HasColumnName("PkCeoId")
                    .IsRequired();

                entity.Property(e => e.UsuarioId)
                    .IsRequired();
            });

            #endregion

            #region Empresa

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.Property(e => e.EmpresaId)
                    .HasColumnName("PkEmpresaId")
                    .IsRequired();

                entity.Property(e => e.CeoId);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion);

                entity.Property(e => e.FechaCreacion)
                    .IsRequired();

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            #endregion

            #region Proyecto

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.Property(e => e.ProyectoId)
                    .HasColumnName("PkProyectoId")
                    .IsRequired();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion);

                entity.Property(e => e.FechaInicio)
                    .IsRequired();

                entity.Property(e => e.FechaFin);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FechaCreado)
                    .IsRequired();

                entity.Property(e => e.FechaActualizacion)
                    .IsRequired();
            });

            #endregion

            #region Tarea

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.Property(e => e.TareaId)
                    .HasColumnName("PkTareaId")
                    .IsRequired();

                entity.Property(e => e.ProyectoId)
                    .IsRequired();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Descripcion);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FechaCreado)
                    .IsRequired();

                entity.Property(e => e.FechaInicio)
                    .IsRequired();

                entity.Property(e => e.FechaVencimiento)
                    .IsRequired();

                entity.Property(e => e.FechaActualizacion);

                entity.Property(e => e.FechaCompletada);

                entity.Property(e => e.ActividadId);
            });

            #endregion

            #region Actividad

            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.Property(e => e.ActividadId)
                    .HasColumnName("PkActividadId")
                    .IsRequired();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50);

                entity.Property(e => e.Prioridad)
                    .HasColumnType("varchar(50)")
                    .HasMaxLength(10);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FechaInicio)
                    .IsRequired();

                entity.Property(e => e.FechaFinalizacion)
                    .IsRequired();

                entity.Property(e => e.Orden)
                    .IsRequired();
            });

            #endregion

            #region Comentario

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.Property(e => e.ComentarioId)
                    .HasColumnName("PkComentarioId")
                    .IsRequired();

                entity.Property(e => e.TareaId)
                    .IsRequired();

                entity.Property(e => e.Contenido)
                    .IsRequired();

                entity.Property(e => e.Adjunto);

                entity.Property(e => e.UsuarioId)
                    .IsRequired();

                entity.Property(e => e.FechaCreado)
                    .IsRequired();
            });

            #endregion

            #region Notificacion

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.Property(e => e.NotificacionId)
                    .HasColumnName("PkNotificacionId")
                    .IsRequired();

                entity.Property(e => e.UsuarioId)
                    .IsRequired();

                entity.Property(e => e.TareaId);

                entity.Property(e => e.Mensaje)
                    .IsRequired();

                entity.Property(e => e.Leida)
                    .IsRequired();

                entity.Property(e => e.FechaCreado)
                    .IsRequired();
            });

            #endregion

            #region TareaUsuario

            modelBuilder.Entity<TareaUsuario>(entity =>
            {
                entity.HasKey(tu => new { tu.TareaId, tu.UsuarioId });

                entity.Property(tu => tu.TareaId)
                    .IsRequired();

                entity.Property(tu => tu.UsuarioId)
                    .IsRequired();
            });

            #endregion

            #region TareaEmpleado

            modelBuilder.Entity<TareaEmpleado>(entity =>
            {
                entity.HasKey(te => new { te.TareaId, te.EmpleadoId });
                
                entity.Property(te => te.TareaId)
                    .IsRequired();
                
                entity.Property(te => te.EmpleadoId)
                    .IsRequired();
            });

            #endregion
            
            #region TareaDependencia

            modelBuilder.Entity<TareaDependencia>(entity =>
            {
                entity.HasKey(td => new { td.TareaId, td.DependeDeId });

                entity.Property(td => td.TareaId)
                    .HasColumnName("FkTareaId")
                    .IsRequired();

                entity.Property(td => td.DependeDeId)
                    .HasColumnName("FkDependeDeId")
                    .IsRequired();
            });

            #endregion

            #region ActividadDependencia

            modelBuilder.Entity<ActividadDependencia>(entity =>
            {
                entity.HasKey(ad => new { ad.ActividadId, ad.DependeDeActividadId });

                entity.Property(ad => ad.ActividadId)
                    .HasColumnName("FkActividadId")
                    .IsRequired();

                entity.Property(ad => ad.DependeDeActividadId)
                    .HasColumnName("FkDependeDeActividadId")
                    .IsRequired();
            });

            #endregion

            #region RolProyecto

            modelBuilder.Entity<RolProyecto>(entity =>
            {
                entity.Property(rp => rp.RolProyectoId)
                    .HasColumnName("PkRolProyectoId")
                    .IsRequired();

                entity.Property(rp => rp.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(rp => rp.Descripcion)
                    .HasMaxLength(255);
                
                entity.Property(rp => rp.ProyectoId)
                    .HasColumnName("FkProyectoId")
                    .IsRequired();
            });


            #endregion
            
            #region EmpleadoProyectoRol

            modelBuilder.Entity<EmpleadoProyectoRol>(entity =>
            {
                entity.HasKey(epr => new { epr.EmpleadoId, epr.ProyectoId });

                entity.Property(epr => epr.EmpleadoId)
                    .HasColumnName("FkEmpleadoId")
                    .IsRequired();

                entity.Property(epr => epr.ProyectoId)
                    .HasColumnName("FkProyectoId")
                    .IsRequired();

                entity.Property(epr => epr.RolProyectoId)
                    .HasColumnName("FkRolProyectoId")
                    .IsRequired();
            });

            #endregion

        }
    
}