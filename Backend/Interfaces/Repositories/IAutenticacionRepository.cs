using AutoCita.Api.DTOs.Usuario;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IAutenticacionRepository
{
    Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO loginDTO, CancellationToken cancellationToken);
    Task<RegistrarUsuarioRespuestaDTO> RegistrarUsuario(RegistrarUsuarioDTO registroDTO, CancellationToken cancellationToken);
}
