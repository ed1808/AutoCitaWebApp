namespace AutoCita.Api.Models;

public class Agenda
{
	public Guid Id { get; set; }
	public Guid UsuarioId { get; set; }
	public DateTime Fecha { get; set; }
	public DateTime HoraInicio { get; set; } 
	public DateTime HoraFin { get; set; } 
	public decimal CapacidadHoras { get; set; } = string.Empty;
	public decimal CargaActual { get; set; } = string.Empty;
	public string EstadoAgenda { get; set; } = string.Empty;
	public DateTime FechaCreacion { get; set; }

	public Usuario Usuario { get; set; } = null!;
    public List<Cita> Citas { get; set; } = [];

}
