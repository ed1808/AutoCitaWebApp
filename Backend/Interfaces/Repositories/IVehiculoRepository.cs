using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.Vehiculo;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IVehiculoRepository
{
    Task<ListarVehiculoDTO> CrearVehiculo(CrearVehiculoDTO vehiculoDTO, CancellationToken cancellationToken);
    Task<ICollection<ListarVehiculoDTO>> ObtenerVehiculos(CancellationToken cancellationToken);
    Task<Result<ListarVehiculoDTO, VehiculoNoEncontradoException>> ObtenerVehiculo(Guid vehiculoId, CancellationToken cancellationToken);
    Task<Result<ListarVehiculoDTO, VehiculoNoEncontradoException>> ActualizarVehiculo(Guid vehiculoId, ActualizarVehiculoDTO vehiculoDTO, CancellationToken cancellationToken);
    Task<Result<bool, VehiculoNoEncontradoException>> EliminarVehiculo(Guid vehiculoId, CancellationToken cancellationToken);
}
