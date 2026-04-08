using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.Usuario;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<ListarUsuarioDTO> CrearUsuario(CrearUsuarioDTO usuarioDTO, CancellationToken cancellationToken);
    Task<ICollection<ListarUsuarioDTO>> ObtenerUsuarios(CancellationToken cancellationToken);
    Task<Result<ListarUsuarioDTO, UsuarioNoEncontradoException>> ObtenerUsuario(string nombreUsuario, CancellationToken cancellationToken);
    Task<Result<ListarUsuarioDTO, UsuarioNoEncontradoException>> ObtenerUsuario(Guid usuarioId, CancellationToken cancellationToken);
    Task<Result<ListarUsuarioDTO, UsuarioNoEncontradoException>> ActualizarUsuario(Guid usuarioId, ActualizarUsuarioDTO usuarioDTO, CancellationToken cancellationToken);
    Task<Result<bool, UsuarioNoEncontradoException>> EliminarUsuario(Guid usuarioId, CancellationToken cancellationToken);
}
