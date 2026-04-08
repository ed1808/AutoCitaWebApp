using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Usuario;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Models;
using AutoCita.Api.Core.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AutoCita.Api.Repositories;

public class AutenticacionRepository : IAutenticacionRepository
{
    private readonly AppDbContext _dbContext;
    private readonly PasswordHasher _passwordHasher;
    private readonly string _secretKey;


    public AutenticacionRepository(AppDbContext dbContext, PasswordHasher passwordHasher, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _secretKey = configuration.GetValue<string>("ApiSettings:SecretKey") ?? throw new InvalidOperationException("La clave secreta para JWT no está configurada.");
    }

    public async Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO loginDTO, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(loginDTO.Username) || string.IsNullOrEmpty(loginDTO.Password))
        {
            return new UsuarioLoginRespuestaDTO
            {
                Token = "",
                Mensaje = "Usuario y/o contraseña requeridos."
            };
        }

        Usuario? usuario = await _dbContext.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Username == loginDTO.Username, cancellationToken);

        if (usuario is null)
        {
            return new UsuarioLoginRespuestaDTO
            {
                Token = "",
                Mensaje = "Usuario y/o contraseña incorrectos."
            };
        }

        bool isPasswordValid = _passwordHasher.VerifyPassword(loginDTO.Password, usuario.Password);

        if (!isPasswordValid)
        {
            return new UsuarioLoginRespuestaDTO
            {
                Token = "",
                Mensaje = "Usuario y/o contraseña incorrectos."
            };
        }

        var handlerToken = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("username", usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
            ]),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handlerToken.CreateToken(tokenDescriptor);

        return new UsuarioLoginRespuestaDTO
        {
            Token = handlerToken.WriteToken(token),
            Mensaje = "Usuario logueado exitosamente."
        };
    }

    public async Task<RegistrarUsuarioRespuestaDTO> RegistrarUsuario(RegistrarUsuarioDTO registroDTO, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(registroDTO.Username) || string.IsNullOrWhiteSpace(registroDTO.Password))
        {
            return new RegistrarUsuarioRespuestaDTO
            {
                Mensaje = "El nombre de usuario y la contraseña son requeridos."
            };
        }

        bool usernameExistente = await _dbContext.Usuarios.AnyAsync(u => u.Username == registroDTO.Username, cancellationToken);

        if (usernameExistente)
        {
            return new RegistrarUsuarioRespuestaDTO
            {
                Mensaje = "El nombre de usuario ya está en uso."
            };
        }

        Rol? rol = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Nombre == "Admin", cancellationToken);

        if (rol is null)
        {
            return new RegistrarUsuarioRespuestaDTO
            {
                Mensaje = "El rol por defecto no está configurado en el sistema."
            };
        }

        Usuario usuario = new()
        {
            PrimerNombre = registroDTO.PrimerNombre,
            SegundoNombre = registroDTO.SegundoNombre,
            PrimerApellido = registroDTO.PrimerApellido,
            SegundoApellido = registroDTO.SegundoApellido,
            NumeroDocumento = registroDTO.NumeroDocumento,
            TipoDocumentoId = registroDTO.TipoDocumentoId,
            Email = registroDTO.Email,
            Username = registroDTO.Username,
            Password = _passwordHasher.HashPassword(registroDTO.Password),
            RolId = rol.Id,
            Activo = true,
            FechaCreacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow
        };

        _dbContext.Usuarios.Add(usuario);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RegistrarUsuarioRespuestaDTO
        {
            Mensaje = "Usuario registrado exitosamente."
        };
    }
}
