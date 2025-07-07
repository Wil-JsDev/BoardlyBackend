using Boardly.Aplicacion.Adaptadores.Autenticacion;
using Boardly.Aplicacion.Adaptadores.Ceo;
using Boardly.Aplicacion.Adaptadores.Codigo;
using Boardly.Aplicacion.Adaptadores.Empleados;
using Boardly.Aplicacion.Adaptadores.Empresa;
using Boardly.Aplicacion.Adaptadores.Proyecto;
using Boardly.Aplicacion.Adaptadores.Usuario;
using Boardly.Aplicacion.DTOs.Autenticacion;
using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Aplicacion.DTOs.Codigo;
using Boardly.Aplicacion.DTOs.Contrasena;
using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Aplicacion.DTOs.Empresa;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;
using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.CasosDeUso.Empresa;
using Boardly.Dominio.Puertos.CasosDeUso.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boardly.Aplicacion;

public static class InyeccionDeDependencia
{
    public static void AgregarAplicacion(this IServiceCollection servicios)
    {

        #region Usuario

            servicios.AddScoped<ICrearUsuario<CrearUsuarioDto, UsuarioDto>, CrearUsuario>();
            servicios.AddScoped<IActualizarUsuario<ActualizarUsuarioDto, ActualizarUsuarioDto>, ActualizarUsuario>();
            servicios.AddScoped<IBorrarUsuario, BorrarUsuario>();
            servicios.AddScoped<IModificarContrasenaUsuario<ModificarContrasenaUsuarioDto>, ModificarContrasenaUsuario>();
            servicios.AddScoped<IObtenerIdUsuario<UsuarioDto>, ObtenerIdUsuario>();
            servicios.AddScoped<IOlvidarContrasenaUsuario, OlvidarContrasenaUsuario>();
            servicios.AddScoped<IResultadoPaginaUsuario<PaginacionParametro, UsuarioDto>, ResultadoPaginadoUsuario>();
            
        #endregion
        
        #region CEO

            servicios.AddScoped<IBorrarCeo, BorrarCeo>();
            servicios.AddScoped<ICrearCeo<CrearCeoDto, CeoDto>, CrearCeo>();
            
        #endregion

        #region Empresa

            servicios.AddScoped<IActualizarEmpresa<ActualizarEmpresaDto, ActualizarEmpresaDto>, ActualizarEmpresa>();
            servicios.AddScoped<IBorrarEmpresa, BorrarEmpresa>();
            servicios.AddScoped<ICrearEmpresa<CrearEmpresaDto, EmpresaDto>, CrearEmpresa>();
            servicios.AddScoped<IResultadoPaginaEmpresa<PaginacionParametro, EmpresaDto>, ResultadoPaginadoEmpresa>();
            
        #endregion

        #region Empleado
        
            servicios.AddScoped<ICrearEmpleado<CrearEmpleadoDto, EmpleadoDto>, CrearEmpleado>();
            servicios.AddScoped<IBorrarEmpleado, BorrarEmpleado>();
            servicios.AddScoped<IObtenerIdEmpleado<EmpleadoDto>, ObtenerIdEmpleado>(); 
            servicios.AddScoped<IResultadoPaginadoEmpleado<PaginacionParametro,EmpleadoDto>, ResultadoPaginadoEmpleado>(); 

        #endregion
        
        #region Codigo
        
        servicios.AddScoped<ICrearCodigo, CrearCodigo>();
        servicios.AddScoped<IObtenerCodigo<CodigoDto>,ObtenerCodigo>();
        servicios.AddScoped<ICodigoDisponible<Resultado>, CodigoDisponible>();
        servicios.AddScoped<IConfirmarCuenta<Resultado, CodigoConfirmarCuentaDto>, ConfirmarCuenta>();
        servicios.AddScoped<IEliminarCodigo<Resultado>, EliminarCodigo>();
        servicios.AddScoped<IBuscarCodigo<CodigoDto>, BuscarCodigo>();
        servicios.AddScoped<IObtenerIdCeo, ObtenerIdCeo>();
        
        #endregion

        #region  Autenticacion

        servicios.AddScoped<IAutenticacion<AutenticacionRespuesta, AutenticacionSolicitud>, Autenticacion>();

        #endregion
        
        #region Proyecto

            servicios.AddScoped<ICrearProyecto<CrearProyectoDto, ProyectoDto>, CrearProyecto>();
            servicios.AddScoped<IActualizarProyecto<ActualizarProyectoDto, ActualizarProyectoDto>, ActualizarProyecto>();
            servicios.AddScoped<IBorrarProyecto, BorrarProyecto>();
            servicios.AddScoped<IObtenerIdProyecto<ProyectoDto>, ObtenerIdProyecto>();
            servicios.AddScoped<IResultadoPaginaProyecto<PaginacionParametro, ProyectoDto>, ResultadoPaginaProyecto>();

        #endregion
    }
}