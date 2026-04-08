namespace AutoCita.Api.DTOs.Usuario;

public class UsuarioLoginRespuestaDTO
{
    public string? Token { get; set; }
    public string Mensaje { get; set; } = null!;
}
