namespace AutoCita.Api.DTOs.Agenda;

public class ListarAgendaDTO
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
}
