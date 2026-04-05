using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Cliente;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Models;

namespace AutoCita.Api.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _dbContext;

    public ClienteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListarClienteDTO, ClienteNoEncontradoException>> ActualizarCliente(Guid clienteId, ActualizarClienteDTO clienteDTO, CancellationToken cancellationToken)
    {
        Cliente? cliente = await _dbContext.Clientes.FindAsync([clienteId], cancellationToken: cancellationToken);

        if (cliente is null)
        {
            return new ClienteNoEncontradoException(clienteId);
        }

        cliente.PrimerNombre = clienteDTO.PrimerNombre;
        cliente.SegundoNombre = clienteDTO.SegundoNombre;
        cliente.PrimerApellido = clienteDTO.PrimerApellido;
        cliente.SegundoApellido = clienteDTO.SegundoApellido;
        cliente.NumeroDocumento = clienteDTO.NumeroDocumento;
        cliente.TipoDocumentoId = clienteDTO.TipoDocumentoId;
        cliente.Email = clienteDTO.Email;
        cliente.Telefono = clienteDTO.Telefono;
        cliente.Direccion = clienteDTO.Direccion;
        cliente.FechaActualizacion = DateTime.UtcNow;

        _dbContext.Clientes.Update(cliente);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarClienteDTO
        {
            Id = cliente.Id,
            PrimerNombre = cliente.PrimerNombre,
            SegundoNombre = cliente.SegundoNombre,
            PrimerApellido = cliente.PrimerApellido,
            SegundoApellido = cliente.SegundoApellido,
            NumeroDocumento = cliente.NumeroDocumento,
            TipoDocumentoId = cliente.TipoDocumentoId,
            Email = cliente.Email,
            Telefono = cliente.Telefono,
            Direccion = cliente.Direccion,
            FechaCreacion = cliente.FechaCreacion,
            FechaActualizacion = cliente.FechaActualizacion
        };
    }

    public async Task<ListarClienteDTO> CrearCliente(CrearClienteDTO clienteDTO, CancellationToken cancellationToken)
    {
        Cliente cliente = new()
        {
            PrimerNombre = clienteDTO.PrimerNombre,
            SegundoNombre = clienteDTO.SegundoNombre,
            PrimerApellido = clienteDTO.PrimerApellido,
            SegundoApellido = clienteDTO.SegundoApellido,
            NumeroDocumento = clienteDTO.NumeroDocumento,
            TipoDocumentoId = clienteDTO.TipoDocumentoId,
            Email = clienteDTO.Email,
            Telefono = clienteDTO.Telefono,
            Direccion = clienteDTO.Direccion
        };

        _dbContext.Clientes.Add(cliente);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarClienteDTO
        {
            Id = cliente.Id,
            PrimerNombre = cliente.PrimerNombre,
            SegundoNombre = cliente.SegundoNombre,
            PrimerApellido = cliente.PrimerApellido,
            SegundoApellido = cliente.SegundoApellido,
            NumeroDocumento = cliente.NumeroDocumento,
            TipoDocumentoId = cliente.TipoDocumentoId,
            Email = cliente.Email,
            Telefono = cliente.Telefono,
            Direccion = cliente.Direccion,
            FechaCreacion = cliente.FechaCreacion,
            FechaActualizacion = cliente.FechaActualizacion
        };
    }

    public async Task<Result<bool, ClienteNoEncontradoException>> EliminarCliente(Guid clienteId, CancellationToken cancellationToken)
    {
        Cliente? cliente = await _dbContext.Clientes.FindAsync([clienteId], cancellationToken: cancellationToken);

        if (cliente is null)
        {
            return new ClienteNoEncontradoException(clienteId);
        }

        _dbContext.Clientes.Remove(cliente);
        int deletedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return deletedRows > 0;
    }

    public async Task<Result<ListarClienteDTO, ClienteNoEncontradoException>> ObtenerCliente(Guid clienteId, CancellationToken cancellationToken)
    {
        Cliente? cliente = await _dbContext.Clientes.FindAsync([clienteId], cancellationToken: cancellationToken);

        if (cliente is null)
        {
            return new ClienteNoEncontradoException(clienteId);
        }

        return new ListarClienteDTO
        {
            Id = cliente.Id,
            PrimerNombre = cliente.PrimerNombre,
            SegundoNombre = cliente.SegundoNombre,
            PrimerApellido = cliente.PrimerApellido,
            SegundoApellido = cliente.SegundoApellido,
            NumeroDocumento = cliente.NumeroDocumento,
            TipoDocumentoId = cliente.TipoDocumentoId,
            Email = cliente.Email,
            Telefono = cliente.Telefono,
            Direccion = cliente.Direccion,
            FechaCreacion = cliente.FechaCreacion,
            FechaActualizacion = cliente.FechaActualizacion
        };
    }

    public async Task<ICollection<ListarClienteDTO>> ObtenerClientes(CancellationToken cancellationToken)
    {
        List<ListarClienteDTO> clientes = await _dbContext.Clientes
            .Select(c => new ListarClienteDTO
            {
                Id = c.Id,
                PrimerNombre = c.PrimerNombre,
                SegundoNombre = c.SegundoNombre,
                PrimerApellido = c.PrimerApellido,
                SegundoApellido = c.SegundoApellido,
                NumeroDocumento = c.NumeroDocumento,
                TipoDocumentoId = c.TipoDocumentoId,
                Email = c.Email,
                Telefono = c.Telefono,
                Direccion = c.Direccion,
                FechaCreacion = c.FechaCreacion,
                FechaActualizacion = c.FechaActualizacion
            })
            .ToListAsync(cancellationToken);

        return clientes;
    }
}
