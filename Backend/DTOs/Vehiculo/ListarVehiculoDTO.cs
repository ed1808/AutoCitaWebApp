namespace AutoCita.Api.DTOs.Vehiculo;

public class ListarVehiculoDTO
{
    public Guid Id { get; set; }
    public string Placa { get; set; } = null!;
    public string Vin { get; set; } = null!;
    public string Marca { get; set; } = null!;
    public string Linea { get; set; } = null!;
    public short Modelo { get; set; }
    public string? Color { get; set; }
    public Guid PropietarioId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
}
