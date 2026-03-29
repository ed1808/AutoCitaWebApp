namespace AutoCita.Api.Models;

public class CitaServicio
{
	public Guid CitaId { get; set; }
	public int ServicioId { get; set; }

	public int Cantidad { get; set; } 

	public Cita Cita { get; set; } = null!;
	public Servicio Servicio { get; set; } = null!;
}
