namespace AutoCita.Api.Models;

public class Servicio
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public decimal DuracionEstimada { get; set; }
	public decimal PrecioSugerido { get; set; }
	public bool Activo { get; set; }

    public List<CitaServicio> CitaServicio { get; set; } = [];

}
