using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Servicio;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Models;

namespace AutoCita.Api.Repositories;

public class ServicioRepository : IServicioRepository
{
    private readonly AppDbContext _dbContext;

    public ServicioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListarServicioDTO, ServicioNoEncontradoException>> ActualizarServicio(int servicioId, ActualizarServicioDTO servicioDTO, CancellationToken cancellationToken)
    {
        Servicio? servicio = await _dbContext.Servicios.FindAsync([servicioId], cancellationToken: cancellationToken);

        if (servicio is null)
        {
            return new ServicioNoEncontradoException(servicioId);
        }

        servicio.Nombre = servicioDTO.Nombre;
        servicio.DuracionEstimada = servicioDTO.DuracionEstimada;
        servicio.PrecioSugerido = servicioDTO.PrecioSugerido;
        servicio.Activo = servicioDTO.Activo;

        _dbContext.Servicios.Update(servicio);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarServicioDTO
        {
            Id = servicio.Id,
            Nombre = servicio.Nombre,
            DuracionEstimada = servicio.DuracionEstimada,
            PrecioSugerido = servicio.PrecioSugerido,
            Activo = servicio.Activo
        };
    }

    public async Task<ListarServicioDTO> CrearServicio(CrearServicioDTO servicioDTO, CancellationToken cancellationToken)
    {
        Servicio servicio = new()
        {
            Nombre = servicioDTO.Nombre,
            DuracionEstimada = servicioDTO.DuracionEstimada,
            PrecioSugerido = servicioDTO.PrecioSugerido,
            Activo = servicioDTO.Activo
        };

        _dbContext.Servicios.Add(servicio);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarServicioDTO
        {
            Id = servicio.Id,
            Nombre = servicio.Nombre,
            DuracionEstimada = servicio.DuracionEstimada,
            PrecioSugerido = servicio.PrecioSugerido,
            Activo = servicio.Activo
        };
    }

    public async Task<Result<bool, ServicioNoEncontradoException>> EliminarServicio(int servicioId, CancellationToken cancellationToken)
    {
        Servicio? servicio = await _dbContext.Servicios.FindAsync([servicioId], cancellationToken: cancellationToken);

        if (servicio is null)
        {
            return new ServicioNoEncontradoException(servicioId);
        }

        _dbContext.Servicios.Remove(servicio);
        int deletedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return deletedRows > 0;
    }

    public async Task<Result<ListarServicioDTO, ServicioNoEncontradoException>> ObtenerServicio(int servicioId, CancellationToken cancellationToken)
    {
        Servicio? servicio = await _dbContext.Servicios.FindAsync([servicioId], cancellationToken: cancellationToken);

        if (servicio is null)
        {
            return new ServicioNoEncontradoException(servicioId);
        }

        return new ListarServicioDTO
        {
            Id = servicio.Id,
            Nombre = servicio.Nombre,
            DuracionEstimada = servicio.DuracionEstimada,
            PrecioSugerido = servicio.PrecioSugerido,
            Activo = servicio.Activo
        };
    }

    public async Task<ICollection<ListarServicioDTO>> ObtenerServicios(CancellationToken cancellationToken)
    {
        List<ListarServicioDTO> servicios = await _dbContext.Servicios
            .Select(s => new ListarServicioDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                DuracionEstimada = s.DuracionEstimada,
                PrecioSugerido = s.PrecioSugerido,
                Activo = s.Activo
            })
            .ToListAsync(cancellationToken);

        return servicios;
    }
}
