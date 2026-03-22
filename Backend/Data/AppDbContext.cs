using AutoCita.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoCita.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    private const string uuidV7 = "uuidv7()";

    public DbSet<Agenda> Agendas { get; set; }
    public DbSet<Cita> Citas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<TipoDocumento> TipoDocumentos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Agenda>(entity =>
        {
            
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            
        });
    }
}
