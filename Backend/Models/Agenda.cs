namespace AutoCita.Api.Models;

public class Agenda
{
	public Guid Id { get; set; }
	public Guid UsuarioId { get; set; }
	public DateTime Fecha { get; set; }
	public DateTime HoraInicio { get; set; } 
	public DateTime HoraFin { get; set; } 
	public decimal CapacidadHoras { get; set; }
	public decimal CargaActual { get; set; }
	public string? EstadoAgenda { get; set; }
	public DateTime FechaCreacion { get; set; }

	public Usuario Usuario { get; set; } = null!;
    public ICollection<Cita> Citas { get; set; } = [];

}
