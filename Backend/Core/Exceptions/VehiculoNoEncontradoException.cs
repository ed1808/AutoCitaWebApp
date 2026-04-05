namespace AutoCita.Api.Core.Exceptions;

public class VehiculoNoEncontradoException : Exception
{
    public VehiculoNoEncontradoException(Guid vehiculoId) : base($"El vehículo {vehiculoId} no ha sido encontrado") {}
}
