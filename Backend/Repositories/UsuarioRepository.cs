using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.Core.Security;
using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Usuario;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Models;

namespace AutoCita.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _dbContext;
    private readonly PasswordHasher _passwordHasher;

    public UsuarioRepository(AppDbContext dbContext, PasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<ListarUsuarioDTO, UsuarioNoEncontradoException>> ActualizarUsuario(Guid usuarioId, ActualizarUsuarioDTO usuarioDTO, CancellationToken cancellationToken)
    {
        Usuario? usuario = await _dbContext.Usuarios.FindAsync([usuarioId], cancellationToken: cancellationToken);

        if (usuario is null)
        {
            return new UsuarioNoEncontradoException(usuarioId);
        }

        usuario.PrimerNombre = usuarioDTO.PrimerNombre;
        usuario.SegundoNombre = usuarioDTO.SegundoNombre;
        usuario.PrimerApellido = usuarioDTO.PrimerApellido;
        usuario.SegundoApellido = usuarioDTO.SegundoApellido;
        usuario.NumeroDocumento = usuarioDTO.NumeroDocumento;
        usuario.TipoDocumentoId = usuarioDTO.TipoDocumentoId;
        usuario.Email = usuarioDTO.Email;
        usuario.Username = usuarioDTO.Username;
        usuario.Password = _passwordHasher.HashPassword(usuarioDTO.Password);
        usuario.RolId = usuarioDTO.RolId;
        usuario.Activo = usuarioDTO.Activo;
        usuario.FechaActualizacion = DateTime.UtcNow;

        _dbContext.Usuarios.Update(usuario);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarUsuarioDTO
        {
            Id = usuario.Id,
            PrimerNombre = usuario.PrimerNombre,
            SegundoNombre = usuario.SegundoNombre,
            PrimerApellido = usuario.PrimerApellido,
            SegundoApellido = usuario.SegundoApellido,
            NumeroDocumento = usuario.NumeroDocumento,
            TipoDocumentoId = usuario.TipoDocumentoId,
            Email = usuario.Email,
            Username = usuario.Username,
            RolId = usuario.RolId,
            FechaCreacion = usuario.FechaCreacion,
            FechaActualizacion = usuario.FechaActualizacion,
            Activo = usuario.Activo
        };
    }

    public async Task<ListarUsuarioDTO> CrearUsuario(CrearUsuarioDTO usuarioDTO, CancellationToken cancellationToken)
    {
        Usuario usuario = new()
        {
            PrimerNombre = usuarioDTO.PrimerNombre,
            SegundoNombre = usuarioDTO.SegundoNombre,
            PrimerApellido = usuarioDTO.PrimerApellido,
            SegundoApellido = usuarioDTO.SegundoApellido,
            NumeroDocumento = usuarioDTO.NumeroDocumento,
            TipoDocumentoId = usuarioDTO.TipoDocumentoId,
            Email = usuarioDTO.Email,
            Username = usuarioDTO.Username,
            Password = _passwordHasher.HashPassword(usuarioDTO.Password),
            RolId = usuarioDTO.RolId
        };

        _dbContext.Usuarios.Add(usuario);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarUsuarioDTO
        {
            Id = usuario.Id,
            PrimerNombre = usuario.PrimerNombre,
            SegundoNombre = usuario.SegundoNombre,
            PrimerApellido = usuario.PrimerApellido,
            SegundoApellido = usuario.SegundoApellido,
            NumeroDocumento = usuario.NumeroDocumento,
            TipoDocumentoId = usuario.TipoDocumentoId,
            Email = usuario.Email,
            Username = usuario.Username,
            RolId = usuario.RolId,
            FechaCreacion = usuario.FechaCreacion,
            FechaActualizacion = usuario.FechaActualizacion
        };
    }

    public async Task<Result<bool, UsuarioNoEncontradoException>> EliminarUsuario(Guid usuarioId, CancellationToken cancellationToken)
    {
        Usuario? usuario = await _dbContext.Usuarios.FindAsync([usuarioId], cancellationToken: cancellationToken);

        if (usuario is null)
        {
            return new UsuarioNoEncontradoException(usuarioId);
        }

        usuario.Activo = false;
        _dbContext.Usuarios.Update(usuario);
        
        int updatedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return updatedRows > 0;
    }

    public async Task<Result<ListarUsuarioDTO, UsuarioNoEncontradoException>> ObtenerUsuario(Guid usuarioId, CancellationToken cancellationToken)
    {
        Usuario? usuario = await _dbContext.Usuarios.FindAsync([usuarioId], cancellationToken: cancellationToken);

        if (usuario is null)
        {
            return new UsuarioNoEncontradoException(usuarioId);
        }

        return new ListarUsuarioDTO
        {
            Id = usuario.Id,
            PrimerNombre = usuario.PrimerNombre,
            SegundoNombre = usuario.SegundoNombre,
            PrimerApellido = usuario.PrimerApellido,
            SegundoApellido = usuario.SegundoApellido,
            NumeroDocumento = usuario.NumeroDocumento,
            TipoDocumentoId = usuario.TipoDocumentoId,
            Email = usuario.Email,
            Username = usuario.Username,
            RolId = usuario.RolId,
            FechaCreacion = usuario.FechaCreacion,
            FechaActualizacion = usuario.FechaActualizacion,
            Activo = usuario.Activo
        };
    }

    public async Task<Result<ListarUsuarioDTO, UsuarioNoEncontradoException>> ObtenerUsuario(string nombreUsuario, CancellationToken cancellationToken)
    {
        Usuario? usuario = await _dbContext.Usuarios.Where(u => u.Username == nombreUsuario).FirstOrDefaultAsync(cancellationToken);

        if (usuario is null)
        {
            return new UsuarioNoEncontradoException(nombreUsuario);
        }

        return new ListarUsuarioDTO
        {
            Id = usuario.Id,
            PrimerNombre = usuario.PrimerNombre,
            SegundoNombre = usuario.SegundoNombre,
            PrimerApellido = usuario.PrimerApellido,
            SegundoApellido = usuario.SegundoApellido,
            NumeroDocumento = usuario.NumeroDocumento,
            TipoDocumentoId = usuario.TipoDocumentoId,
            Email = usuario.Email,
            Username = usuario.Username,
            RolId = usuario.RolId,
            FechaCreacion = usuario.FechaCreacion,
            FechaActualizacion = usuario.FechaActualizacion,
            Activo = usuario.Activo
        };
    }

    public async Task<ICollection<ListarUsuarioDTO>> ObtenerUsuarios(CancellationToken cancellationToken)
    {
        List<ListarUsuarioDTO> usuarios = await _dbContext.Usuarios
            .Select(u => new ListarUsuarioDTO
            {
                Id = u.Id,
                PrimerNombre = u.PrimerNombre,
                SegundoNombre = u.SegundoNombre,
                PrimerApellido = u.PrimerApellido,
                SegundoApellido = u.SegundoApellido,
                NumeroDocumento = u.NumeroDocumento,
                TipoDocumentoId = u.TipoDocumentoId,
                Email = u.Email,
                Username = u.Username,
                RolId = u.RolId,
                FechaCreacion = u.FechaCreacion,
                FechaActualizacion = u.FechaActualizacion,
                Activo = u.Activo
            })
            .ToListAsync(cancellationToken);

        return usuarios;
    }
}
