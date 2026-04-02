using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.Rol;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IRolRepository
{
    Task<ListarRolDTO> CrearRol(CrearRolDTO rolDTO);
    Task<ICollection<ListarRolDTO>> ObtenerRoles();
    Task<Result<ListarRolDTO, RolNoEncontradoException>> ObtenerRol(int rolId);
    Task<Result<ListarRolDTO, RolNoEncontradoException>> ActualizarRol(ActualizarRolDTO rolDTO);
    Task<Result<bool, RolNoEncontradoException>> EliminarRol(int id);
}
