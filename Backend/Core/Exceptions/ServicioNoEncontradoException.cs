namespace AutoCita.Api.Core.Exceptions;

public class ServicioNoEncontradoException : Exception
{
    public ServicioNoEncontradoException(int servicioId) : base($"El servicio {servicioId} no ha sido encontrado") {}
}
