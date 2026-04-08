using AutoCita.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoCita.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Director")]
public class InformeController : ControllerBase
{
    private readonly IInformeRepository _informeRepository;

    public InformeController(IInformeRepository informeRepository)
    {
        _informeRepository = informeRepository;
    }

    [HttpGet("citas")]
    public async Task<IActionResult> ObtenerResumenCitas(
        [FromQuery] DateOnly? fecha,
        CancellationToken cancellationToken)
    {
        var fechaConsulta = fecha ?? DateOnly.FromDateTime(DateTime.Now);
        var resultado = await _informeRepository.ObtenerResumenCitas(fechaConsulta, cancellationToken);
        return Ok(resultado);
    }

    [HttpGet("carga-taller")]
    public async Task<IActionResult> ObtenerCargaTaller(
        [FromQuery] DateOnly? fecha,
        CancellationToken cancellationToken)
    {
        var fechaConsulta = fecha ?? DateOnly.FromDateTime(DateTime.Now);
        var resultado = await _informeRepository.ObtenerCargaTaller(fechaConsulta, cancellationToken);
        return Ok(resultado);
    }

    [HttpGet("servicios-mas-solicitados")]
    public async Task<IActionResult> ObtenerServiciosMasSolicitados(
        [FromQuery] string periodo = "dia",
        [FromQuery] DateOnly? fecha = null,
        [FromQuery] int top = 10,
        CancellationToken cancellationToken = default)
    {
        if (periodo is not ("dia" or "semana" or "mes"))
            return BadRequest("El parámetro 'periodo' debe ser 'dia', 'semana' o 'mes'.");

        if (top <= 0)
            return BadRequest("El parámetro 'top' debe ser mayor que cero.");

        var fechaConsulta = fecha ?? DateOnly.FromDateTime(DateTime.Now);
        var resultado = await _informeRepository.ObtenerServiciosMasSolicitados(
            periodo, fechaConsulta, top, cancellationToken);
        return Ok(resultado);
    }
}
