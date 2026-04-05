using AutoCita.Api.DTOs.Servicio;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioController(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerServicios(CancellationToken cancellationToken)
        {
            var servicios = await _servicioRepository.ObtenerServicios(cancellationToken);
            return Ok(servicios);
        }

        [HttpGet]
        [Route("{servicioId}")]
        public async Task<IActionResult> ObtenerServicio(int servicioId, CancellationToken cancellationToken)
        {
            var resultado = await _servicioRepository.ObtenerServicio(servicioId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }

        [HttpDelete]
        [Route("{servicioId}")]
        public async Task<IActionResult> EliminarServicio(int servicioId, CancellationToken cancellationToken)
        {
            var resultado = await _servicioRepository.EliminarServicio(servicioId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return resultado.Value ? NoContent() : BadRequest(new { Message = "No se pudo eliminar el servicio" });
        }

        [HttpPost]
        public async Task<IActionResult> CrearServicio(CrearServicioDTO servicioDTO, CancellationToken cancellationToken)
        {
            var resultado = await _servicioRepository.CrearServicio(servicioDTO, cancellationToken);
            return CreatedAtAction(nameof(ObtenerServicio), new { servicioId = resultado.Id }, resultado);
        }

        [HttpPut]
        [Route("{servicioId}")]
        public async Task<IActionResult> ActualizarServicio(int servicioId, [FromBody] ActualizarServicioDTO servicioDTO, CancellationToken cancellationToken)
        {
            var resultado = await _servicioRepository.ActualizarServicio(servicioId, servicioDTO, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }
    }
}
