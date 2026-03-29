namespace AutoCita.Api.Models;

public class Servicio
{
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;
	public decimal DuracionEstimada { get; set; }
	public decimal PrecioSugerido { get; set; }
	public bool Activo { get; set; }

	public ICollection<Cita> Citas { get; set; } = [];
    public ICollection<CitaServicio> CitaServicio { get; set; } = [];
}
