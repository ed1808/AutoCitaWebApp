namespace AutoCita.Api.DTOs.Informe;

public class CargaTecnicoDTO
{
    public Guid UsuarioId { get; set; }
    public string NombreTecnico { get; set; } = null!;
    public decimal CapacidadHoras { get; set; }
    public decimal CargaActual { get; set; }
    public decimal PorcentajeOcupacion { get; set; }
    public string? EstadoAgenda { get; set; }
}
