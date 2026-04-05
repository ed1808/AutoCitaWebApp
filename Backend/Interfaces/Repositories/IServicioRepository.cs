using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.Servicio;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IServicioRepository
{
    Task<ListarServicioDTO> CrearServicio(CrearServicioDTO servicioDTO, CancellationToken cancellationToken);
    Task<ICollection<ListarServicioDTO>> ObtenerServicios(CancellationToken cancellationToken);
    Task<Result<ListarServicioDTO, ServicioNoEncontradoException>> ObtenerServicio(int servicioId, CancellationToken cancellationToken);
    Task<Result<ListarServicioDTO, ServicioNoEncontradoException>> ActualizarServicio(int servicioId, ActualizarServicioDTO servicioDTO, CancellationToken cancellationToken);
    Task<Result<bool, ServicioNoEncontradoException>> EliminarServicio(int servicioId, CancellationToken cancellationToken);
}
