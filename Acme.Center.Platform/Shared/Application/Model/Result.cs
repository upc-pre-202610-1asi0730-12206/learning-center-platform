namespace Acme.Center.Platform.Shared.Application.Model;

/// <summary>
///     Generic Result class for Command Handlers in the Application Layer.
/// </summary>
/// <typeparam name="T">The type of the result value.</typeparam>
public class Result<T>
{
    protected Result(bool isSuccess, T value, string message)
    {
        IsSuccess = isSuccess;
        Value = value;
        Message = message;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T Value { get; }
    public string Message { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, string.Empty);
    }

    public static Result<T> Failure(string message)
    {
        return new Result<T>(false, default!, message);
    }
}

/// <summary>
///     Non-generic Result class for Command Handlers.
/// </summary>
public class Result : Result<object>
{
    private Result(bool isSuccess, string message) : base(isSuccess, null!, message)
    {
    }

    public static Result Success()
    {
        return new Result(true, string.Empty);
    }

    public new static Result Failure(string message)
    {
        return new Result(false, message);
    }
}