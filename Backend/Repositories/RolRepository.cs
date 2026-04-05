using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Rol;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Models;

namespace AutoCita.Api.Repositories;

public class RolRepository : IRolRepository
{
    private readonly AppDbContext _dbContext;

    public RolRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListarRolDTO, RolNoEncontradoException>> ActualizarRol(int rolId, ActualizarRolDTO rolDTO, CancellationToken cancellationToken)
    {
        Rol? rol = await _dbContext.Roles.FindAsync([rolId], cancellationToken: cancellationToken);

        if (rol is null)
        {
            return new RolNoEncontradoException(rolId);
        }

        rol.Nombre = rolDTO.Nombre;
        _dbContext.Roles.Update(rol);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarRolDTO
        {
            Id = rol.Id,
            Nombre = rol.Nombre,
            FechaCreacion = rol.FechaCreacion
        };
    }

    public async Task<ListarRolDTO> CrearRol(CrearRolDTO rolDTO, CancellationToken cancellationToken)
    {
        Rol rol = new()
        {
            Nombre = rolDTO.Nombre
        };

        _dbContext.Roles.Add(rol);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarRolDTO
        {
            Id = rol.Id,
            Nombre = rol.Nombre,
            FechaCreacion = rol.FechaCreacion
        };
    }

    public async Task<Result<bool, RolNoEncontradoException>> EliminarRol(int rolId, CancellationToken cancellationToken)
    {
        Rol? rol = await _dbContext.Roles.FindAsync([rolId], cancellationToken: cancellationToken);

        if (rol is null)
        {
            return new RolNoEncontradoException(rolId);
        }

        _dbContext.Roles.Remove(rol);
        int deletedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return deletedRows > 0;
    }

    public async Task<Result<ListarRolDTO, RolNoEncontradoException>> ObtenerRol(int rolId, CancellationToken cancellationToken)
    {
        Rol? rol = await _dbContext.Roles.FindAsync([rolId], cancellationToken: cancellationToken);

        if (rol is null)
        {
            return new RolNoEncontradoException(rolId);
        }

        return new ListarRolDTO
        {
            Id = rol.Id,
            Nombre = rol.Nombre,
            FechaCreacion = rol.FechaCreacion
        };
    }

    public async Task<ICollection<ListarRolDTO>> ObtenerRoles(CancellationToken cancellationToken)
    {
        List<ListarRolDTO> roles = await _dbContext.Roles
            .Select(rol => new ListarRolDTO
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                FechaCreacion = rol.FechaCreacion
            })
            .ToListAsync(cancellationToken);

        return roles;
    }
}
