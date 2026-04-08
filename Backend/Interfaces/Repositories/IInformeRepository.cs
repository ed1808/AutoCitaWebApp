using AutoCita.Api.DTOs.Informe;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IInformeRepository
{
    Task<ResumenCitasDTO> ObtenerResumenCitas(DateOnly fecha, CancellationToken cancellationToken);
    Task<CargaTallerDTO> ObtenerCargaTaller(DateOnly fecha, CancellationToken cancellationToken);
    Task<ServiciosMasSolicitadosDTO> ObtenerServiciosMasSolicitados(string periodo, DateOnly fecha, int top, CancellationToken cancellationToken);
}
