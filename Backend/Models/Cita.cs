namespace AutoCita.Api.Models;

public class Cita
{
	public Guid Id { get; set; }
	public Guid AgendaId { get; set; }
	public Guid VehiculoId { get; set; }
	public Guid ClienteId { get; set; }
	public Guid UsuarioId { get; set; }

	public DataTime FechaInicio { get; set; }
	public DataTime FechaFin { get; set; }
	public decimal DuracionEst { get; set; }
	public string EstadoCita { get; set; } = string.Empty;
	public string MotivoCita { get; set; } = string.Empty;
	public DateTime FechaCreacion { get; set; }

	public List<CitaServicio> CitaServicio { get; set; } = [];
    public Usuario Usuario { get; set; } = null!;
    public Agenda Agenda { get; set; } = null!;
    public Vehiculo Vehiculo { get; set; } = null!;
    public Cliente Cliente { get; set; } = null!;


}
