namespace Boardly.Dominio.Modelos;

public sealed class Codigo
{
    public Guid CodigoId { get; set; }
    public Guid UsuarioId { get; set; }
    public string Valor { get; set; }
    public DateTime Expiracion { get; set; }
    public DateTime Creado { get; set; }
    public bool Revocado { get; set; }

    public Usuario Usuario { get; set; }
}