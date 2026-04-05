using AutoCita.Api.DTOs.TipoDocumentos;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentosRepository _tipoDocumentosRepository;

        public TipoDocumentoController(ITipoDocumentosRepository tipoDocumentosRepository)
        {
            _tipoDocumentosRepository = tipoDocumentosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTiposDocumentos(CancellationToken cancellationToken)
        {
            var tiposDocumentos = await _tipoDocumentosRepository.ObtenerTiposDocumentos(cancellationToken);
            return Ok(tiposDocumentos);
        }

        [HttpGet]
        [Route("{tipoDocumentoId}")]
        public async Task<IActionResult> ObtenerTipoDocumento(int tipoDocumentoId, CancellationToken cancellationToken)
        {
            var resultado = await _tipoDocumentosRepository.ObtenerTipoDocumento(tipoDocumentoId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }

        [HttpDelete]
        [Route("{tipoDocumentoId}")]
        public async Task<IActionResult> EliminarTipoDocumento(int tipoDocumentoId, CancellationToken cancellationToken)
        {
            var resultado = await _tipoDocumentosRepository.EliminarTipoDocumento(tipoDocumentoId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return resultado.Value ? NoContent() : BadRequest(new { Message = "No se pudo eliminar el tipo de documento" });
        }

        [HttpPost]
        public async Task<IActionResult> CrearTipoDocumento(CrearTipoDocumentoDTO tipoDocumentoDTO, CancellationToken cancellationToken)
        {
            var resultado = await _tipoDocumentosRepository.CrearTipoDocumento(tipoDocumentoDTO, cancellationToken);
            return CreatedAtAction(nameof(ObtenerTipoDocumento), new { tipoDocumentoId = resultado.Id }, resultado);
        }

        [HttpPut]
        [Route("{tipoDocumentoId}")]
        public async Task<IActionResult> ActualizarTipoDocumento(int tipoDocumentoId, [FromBody] ActualizarTipoDocumentoDTO tipoDocumentoDTO, CancellationToken cancellationToken)
        {
            var resultado = await _tipoDocumentosRepository.ActualizarTipoDocumento(tipoDocumentoId, tipoDocumentoDTO, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }
    }
}
