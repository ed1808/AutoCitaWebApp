using AutoCita.Api.DTOs.Cliente;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerClientes(CancellationToken cancellationToken)
        {
            var clientes = await _clienteRepository.ObtenerClientes(cancellationToken);
            return Ok(clientes);
        }

        [HttpGet]
        [Route("{clienteId}")]
        public async Task<IActionResult> ObtenerCliente(Guid clienteId, CancellationToken cancellationToken)
        {
            var resultado = await _clienteRepository.ObtenerCliente(clienteId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }

        [HttpDelete]
        [Route("{clienteId}")]
        public async Task<IActionResult> EliminarCliente(Guid clienteId, CancellationToken cancellationToken)
        {
            var resultado = await _clienteRepository.EliminarCliente(clienteId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return resultado.Value ? NoContent() : BadRequest(new { Message = "No se pudo eliminar el cliente" });
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(CrearClienteDTO clienteDTO, CancellationToken cancellationToken)
        {
            var resultado = await _clienteRepository.CrearCliente(clienteDTO, cancellationToken);
            return CreatedAtAction(nameof(ObtenerCliente), new { clienteId = resultado.Id }, resultado);
        }

        [HttpPut]
        [Route("{clienteId}")]
        public async Task<IActionResult> ActualizarCliente(Guid clienteId, [FromBody] ActualizarClienteDTO clienteDTO, CancellationToken cancellationToken)
        {
            var resultado = await _clienteRepository.ActualizarCliente(clienteId, clienteDTO, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }
    }
}
