namespace AutoCita.Api.Core.Exceptions;

public class ClienteNoEncontradoException : Exception
{
    public ClienteNoEncontradoException(Guid clienteId) : base($"El cliente {clienteId} no ha sido encontrado") {}
}
