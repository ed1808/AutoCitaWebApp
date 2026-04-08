using AutoCita.Api.DTOs.Rol;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _rolRepository;

        public RolController(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRoles(CancellationToken cancellationToken)
        {
            var roles = await _rolRepository.ObtenerRoles(cancellationToken);

            return Ok(roles);
        }

        [HttpGet]
        [Route("{rolId}")]
        public async Task<IActionResult> ObtenerRol(int rolId, CancellationToken cancellationToken)
        {
            var resultado = await _rolRepository.ObtenerRol(rolId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }

        [HttpDelete]
        [Route("{rolId}")]
        public async Task<IActionResult> EliminarRol(int rolId, CancellationToken cancellationToken)
        {
            var resultado = await _rolRepository.EliminarRol(rolId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return resultado.Value ? NoContent() : BadRequest(new { Message = "No se pudo eliminar el rol" });
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(CrearRolDTO rolDTO, CancellationToken cancellationToken)
        {
            var resultado = await _rolRepository.CrearRol(rolDTO, cancellationToken);

            return CreatedAtAction(nameof(ObtenerRol), new { rolId = resultado.Id }, resultado);
        }

        [HttpPut]
        [Route("{rolId}")]
        public async Task<IActionResult> ActualizarRol(int rolId, [FromBody] ActualizarRolDTO rolDTO, CancellationToken cancellationToken)
        {
            var resultado = await _rolRepository.ActualizarRol(rolId, rolDTO, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }
    }
}
