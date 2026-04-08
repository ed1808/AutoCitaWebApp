namespace AutoCita.Api.DTOs.Informe;

public class ServicioSolicitadoDTO
{
    public int ServicioId { get; set; }
    public string NombreServicio { get; set; } = null!;
    public int TotalCitas { get; set; }
}
