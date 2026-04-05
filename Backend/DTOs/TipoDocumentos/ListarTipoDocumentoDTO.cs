namespace AutoCita.Api.DTOs.TipoDocumentos;

public class ListarTipoDocumentoDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
}
