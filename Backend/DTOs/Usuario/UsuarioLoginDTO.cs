using System;

namespace AutoCita.Api.DTOs.Usuario;

public class UsuarioLoginDTO
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
