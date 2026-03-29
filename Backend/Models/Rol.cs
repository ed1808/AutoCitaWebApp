namespace AutoCita.Api.Models;

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }

    public ICollection<Usuario> Usuarios { get; set; } = [];
}
