using AutoCita.Api.DTOs.Cita;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ICitaRepository _citaRepository;

        public CitaController(ICitaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCitas(CancellationToken cancellationToken)
        {
            var citas = await _citaRepository.ObtenerCitas(cancellationToken);
            return Ok(citas);
        }

        [HttpGet]
        [Route("{citaId}")]
        public async Task<IActionResult> ObtenerCita(Guid citaId, CancellationToken cancellationToken)
        {
            var resultado = await _citaRepository.ObtenerCita(citaId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }

        [HttpDelete]
        [Route("{citaId}")]
        public async Task<IActionResult> EliminarCita(Guid citaId, CancellationToken cancellationToken)
        {
            var resultado = await _citaRepository.EliminarCita(citaId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return resultado.Value ? NoContent() : BadRequest(new { Message = "No se pudo eliminar la cita" });
        }

        [HttpPost]
        public async Task<IActionResult> CrearCita(CrearCitaDTO citaDTO, CancellationToken cancellationToken)
        {
            var resultado = await _citaRepository.CrearCita(citaDTO, cancellationToken);
            return CreatedAtAction(nameof(ObtenerCita), new { citaId = resultado.Id }, resultado);
        }

        [HttpPut]
        [Route("{citaId}")]
        public async Task<IActionResult> ActualizarCita(Guid citaId, [FromBody] ActualizarCitaDTO citaDTO, CancellationToken cancellationToken)
        {
            var resultado = await _citaRepository.ActualizarCita(citaId, citaDTO, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }
    }
}
