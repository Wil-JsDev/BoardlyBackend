using System.Collections;

namespace Boardly.Aplicacion.DTOs.PDF;

public record ProyectoDatosPdf
(
    DateTime Fecha ,
    List<ProyectoDtoPdf> ProyectoDto
);

public record ProyectoDtoPdf
(
    string Nombre,
    string? Descripcion,
    DateTime? FechaInicio,
    DateTime? FechaFin    
);
