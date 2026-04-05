namespace AutoCita.Api.Core.Exceptions;

public class UsuarioNoEncontradoException : Exception
{
    public UsuarioNoEncontradoException(Guid usuarioId) : base($"El usuario {usuarioId} no ha sido encontrado") {}
}
