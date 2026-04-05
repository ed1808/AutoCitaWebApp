namespace AutoCita.Api.DTOs.Vehiculo;

public class CrearVehiculoDTO
{
    public string Placa { get; set; } = null!;
    public string Vin { get; set; } = null!;
    public string Marca { get; set; } = null!;
    public string Linea { get; set; } = null!;
    public short Modelo { get; set; }
    public string? Color { get; set; }
    public Guid PropietarioId { get; set; }
}
