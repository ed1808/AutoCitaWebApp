namespace AutoCita.Api.Core.Exceptions;

public class CitaNoEncontradaException : Exception
{
    public CitaNoEncontradaException(Guid citaId) : base($"La cita {citaId} no ha sido encontrada") {}
}
