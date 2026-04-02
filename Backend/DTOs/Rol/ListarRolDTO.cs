namespace AutoCita.Api.DTOs.Rol;

public class ListarRolDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
}
