using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class ResultadoPaginadoUsuario(
    ILogger<ResultadoPaginadoUsuario> logger,
    IUsuarioRepositorio repositorioUsuario
    ) : IResultadoPaginaUsuario<PaginacionParametro, UsuarioDto>
{
   public async Task<ResultadoT<ResultadoPaginado<UsuarioDto>>> ObtenerPaginacionUsuarioAsync(PaginacionParametro solicitud, CancellationToken cancellationToken)
    {
        if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
        {
            logger.LogWarning("Parámetros inválidos de paginación. TamañoPagina: {TamanoPagina}, NumeroPagina: {NumeroPagina}",
                solicitud.TamanoPagina, solicitud.NumeroPagina);

            return ResultadoT<ResultadoPaginado<UsuarioDto>>.Fallo(
                Error.Fallo("400", "Los parámetros de paginación deben ser mayores a cero.")
            );
        }

        var resultadoPagina = await repositorioUsuario.ObtenerPaginadoAsync(
            solicitud.NumeroPagina, solicitud.TamanoPagina, cancellationToken
        );

        if (!resultadoPagina.Elementos!.Any())
        {
            logger.LogWarning("No se encontraron usuarios en la página {NumeroPagina} con tamaño {TamanoPagina}.",
                solicitud.NumeroPagina, solicitud.TamanoPagina);

            return ResultadoT<ResultadoPaginado<UsuarioDto>>.Fallo(
                Error.Fallo("409", "No se encontraron usuarios para los parámetros de paginación especificados.")
            );
        }

        var dtoList = resultadoPagina.Elementos!.Select(usuarioEntidad => new UsuarioDto
        (
            UsuarioId: usuarioEntidad.UsuarioId,
            Nombre: usuarioEntidad.Nombre,
            Apellido: usuarioEntidad.Apellido,
            Correo: usuarioEntidad.Correo,
            NombreUsuario: usuarioEntidad.Nombre,
            FechaCreacion: usuarioEntidad.FechaCreacion,
            Estado: usuarioEntidad.Estado,
            FotoPerfil: usuarioEntidad.FotoPerfil,
            FechaRegistro: usuarioEntidad.FechaRegistro
        ));

        var resultadoPaginado = new ResultadoPaginado<UsuarioDto>(
            elementos: dtoList,
            totalElementos: resultadoPagina.TotalElementos,
            paginaActual: solicitud.NumeroPagina,
            tamanioPagina: solicitud.TamanoPagina
        );

        logger.LogInformation("Se obtuvo la página {NumeroPagina} de usuarios con éxito. Total de usuarios en esta página: {CantidadUsuarios}",
            solicitud.NumeroPagina, dtoList.Count());

        return ResultadoT<ResultadoPaginado<UsuarioDto>>.Exito(resultadoPaginado);
    }

}