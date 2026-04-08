namespace AutoCita.Api.DTOs.Informe;

public class ResumenCitasDTO
{
    public DateOnly Fecha { get; set; }
    public int TotalCitas { get; set; }
    public int Ingresadas { get; set; }
    public int Pendientes { get; set; }
    public int Retrasadas { get; set; }
    public int Canceladas { get; set; }
}
