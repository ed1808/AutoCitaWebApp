using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.TipoDocumentos;

namespace AutoCita.Api.Interfaces.Repositories;

public interface ITipoDocumentosRepository
{
    Task<ListarTipoDocumentoDTO> CrearTipoDocumento(CrearTipoDocumentoDTO tipoDocumentoDTO, CancellationToken cancellationToken);
    Task<ICollection<ListarTipoDocumentoDTO>> ObtenerTiposDocumentos(CancellationToken cancellationToken);
    Task<Result<ListarTipoDocumentoDTO, TipoDocumentoNoEncontradoException>> ObtenerTipoDocumento(int tipoDocumentoId, CancellationToken cancellationToken);
    Task<Result<ListarTipoDocumentoDTO, TipoDocumentoNoEncontradoException>> ActualizarTipoDocumento(int tipoDocumentoId, ActualizarTipoDocumentoDTO tipoDocumentoDTO, CancellationToken cancellationToken);
    Task<Result<bool, TipoDocumentoNoEncontradoException>> EliminarTipoDocumento(int tipoDocumentoId, CancellationToken cancellationToken);
}
