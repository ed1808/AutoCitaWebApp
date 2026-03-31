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

    public static Result<U, F> Ok<U, F>(U? value)
    {
        return new Result<U, F>(true, value, default);
    }

    public static Result<U, F> Fail<U, F>(F? error)
    {
        return new Result<U, F>(false, default, error);
    }
}
