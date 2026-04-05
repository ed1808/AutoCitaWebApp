using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.Rol;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IRolRepository
{
    Task<ListarRolDTO> CrearRol(CrearRolDTO rolDTO, CancellationToken cancellationToken);
    Task<ICollection<ListarRolDTO>> ObtenerRoles(CancellationToken cancellationToken);
    Task<Result<ListarRolDTO, RolNoEncontradoException>> ObtenerRol(int rolId, CancellationToken cancellationToken);
    Task<Result<ListarRolDTO, RolNoEncontradoException>> ActualizarRol(int rolId, ActualizarRolDTO rolDTO, CancellationToken cancellationToken);
    Task<Result<bool, RolNoEncontradoException>> EliminarRol(int rolId, CancellationToken cancellationToken);
}
