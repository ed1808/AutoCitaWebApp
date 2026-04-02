namespace AutoCita.Api.Core.Exceptions;

public class RolNoEncontradoException : Exception
{
    public RolNoEncontradoException(int rolId) : base($"El rol {rolId} no ha sido encontrado") {}
}
