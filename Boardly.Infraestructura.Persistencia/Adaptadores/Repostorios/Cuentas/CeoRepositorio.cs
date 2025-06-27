using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Infraestructura.Persistencia.Contexto;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios.Cuentas;

public class CeoRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<Ceo>(boardlyContexto), ICeoRepositorio
{
    
}