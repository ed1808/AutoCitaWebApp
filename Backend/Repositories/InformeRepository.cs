using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Informe;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoCita.Api.Repositories;

public class InformeRepository(AppDbContext context) : IInformeRepository
{
    private readonly AppDbContext _context = context;

    public async Task<ResumenCitasDTO> ObtenerResumenCitas(DateOnly fecha, CancellationToken cancellationToken)
    {
        var fechaInicio = fecha.ToDateTime(TimeOnly.MinValue);
        var fechaFin = fecha.ToDateTime(TimeOnly.MaxValue);
        var ahora = DateTime.Now;

        var citas = await _context.Citas
            .Where(c => c.FechaInicio >= fechaInicio && c.FechaInicio <= fechaFin)
            .Select(c => new { c.EstadoCita, c.FechaInicio })
            .ToListAsync(cancellationToken);

        var ingresadas = citas.Count(c => c.EstadoCita == "ingresada");
        var pendientes = citas.Count(c =>
            (c.EstadoCita == "programada" || c.EstadoCita == "pendiente") && c.FechaInicio >= ahora);
        var retrasadas = citas.Count(c =>
            c.EstadoCita == "retrasada" ||
            (c.EstadoCita == "programada" && c.FechaInicio < ahora));
        var canceladas = citas.Count(c => c.EstadoCita == "cancelada");

        return new ResumenCitasDTO
        {
            Fecha = fecha,
            TotalCitas = citas.Count,
            Ingresadas = ingresadas,
            Pendientes = pendientes,
            Retrasadas = retrasadas,
            Canceladas = canceladas
        };
    }

    public async Task<CargaTallerDTO> ObtenerCargaTaller(DateOnly fecha, CancellationToken cancellationToken)
    {
        var fechaInicio = fecha.ToDateTime(TimeOnly.MinValue);
        var fechaFin = fecha.ToDateTime(TimeOnly.MaxValue);

        var agendas = await _context.Agendas
            .Include(a => a.Usuario)
            .Where(a => a.Fecha >= fechaInicio && a.Fecha <= fechaFin)
            .ToListAsync(cancellationToken);

        var capacidadTotal = agendas.Sum(a => a.CapacidadHoras);
        var cargaTotal = agendas.Sum(a => a.CargaActual);
        var porcentajeGlobal = capacidadTotal > 0
            ? Math.Round(cargaTotal / capacidadTotal * 100, 2)
            : 0m;

        var tecnicos = agendas.Select(a => new CargaTecnicoDTO
        {
            UsuarioId = a.UsuarioId,
            NombreTecnico = $"{a.Usuario.PrimerNombre} {a.Usuario.PrimerApellido}".Trim(),
            CapacidadHoras = a.CapacidadHoras,
            CargaActual = a.CargaActual,
            PorcentajeOcupacion = a.CapacidadHoras > 0
                ? Math.Round(a.CargaActual / a.CapacidadHoras * 100, 2)
                : 0m,
            EstadoAgenda = a.EstadoAgenda
        }).ToList();

        return new CargaTallerDTO
        {
            Fecha = fecha,
            TotalAgendas = agendas.Count,
            CapacidadTotalHoras = capacidadTotal,
            CargaTotalHoras = cargaTotal,
            PorcentajeOcupacionGlobal = porcentajeGlobal,
            Tecnicos = tecnicos
        };
    }

    public async Task<ServiciosMasSolicitadosDTO> ObtenerServiciosMasSolicitados(
        string periodo, DateOnly fecha, int top, CancellationToken cancellationToken)
    {
        var (fechaInicio, fechaFin) = CalcularRango(periodo, fecha);

        var dtInicio = fechaInicio.ToDateTime(TimeOnly.MinValue);
        var dtFin = fechaFin.ToDateTime(TimeOnly.MaxValue);

        var servicios = await _context.Servicios
            .Select(s => new ServicioSolicitadoDTO
            {
                ServicioId = s.Id,
                NombreServicio = s.Nombre,
                TotalCitas = s.Citas.Count(c =>
                    c.FechaInicio >= dtInicio &&
                    c.FechaInicio <= dtFin &&
                    c.EstadoCita != "cancelada")
            })
            .Where(s => s.TotalCitas > 0)
            .OrderByDescending(s => s.TotalCitas)
            .Take(top)
            .ToListAsync(cancellationToken);

        return new ServiciosMasSolicitadosDTO
        {
            Periodo = periodo,
            FechaInicio = fechaInicio,
            FechaFin = fechaFin,
            Servicios = servicios
        };
    }

    private static (DateOnly inicio, DateOnly fin) CalcularRango(string periodo, DateOnly fecha)
    {
        return periodo switch
        {
            "dia" => (fecha, fecha),
            "semana" => (
                fecha.AddDays(-(int)fecha.DayOfWeek == 0 ? 6 : (int)fecha.DayOfWeek - 1),
                fecha.AddDays((int)fecha.DayOfWeek == 0 ? 0 : 7 - (int)fecha.DayOfWeek)
            ),
            "mes" => (
                new DateOnly(fecha.Year, fecha.Month, 1),
                new DateOnly(fecha.Year, fecha.Month, DateTime.DaysInMonth(fecha.Year, fecha.Month))
            ),
            _ => throw new ArgumentException($"Periodo no válido: {periodo}")
        };
    }
}
