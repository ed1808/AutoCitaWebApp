namespace AutoCita.Api.Models;

public class TipoDocumento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }

    public ICollection<Cliente> Clientes { get; set; } = [];
    public ICollection<Usuario> Usuarios { get; set; } = [];
}
