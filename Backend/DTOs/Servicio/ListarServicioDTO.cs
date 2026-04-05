namespace AutoCita.Api.DTOs.Servicio;

public class ListarServicioDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public decimal DuracionEstimada { get; set; }
    public decimal PrecioSugerido { get; set; }
    public bool Activo { get; set; }
}
