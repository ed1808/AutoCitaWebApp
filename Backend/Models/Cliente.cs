namespace AutoCita.Api.Models;

public class Cliente
{
    public Guid Id { get; set; }
    public string PrimerNombre { get; set; } = string.Empty;
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = string.Empty;
    public string? SegundoApellido { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public int TipoDocumentoId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }

    public TipoDocumento TipoDocumento { get; set; } = null!;
    public ICollection<Vehiculo> Vehiculos { get; set; } = [];
}
