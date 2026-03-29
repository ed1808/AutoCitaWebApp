using AutoCita.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoCita.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    private const string uuidV7 = "uuidv7()";
    private const string currentTimestamp = "CURRENT_TIMESTAMP";
    private const string timestampWithoutTimeZone = "timestamp without time zone";

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
            entity.HasKey(e => e.Id)
                    .HasName("PK_Agenda_Id");
            
            entity.ToTable("Agendas");

            entity.Property(e => e.Id)
                    .HasDefaultValueSql(uuidV7);

            entity.Property(e => e.CapacidadHoras)
                    .HasPrecision(4, 2);

            entity.Property(e => e.CargaActual)
                    .HasPrecision(4, 2)
                    .HasDefaultValue(0m);

            entity.Property(e => e.EstadoAgenda)
                    .HasMaxLength(20)
                    .HasDefaultValue("disponible");

            entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Agendas)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Agenda_Usuario");
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.Id)
                    .HasName("PK_Cita_Id");

            entity.ToTable("Citas");

            entity.Property(e => e.Id)
                    .HasDefaultValueSql(uuidV7);

            entity.Property(e => e.FechaInicio)
                    .HasColumnType(timestampWithoutTimeZone);
            
            entity.Property(e => e.FechaFin)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.Property(e => e.DuracionEst)
                    .HasPrecision(4, 2);

            entity.Property(e => e.EstadoCita)
                    .HasMaxLength(20)
                    .HasDefaultValue("programada");

            entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Citas)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Cita_Usuario");

            entity.HasOne(e => e.Agenda)
                    .WithMany(a => a.Citas)
                    .HasForeignKey(e => e.AgendaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Cita_Agenda");

            entity.HasOne(e => e.Vehiculo)
                    .WithMany(v => v.Citas)
                    .HasForeignKey(e => e.VehiculoId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Cita_Vehiculo");

            entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Citas)
                    .HasForeignKey(e => e.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Cita_Cliente");

            entity.HasMany(e => e.Servicios)
                    .WithMany(s => s.Citas)
                    .UsingEntity<CitaServicio>(
                        j => j.Property(e => e.Cantidad).HasDefaultValue(1));
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id)
                    .HasName("PK_Cliente_Id");

            entity.HasIndex(e => e.NumeroDocumento)
                    .IsUnique();

            entity.HasIndex(e => e.Email)
                    .IsUnique();

            entity.ToTable("Clientes");

            entity.Property(e => e.Id)
                    .HasDefaultValueSql(uuidV7);

            entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(150)
                    .IsRequired();

            entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(150);

            entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(150)
                    .IsRequired();

            entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(150);

            entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(20)
                    .IsRequired();

            entity.Property(e => e.Email)
                    .HasMaxLength(200);

            entity.Property(e => e.Telefono)
                    .HasMaxLength(20);

            entity.Property(e => e.Direccion)
                    .HasColumnType("text");

            entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.Property(e => e.FechaActualizacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.HasOne(e => e.TipoDocumento)
                    .WithMany(t => t.Clientes)
                    .HasForeignKey(e => e.TipoDocumentoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Cliente_TipoDocumento");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id)
                    .HasName("PK_Rol_Id");

            entity.ToTable("Roles");

            entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsRequired();
            
            entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id)
                    .HasName("PK_Servicio_Id");

            entity.ToTable("Servicios");

            entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsRequired();

            entity.Property(e => e.DuracionEstimada)
                    .HasPrecision(4, 2);

            entity.Property(e => e.PrecioSugerido)
                    .HasPrecision(12, 2);

            entity.Property(e => e.Activo)
                    .HasDefaultValue(true);
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id)
                    .HasName("PK_TipoDocumento_Id");

            entity.ToTable("TiposDocumento");

            entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsRequired();

            entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id)
                    .HasName("PK_Usuario_Id");

            entity.HasIndex(e => e.NumeroDocumento)
                    .IsUnique();

            entity.HasIndex(e => e.Username)
                    .IsUnique();

            entity.ToTable("Usuarios");

            entity.Property(e => e.Id)
                    .HasDefaultValueSql(uuidV7);

            entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(150)
                    .IsRequired();

            entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(150);

            entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(150)
                    .IsRequired();

            entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(150);
            
            entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(20)
                    .IsRequired();

            entity.Property(e => e.Email)
                    .HasMaxLength(200);

            entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsRequired();

            entity.Property(e => e.Password)
                    .HasColumnType("text")
                    .IsRequired();

            entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.Property(e => e.FechaActualizacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.Property(e => e.Activo)
                    .HasDefaultValue(true);

            entity.HasOne(e => e.TipoDocumento)
                    .WithMany(t => t.Usuarios)
                    .HasForeignKey(e => e.TipoDocumentoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Usuario_TipoDocumento");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Id)
                    .HasName("PK_Vehiculo_Id");

            entity.HasIndex(e => e.Placa)
                    .IsUnique();

            entity.HasIndex(e => e.Vin)
                    .IsUnique();

            entity.ToTable("Vehiculos");

            entity.Property(e => e.Id)
                    .HasDefaultValueSql(uuidV7);

            entity.Property(e => e.Placa)
                    .HasMaxLength(6)
                    .IsRequired();

            entity.Property(e => e.Vin)
                    .HasMaxLength(17)
                    .IsRequired();
            
            entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsRequired();

            entity.Property(e => e.Linea)
                    .HasMaxLength(50)
                    .IsRequired();

            entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.Property(e => e.FechaActualizacion)
                    .HasDefaultValueSql(currentTimestamp)
                    .HasColumnType(timestampWithoutTimeZone);

            entity.HasOne(e => e.Propietario)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(e => e.PropietarioId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Vehiculo_Cliente");
        });
    }
}
