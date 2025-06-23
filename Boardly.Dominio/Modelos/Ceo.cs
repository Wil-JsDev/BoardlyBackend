namespace Boardly.Dominio.Modelos;

public class Ceo
{
    public Guid CeoId { get; set; }
    public Guid UsuarioId { get; set; }

    public Usuario Usuario { get; set; }
    public ICollection<Empresa> Empresas { get; set; }
}