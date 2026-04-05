using Microsoft.EntityFrameworkCore;

using AutoCita.Api.Core;
using AutoCita.Api.Core.Exceptions;
using AutoCita.Api.Data;
using AutoCita.Api.DTOs.Vehiculo;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Models;

namespace AutoCita.Api.Repositories;

public class VehiculoRepository : IVehiculoRepository
{
    private readonly AppDbContext _dbContext;

    public VehiculoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListarVehiculoDTO, VehiculoNoEncontradoException>> ActualizarVehiculo(Guid vehiculoId, ActualizarVehiculoDTO vehiculoDTO, CancellationToken cancellationToken)
    {
        Vehiculo? vehiculo = await _dbContext.Vehiculos.FindAsync([vehiculoId], cancellationToken: cancellationToken);

        if (vehiculo is null)
        {
            return new VehiculoNoEncontradoException(vehiculoId);
        }

        vehiculo.Placa = vehiculoDTO.Placa;
        vehiculo.Vin = vehiculoDTO.Vin;
        vehiculo.Marca = vehiculoDTO.Marca;
        vehiculo.Linea = vehiculoDTO.Linea;
        vehiculo.Modelo = vehiculoDTO.Modelo;
        vehiculo.Color = vehiculoDTO.Color;
        vehiculo.PropietarioId = vehiculoDTO.PropietarioId;
        vehiculo.FechaActualizacion = DateTime.UtcNow;

        _dbContext.Vehiculos.Update(vehiculo);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarVehiculoDTO
        {
            Id = vehiculo.Id,
            Placa = vehiculo.Placa,
            Vin = vehiculo.Vin,
            Marca = vehiculo.Marca,
            Linea = vehiculo.Linea,
            Modelo = vehiculo.Modelo,
            Color = vehiculo.Color,
            PropietarioId = vehiculo.PropietarioId,
            FechaCreacion = vehiculo.FechaCreacion,
            FechaActualizacion = vehiculo.FechaActualizacion
        };
    }

    public async Task<ListarVehiculoDTO> CrearVehiculo(CrearVehiculoDTO vehiculoDTO, CancellationToken cancellationToken)
    {
        Vehiculo vehiculo = new()
        {
            Placa = vehiculoDTO.Placa,
            Vin = vehiculoDTO.Vin,
            Marca = vehiculoDTO.Marca,
            Linea = vehiculoDTO.Linea,
            Modelo = vehiculoDTO.Modelo,
            Color = vehiculoDTO.Color,
            PropietarioId = vehiculoDTO.PropietarioId
        };

        _dbContext.Vehiculos.Add(vehiculo);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ListarVehiculoDTO
        {
            Id = vehiculo.Id,
            Placa = vehiculo.Placa,
            Vin = vehiculo.Vin,
            Marca = vehiculo.Marca,
            Linea = vehiculo.Linea,
            Modelo = vehiculo.Modelo,
            Color = vehiculo.Color,
            PropietarioId = vehiculo.PropietarioId,
            FechaCreacion = vehiculo.FechaCreacion,
            FechaActualizacion = vehiculo.FechaActualizacion
        };
    }

    public async Task<Result<bool, VehiculoNoEncontradoException>> EliminarVehiculo(Guid vehiculoId, CancellationToken cancellationToken)
    {
        Vehiculo? vehiculo = await _dbContext.Vehiculos.FindAsync([vehiculoId], cancellationToken: cancellationToken);

        if (vehiculo is null)
        {
            return new VehiculoNoEncontradoException(vehiculoId);
        }

        _dbContext.Vehiculos.Remove(vehiculo);
        int deletedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return deletedRows > 0;
    }

    public async Task<Result<ListarVehiculoDTO, VehiculoNoEncontradoException>> ObtenerVehiculo(Guid vehiculoId, CancellationToken cancellationToken)
    {
        Vehiculo? vehiculo = await _dbContext.Vehiculos.FindAsync([vehiculoId], cancellationToken: cancellationToken);

        if (vehiculo is null)
        {
            return new VehiculoNoEncontradoException(vehiculoId);
        }

        return new ListarVehiculoDTO
        {
            Id = vehiculo.Id,
            Placa = vehiculo.Placa,
            Vin = vehiculo.Vin,
            Marca = vehiculo.Marca,
            Linea = vehiculo.Linea,
            Modelo = vehiculo.Modelo,
            Color = vehiculo.Color,
            PropietarioId = vehiculo.PropietarioId,
            FechaCreacion = vehiculo.FechaCreacion,
            FechaActualizacion = vehiculo.FechaActualizacion
        };
    }

    public async Task<ICollection<ListarVehiculoDTO>> ObtenerVehiculos(CancellationToken cancellationToken)
    {
        List<ListarVehiculoDTO> vehiculos = await _dbContext.Vehiculos
            .Select(v => new ListarVehiculoDTO
            {
                Id = v.Id,
                Placa = v.Placa,
                Vin = v.Vin,
                Marca = v.Marca,
                Linea = v.Linea,
                Modelo = v.Modelo,
                Color = v.Color,
                PropietarioId = v.PropietarioId,
                FechaCreacion = v.FechaCreacion,
                FechaActualizacion = v.FechaActualizacion
            })
            .ToListAsync(cancellationToken);

        return vehiculos;
    }
}
