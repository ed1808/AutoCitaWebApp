using AutoCita.Api.DTOs.Usuario;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionRepository _autenticacionRepository;

        public AutenticacionController(IAutenticacionRepository autenticacionRepository)
        {
            _autenticacionRepository = autenticacionRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDTO, CancellationToken cancellationToken)
        {
            var resultado = await _autenticacionRepository.Login(loginDTO, cancellationToken);

            return string.IsNullOrEmpty(resultado.Token) ? BadRequest(resultado) : Ok(resultado);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistrarUsuarioDTO registroDTO, CancellationToken cancellationToken)
        {
            var resultado = await _autenticacionRepository.RegistrarUsuario(registroDTO, cancellationToken);

            return resultado.Mensaje == "Usuario registrado exitosamente." ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
