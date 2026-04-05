using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.Agenda;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IAgendaRepository
{
    Task<ListarAgendaDTO> CrearAgenda(CrearAgendaDTO agendaDTO, CancellationToken cancellationToken);
    Task<ICollection<ListarAgendaDTO>> ObtenerAgendas(CancellationToken cancellationToken);
    Task<Result<ListarAgendaDTO, AgendaNoEncontradaException>> ObtenerAgenda(Guid agendaId, CancellationToken cancellationToken);
    Task<Result<ListarAgendaDTO, AgendaNoEncontradaException>> ActualizarAgenda(Guid agendaId, ActualizarAgendaDTO agendaDTO, CancellationToken cancellationToken);
    Task<Result<bool, AgendaNoEncontradaException>> EliminarAgenda(Guid agendaId, CancellationToken cancellationToken);
}
