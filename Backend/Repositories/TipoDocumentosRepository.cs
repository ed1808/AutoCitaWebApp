using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.TipoDocumentos;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Data;
using AutoCita.Api.Models;

namespace AutoCita.Api.Repositories;

public class TipoDocumentosRepository : ITipoDocumentosRepository
{
    private readonly AppDbContext _dbContext;

    public TipoDocumentosRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListarTipoDocumentoDTO, TipoDocumentoNoEncontradoException>> ActualizarTipoDocumento(int tipoDocumentoId, ActualizarTipoDocumentoDTO tipoDocumentoDTO, CancellationToken cancellationToken)
    {
        TipoDocumento? tipoDocumento = await _dbContext.TipoDocumentos.FindAsync([tipoDocumentoId], cancellationToken: cancellationToken);

        if (tipoDocumento is null)
        {
            return new TipoDocumentoNoEncontradoException(tipoDocumentoId);
        }

        tipoDocumento.Nombre = tipoDocumentoDTO.Nombre;

        _dbContext.TipoDocumentos.Update(tipoDocumento);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarTipoDocumentoDTO
        {
            Id = tipoDocumento.Id,
            Nombre = tipoDocumento.Nombre,
            FechaCreacion = tipoDocumento.FechaCreacion  
        };
    }

    public async Task<ListarTipoDocumentoDTO> CrearTipoDocumento(CrearTipoDocumentoDTO tipoDocumentoDTO, CancellationToken cancellationToken)
    {
        TipoDocumento tipoDocumento = new()
        {
            Nombre = tipoDocumentoDTO.Nombre
        };

        _dbContext.TipoDocumentos.Add(tipoDocumento);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarTipoDocumentoDTO
        {
            Id = tipoDocumento.Id,
            Nombre = tipoDocumento.Nombre,
            FechaCreacion = tipoDocumento.FechaCreacion
        };
    }

    public async Task<Result<bool, TipoDocumentoNoEncontradoException>> EliminarTipoDocumento(int tipoDocumentoId, CancellationToken cancellationToken)
    {
        TipoDocumento? tipoDocumento = await _dbContext.TipoDocumentos.FindAsync([tipoDocumentoId], cancellationToken: cancellationToken);

        if (tipoDocumento is null)
        {
            return new TipoDocumentoNoEncontradoException(tipoDocumentoId);
        }

        _dbContext.TipoDocumentos.Remove(tipoDocumento);
        int deletedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return deletedRows > 0;
    }

    public async Task<Result<ListarTipoDocumentoDTO, TipoDocumentoNoEncontradoException>> ObtenerTipoDocumento(int tipoDocumentoId, CancellationToken cancellationToken)
    {
        TipoDocumento? tipoDocumento = await _dbContext.TipoDocumentos.FindAsync([tipoDocumentoId], cancellationToken: cancellationToken);

        if (tipoDocumento is null)
        {
            return new TipoDocumentoNoEncontradoException(tipoDocumentoId);
        }

        return new ListarTipoDocumentoDTO
        {
            Id = tipoDocumento.Id,
            Nombre = tipoDocumento.Nombre,
            FechaCreacion = tipoDocumento.FechaCreacion
        };
    }

    public async Task<ICollection<ListarTipoDocumentoDTO>> ObtenerTiposDocumentos(CancellationToken cancellationToken)
    {
        List<ListarTipoDocumentoDTO> tipoDocumentos = await _dbContext.TipoDocumentos.Select(td => new ListarTipoDocumentoDTO
        {
            Id = td.Id,
            Nombre = td.Nombre,
            FechaCreacion = td.FechaCreacion
        }).ToListAsync(cancellationToken);

        return tipoDocumentos;
    }
}
