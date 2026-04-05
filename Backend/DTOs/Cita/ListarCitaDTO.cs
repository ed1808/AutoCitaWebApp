namespace AutoCita.Api.DTOs.Cita;

public class ListarCitaDTO
{
    public Guid Id { get; set; }
    public Guid AgendaId { get; set; }
    public Guid VehiculoId { get; set; }
    public Guid ClienteId { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public decimal DuracionEst { get; set; }
    public string? EstadoCita { get; set; }
    public string MotivoCita { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
}
