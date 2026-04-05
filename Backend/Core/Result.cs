namespace AutoCita.Api.Core;

public class Result<T, E>
{
    public bool IsSuccess { get; }
    public T? Value { get
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException("Cannot access Value when the result is a failure.");
            }
            return _value;
        } 
    }
    public E? Error { get
        {
            if (IsSuccess)
            {
                throw new InvalidOperationException("Cannot access Error when the result is a success.");
            }
            return _error;
        } 
    }

    private readonly T? _value;
    private readonly E? _error;


    private Result(bool isSuccess, T? value, E? error)
    {
        IsSuccess = isSuccess;
        _value = value;
        _error = error;
    }

    public static Result<T, E> Ok(T? value)
    {
        return new Result<T, E>(true, value, default);
    }

    public static Result<T, E> Fail(E? error)
    {
        return new Result<T, E>(false, default, error);
    }

    // Operadores de conversión implícita para facilitar la creación de resultados
    public static implicit operator Result<T, E>(T value) => Ok(value);
    public static implicit operator Result<T, E>(E error) => Fail(error);
}
