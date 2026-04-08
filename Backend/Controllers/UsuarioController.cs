using AutoCita.Api.DTOs.Usuario;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios(CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.ObtenerUsuarios(cancellationToken);
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("{usuarioId}")]
        public async Task<IActionResult> ObtenerUsuario(Guid usuarioId, CancellationToken cancellationToken)
        {
            var resultado = await _usuarioRepository.ObtenerUsuario(usuarioId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }

        [HttpDelete]
        [Route("{usuarioId}")]
        public async Task<IActionResult> EliminarUsuario(Guid usuarioId, CancellationToken cancellationToken)
        {
            var resultado = await _usuarioRepository.EliminarUsuario(usuarioId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return resultado.Value ? NoContent() : BadRequest(new { Message = "No se pudo eliminar el usuario" });
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioDTO usuarioDTO, CancellationToken cancellationToken)
        {
            var resultado = await _usuarioRepository.CrearUsuario(usuarioDTO, cancellationToken);
            return CreatedAtAction(nameof(ObtenerUsuario), new { usuarioId = resultado.Id }, resultado);
        }

        [HttpPut]
        [Route("{usuarioId}")]
        public async Task<IActionResult> ActualizarUsuario(Guid usuarioId, [FromBody] ActualizarUsuarioDTO usuarioDTO, CancellationToken cancellationToken)
        {
            var resultado = await _usuarioRepository.ActualizarUsuario(usuarioId, usuarioDTO, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }
    }
}
