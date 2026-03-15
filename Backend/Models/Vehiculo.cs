namespace AutoCita.Api.Models;

public class Vehiculo
{
    public Guid Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Linea { get; set; } = string.Empty;
    public short Modelo { get; set; }
    public string Color { get; set; } = string.Empty;
    public Guid PropietarioId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }

    public Cliente Propietario { get; set; } = null!;
}
