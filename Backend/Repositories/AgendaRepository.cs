using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Agenda;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Models;

namespace AutoCita.Api.Repositories;

public class AgendaRepository : IAgendaRepository
{
    private readonly AppDbContext _dbContext;

    public AgendaRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListarAgendaDTO, AgendaNoEncontradaException>> ActualizarAgenda(Guid agendaId, ActualizarAgendaDTO agendaDTO, CancellationToken cancellationToken)
    {
        Agenda? agenda = await _dbContext.Agendas.FindAsync([agendaId], cancellationToken: cancellationToken);

        if (agenda is null)
        {
            return new AgendaNoEncontradaException(agendaId);
        }

        agenda.UsuarioId = agendaDTO.UsuarioId;
        agenda.Fecha = agendaDTO.Fecha;
        agenda.HoraInicio = agendaDTO.HoraInicio;
        agenda.HoraFin = agendaDTO.HoraFin;
        agenda.CapacidadHoras = agendaDTO.CapacidadHoras;
        agenda.CargaActual = agendaDTO.CargaActual;
        agenda.EstadoAgenda = agendaDTO.EstadoAgenda;

        _dbContext.Agendas.Update(agenda);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarAgendaDTO
        {
            Id = agenda.Id,
            UsuarioId = agenda.UsuarioId,
            Fecha = agenda.Fecha,
            HoraInicio = agenda.HoraInicio,
            HoraFin = agenda.HoraFin,
            CapacidadHoras = agenda.CapacidadHoras,
            CargaActual = agenda.CargaActual,
            EstadoAgenda = agenda.EstadoAgenda,
            FechaCreacion = agenda.FechaCreacion
        };
    }

    public async Task<ListarAgendaDTO> CrearAgenda(CrearAgendaDTO agendaDTO, CancellationToken cancellationToken)
    {
        Agenda agenda = new()
        {
            UsuarioId = agendaDTO.UsuarioId,
            Fecha = agendaDTO.Fecha,
            HoraInicio = agendaDTO.HoraInicio,
            HoraFin = agendaDTO.HoraFin,
            CapacidadHoras = agendaDTO.CapacidadHoras,
            CargaActual = agendaDTO.CargaActual,
            EstadoAgenda = agendaDTO.EstadoAgenda
        };

        _dbContext.Agendas.Add(agenda);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarAgendaDTO
        {
            Id = agenda.Id,
            UsuarioId = agenda.UsuarioId,
            Fecha = agenda.Fecha,
            HoraInicio = agenda.HoraInicio,
            HoraFin = agenda.HoraFin,
            CapacidadHoras = agenda.CapacidadHoras,
            CargaActual = agenda.CargaActual,
            EstadoAgenda = agenda.EstadoAgenda,
            FechaCreacion = agenda.FechaCreacion
        };
    }

    public async Task<Result<bool, AgendaNoEncontradaException>> EliminarAgenda(Guid agendaId, CancellationToken cancellationToken)
    {
        Agenda? agenda = await _dbContext.Agendas.FindAsync([agendaId], cancellationToken: cancellationToken);

        if (agenda is null)
        {
            return new AgendaNoEncontradaException(agendaId);
        }

        _dbContext.Agendas.Remove(agenda);
        int deletedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return deletedRows > 0;
    }

    public async Task<Result<ListarAgendaDTO, AgendaNoEncontradaException>> ObtenerAgenda(Guid agendaId, CancellationToken cancellationToken)
    {
        Agenda? agenda = await _dbContext.Agendas.FindAsync([agendaId], cancellationToken: cancellationToken);

        if (agenda is null)
        {
            return new AgendaNoEncontradaException(agendaId);
        }

        return new ListarAgendaDTO
        {
            Id = agenda.Id,
            UsuarioId = agenda.UsuarioId,
            Fecha = agenda.Fecha,
            HoraInicio = agenda.HoraInicio,
            HoraFin = agenda.HoraFin,
            CapacidadHoras = agenda.CapacidadHoras,
            CargaActual = agenda.CargaActual,
            EstadoAgenda = agenda.EstadoAgenda,
            FechaCreacion = agenda.FechaCreacion
        };
    }

    public async Task<ICollection<ListarAgendaDTO>> ObtenerAgendas(CancellationToken cancellationToken)
    {
        List<ListarAgendaDTO> agendas = await _dbContext.Agendas
            .Select(a => new ListarAgendaDTO
            {
                Id = a.Id,
                UsuarioId = a.UsuarioId,
                Fecha = a.Fecha,
                HoraInicio = a.HoraInicio,
                HoraFin = a.HoraFin,
                CapacidadHoras = a.CapacidadHoras,
                CargaActual = a.CargaActual,
                EstadoAgenda = a.EstadoAgenda,
                FechaCreacion = a.FechaCreacion
            })
            .ToListAsync(cancellationToken);

        return agendas;
    }
}
