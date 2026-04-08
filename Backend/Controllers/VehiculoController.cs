using AutoCita.Api.DTOs.Vehiculo;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculoController(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVehiculos(CancellationToken cancellationToken)
        {
            var vehiculos = await _vehiculoRepository.ObtenerVehiculos(cancellationToken);
            return Ok(vehiculos);
        }

        [HttpGet]
        [Route("{vehiculoId}")]
        public async Task<IActionResult> ObtenerVehiculo(Guid vehiculoId, CancellationToken cancellationToken)
        {
            var resultado = await _vehiculoRepository.ObtenerVehiculo(vehiculoId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }

        [HttpDelete]
        [Route("{vehiculoId}")]
        public async Task<IActionResult> EliminarVehiculo(Guid vehiculoId, CancellationToken cancellationToken)
        {
            var resultado = await _vehiculoRepository.EliminarVehiculo(vehiculoId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return resultado.Value ? NoContent() : BadRequest(new { Message = "No se pudo eliminar el vehículo" });
        }

        [HttpPost]
        public async Task<IActionResult> CrearVehiculo(CrearVehiculoDTO vehiculoDTO, CancellationToken cancellationToken)
        {
            var resultado = await _vehiculoRepository.CrearVehiculo(vehiculoDTO, cancellationToken);
            return CreatedAtAction(nameof(ObtenerVehiculo), new { vehiculoId = resultado.Id }, resultado);
        }

        [HttpPut]
        [Route("{vehiculoId}")]
        public async Task<IActionResult> ActualizarVehiculo(Guid vehiculoId, [FromBody] ActualizarVehiculoDTO vehiculoDTO, CancellationToken cancellationToken)
        {
            var resultado = await _vehiculoRepository.ActualizarVehiculo(vehiculoId, vehiculoDTO, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }
    }
}
