namespace AutoCita.Api.Models;

public class Usuario
{
    public Guid Id { get; set; }
    public string PrimerNombre { get; set; } = string.Empty;
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = string.Empty;
    public string? SegundoApellido { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public int TipoDocumentoId { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RolId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
    public bool Activo { get; set; }

    public TipoDocumento TipoDocumento { get; set; } = null!;
    public Rol Rol { get; set; } = null!;
    public List<Agenda> Agendas { get; set; } = [];
    public List<Cita> Citas { get; set; } = [];
}
