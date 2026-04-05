using System;

namespace AutoCita.Api.Core.Exceptions;

public class TipoDocumentoNoEncontradoException : Exception
{
    public TipoDocumentoNoEncontradoException(int tipoDocumentoId)
        : base($"No se encontró el tipo de documento con ID {tipoDocumentoId}.") { }
}
