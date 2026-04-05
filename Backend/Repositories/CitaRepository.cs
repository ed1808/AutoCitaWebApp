using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Cita;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Models;

namespace AutoCita.Api.Repositories;

public class CitaRepository : ICitaRepository
{
    private readonly AppDbContext _dbContext;

    public CitaRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListarCitaDTO, CitaNoEncontradaException>> ActualizarCita(Guid citaId, ActualizarCitaDTO citaDTO, CancellationToken cancellationToken)
    {
        Cita? cita = await _dbContext.Citas.FindAsync([citaId], cancellationToken: cancellationToken);

        if (cita is null)
        {
            return new CitaNoEncontradaException(citaId);
        }

        cita.AgendaId = citaDTO.AgendaId;
        cita.VehiculoId = citaDTO.VehiculoId;
        cita.ClienteId = citaDTO.ClienteId;
        cita.UsuarioId = citaDTO.UsuarioId;
        cita.FechaInicio = citaDTO.FechaInicio;
        cita.FechaFin = citaDTO.FechaFin;
        cita.DuracionEst = citaDTO.DuracionEst;
        cita.EstadoCita = citaDTO.EstadoCita;
        cita.MotivoCita = citaDTO.MotivoCita;

        _dbContext.Citas.Update(cita);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarCitaDTO
        {
            Id = cita.Id,
            AgendaId = cita.AgendaId,
            VehiculoId = cita.VehiculoId,
            ClienteId = cita.ClienteId,
            UsuarioId = cita.UsuarioId,
            FechaInicio = cita.FechaInicio,
            FechaFin = cita.FechaFin,
            DuracionEst = cita.DuracionEst,
            EstadoCita = cita.EstadoCita,
            MotivoCita = cita.MotivoCita,
            FechaCreacion = cita.FechaCreacion
        };
    }

    public async Task<ListarCitaDTO> CrearCita(CrearCitaDTO citaDTO, CancellationToken cancellationToken)
    {
        Cita cita = new()
        {
            AgendaId = citaDTO.AgendaId,
            VehiculoId = citaDTO.VehiculoId,
            ClienteId = citaDTO.ClienteId,
            UsuarioId = citaDTO.UsuarioId,
            FechaInicio = citaDTO.FechaInicio,
            FechaFin = citaDTO.FechaFin,
            DuracionEst = citaDTO.DuracionEst,
            EstadoCita = citaDTO.EstadoCita,
            MotivoCita = citaDTO.MotivoCita
        };

        _dbContext.Citas.Add(cita);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarCitaDTO
        {
            Id = cita.Id,
            AgendaId = cita.AgendaId,
            VehiculoId = cita.VehiculoId,
            ClienteId = cita.ClienteId,
            UsuarioId = cita.UsuarioId,
            FechaInicio = cita.FechaInicio,
            FechaFin = cita.FechaFin,
            DuracionEst = cita.DuracionEst,
            EstadoCita = cita.EstadoCita,
            MotivoCita = cita.MotivoCita,
            FechaCreacion = cita.FechaCreacion
        };
    }

    public async Task<Result<bool, CitaNoEncontradaException>> EliminarCita(Guid citaId, CancellationToken cancellationToken)
    {
        Cita? cita = await _dbContext.Citas.FindAsync([citaId], cancellationToken: cancellationToken);

        if (cita is null)
        {
            return new CitaNoEncontradaException(citaId);
        }

        _dbContext.Citas.Remove(cita);
        int deletedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return deletedRows > 0;
    }

    public async Task<Result<ListarCitaDTO, CitaNoEncontradaException>> ObtenerCita(Guid citaId, CancellationToken cancellationToken)
    {
        Cita? cita = await _dbContext.Citas.FindAsync([citaId], cancellationToken: cancellationToken);

        if (cita is null)
        {
            return new CitaNoEncontradaException(citaId);
        }

        return new ListarCitaDTO
        {
            Id = cita.Id,
            AgendaId = cita.AgendaId,
            VehiculoId = cita.VehiculoId,
            ClienteId = cita.ClienteId,
            UsuarioId = cita.UsuarioId,
            FechaInicio = cita.FechaInicio,
            FechaFin = cita.FechaFin,
            DuracionEst = cita.DuracionEst,
            EstadoCita = cita.EstadoCita,
            MotivoCita = cita.MotivoCita,
            FechaCreacion = cita.FechaCreacion
        };
    }

    public async Task<ICollection<ListarCitaDTO>> ObtenerCitas(CancellationToken cancellationToken)
    {
        List<ListarCitaDTO> citas = await _dbContext.Citas
            .Select(c => new ListarCitaDTO
            {
                Id = c.Id,
                AgendaId = c.AgendaId,
                VehiculoId = c.VehiculoId,
                ClienteId = c.ClienteId,
                UsuarioId = c.UsuarioId,
                FechaInicio = c.FechaInicio,
                FechaFin = c.FechaFin,
                DuracionEst = c.DuracionEst,
                EstadoCita = c.EstadoCita,
                MotivoCita = c.MotivoCita,
                FechaCreacion = c.FechaCreacion
            })
            .ToListAsync(cancellationToken);

        return citas;
    }
}
