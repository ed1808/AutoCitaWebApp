namespace AutoCita.Api.DTOs.Cliente;

public class ListarClienteDTO
{
    public Guid Id { get; set; }
    public string PrimerNombre { get; set; } = null!;
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = null!;
    public string? SegundoApellido { get; set; }
    public string NumeroDocumento { get; set; } = null!;
    public int TipoDocumentoId { get; set; }
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
}
