namespace AutoCita.Api.Models;

public class Usuario
{
    public Guid Id { get; set; }
    public string PrimerNombre { get; set; } = null!;
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = null!;
    public string? SegundoApellido { get; set; }
    public string NumeroDocumento { get; set; } = null!;
    public int TipoDocumentoId { get; set; }
    public string? Email { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RolId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
    public bool Activo { get; set; }

    public TipoDocumento TipoDocumento { get; set; } = null!;
    public Rol Rol { get; set; } = null!;
    public ICollection<Agenda> Agendas { get; set; } = [];
    public ICollection<Cita> Citas { get; set; } = [];
}
