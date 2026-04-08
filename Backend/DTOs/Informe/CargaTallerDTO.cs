namespace AutoCita.Api.DTOs.Informe;

public class CargaTallerDTO
{
    public DateOnly Fecha { get; set; }
    public int TotalAgendas { get; set; }
    public decimal CapacidadTotalHoras { get; set; }
    public decimal CargaTotalHoras { get; set; }
    public decimal PorcentajeOcupacionGlobal { get; set; }
    public ICollection<CargaTecnicoDTO> Tecnicos { get; set; } = [];
}
