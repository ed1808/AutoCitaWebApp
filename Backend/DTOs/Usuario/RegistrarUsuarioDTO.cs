namespace AutoCita.Api.DTOs.Usuario;

public class RegistrarUsuarioDTO
{
    public string PrimerNombre { get; set; } = null!;
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = null!;
    public string? SegundoApellido { get; set; }
    public string NumeroDocumento { get; set; } = null!;
    public int TipoDocumentoId { get; set; }
    public string? Email { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
