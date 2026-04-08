namespace AutoCita.Api.DTOs.Informe;

public class ServiciosMasSolicitadosDTO
{
    public string Periodo { get; set; } = null!;
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }
    public ICollection<ServicioSolicitadoDTO> Servicios { get; set; } = [];
}
