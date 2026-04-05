using AutoCita.Api.DTOs.Agenda;
using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepository _agendaRepository;

        public AgendaController(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAgendas(CancellationToken cancellationToken)
        {
            var agendas = await _agendaRepository.ObtenerAgendas(cancellationToken);
            return Ok(agendas);
        }

        [HttpGet]
        [Route("{agendaId}")]
        public async Task<IActionResult> ObtenerAgenda(Guid agendaId, CancellationToken cancellationToken)
        {
            var resultado = await _agendaRepository.ObtenerAgenda(agendaId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }

        [HttpDelete]
        [Route("{agendaId}")]
        public async Task<IActionResult> EliminarAgenda(Guid agendaId, CancellationToken cancellationToken)
        {
            var resultado = await _agendaRepository.EliminarAgenda(agendaId, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return resultado.Value ? NoContent() : BadRequest(new { Message = "No se pudo eliminar la agenda" });
        }

        [HttpPost]
        public async Task<IActionResult> CrearAgenda(CrearAgendaDTO agendaDTO, CancellationToken cancellationToken)
        {
            var resultado = await _agendaRepository.CrearAgenda(agendaDTO, cancellationToken);
            return CreatedAtAction(nameof(ObtenerAgenda), new { agendaId = resultado.Id }, resultado);
        }

        [HttpPut]
        [Route("{agendaId}")]
        public async Task<IActionResult> ActualizarAgenda(Guid agendaId, [FromBody] ActualizarAgendaDTO agendaDTO, CancellationToken cancellationToken)
        {
            var resultado = await _agendaRepository.ActualizarAgenda(agendaId, agendaDTO, cancellationToken);

            if (!resultado.IsSuccess)
            {
                return NotFound(resultado.Error);
            }

            return Ok(resultado.Value);
        }
    }
}
