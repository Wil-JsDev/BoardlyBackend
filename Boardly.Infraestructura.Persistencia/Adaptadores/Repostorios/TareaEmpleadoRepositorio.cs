using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Infraestructura.Persistencia.Contexto;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class TareaEmpleadoRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<TareaEmpleado>(boardlyContexto), ITareaEmpleadoRepositorio;