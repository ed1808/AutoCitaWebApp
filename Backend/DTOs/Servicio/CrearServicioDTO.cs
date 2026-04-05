namespace AutoCita.Api.DTOs.Servicio;

public class CrearServicioDTO
{
    public string Nombre { get; set; } = null!;
    public decimal DuracionEstimada { get; set; }
    public decimal PrecioSugerido { get; set; }
    public bool Activo { get; set; }
}
