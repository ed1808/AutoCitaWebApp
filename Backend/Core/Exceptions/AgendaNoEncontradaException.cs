namespace AutoCita.Api.Core.Exceptions;

public class AgendaNoEncontradaException : Exception
{
    public AgendaNoEncontradaException(Guid agendaId) : base($"La agenda {agendaId} no ha sido encontrada") {}
}
