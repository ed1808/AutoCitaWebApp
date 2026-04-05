using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.DTOs.Cliente;

namespace AutoCita.Api.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<ListarClienteDTO> CrearCliente(CrearClienteDTO clienteDTO, CancellationToken cancellationToken);
    Task<ICollection<ListarClienteDTO>> ObtenerClientes(CancellationToken cancellationToken);
    Task<Result<ListarClienteDTO, ClienteNoEncontradoException>> ObtenerCliente(Guid clienteId, CancellationToken cancellationToken);
    Task<Result<ListarClienteDTO, ClienteNoEncontradoException>> ActualizarCliente(Guid clienteId, ActualizarClienteDTO clienteDTO, CancellationToken cancellationToken);
    Task<Result<bool, ClienteNoEncontradoException>> EliminarCliente(Guid clienteId, CancellationToken cancellationToken);
}
