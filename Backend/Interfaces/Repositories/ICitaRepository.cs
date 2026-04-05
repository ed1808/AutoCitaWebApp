using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.Cita;

namespace AutoCita.Api.Interfaces.Repositories;

public interface ICitaRepository
{
    Task<ListarCitaDTO> CrearCita(CrearCitaDTO citaDTO, CancellationToken cancellationToken);
    Task<ICollection<ListarCitaDTO>> ObtenerCitas(CancellationToken cancellationToken);
    Task<Result<ListarCitaDTO, CitaNoEncontradaException>> ObtenerCita(Guid citaId, CancellationToken cancellationToken);
    Task<Result<ListarCitaDTO, CitaNoEncontradaException>> ActualizarCita(Guid citaId, ActualizarCitaDTO citaDTO, CancellationToken cancellationToken);
    Task<Result<bool, CitaNoEncontradaException>> EliminarCita(Guid citaId, CancellationToken cancellationToken);
}
