namespace MultiPortalSchoolSys.Application.Common;

/// <summary>
/// Generic result wrapper used by every service method.
/// Eliminates exception-driven flow control and gives every consumer
/// (MVC controllers, API controllers, background jobs) a consistent contract.
/// </summary>
public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Value { get; private set; }
    public string? ErrorMessage { get; private set; }
    public int StatusCode { get; private set; }

    private Result() { }

    public static Result<T> Success(T value) => new()
    {
        IsSuccess  = true,
        Value      = value,
        StatusCode = 200
    };

    public static Result<T> Failure(string error, int statusCode = 400) => new()
    {
        IsSuccess    = false,
        ErrorMessage = error,
        StatusCode   = statusCode
    };

    public static Result<T> NotFound(string error) =>
        Failure(error, 404);

    public static Result<T> Unauthorized(string error) =>
        Failure(error, 401);

    public static Result<T> Forbidden(string error) =>
        Failure(error, 403);

    public static Result<T> Conflict(string error) =>
        Failure(error, 409);
}

/// <summary>
/// Non-generic Result for service operations that return no data
/// (e.g., delete, update, publish).
/// </summary>
public class Result
{
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }
    public int StatusCode { get; private set; }

    private Result() { }

    public static Result Success() => new()
    {
        IsSuccess  = true,
        StatusCode = 200
    };

    public static Result Failure(string error, int statusCode = 400) => new()
    {
        IsSuccess    = false,
        ErrorMessage = error,
        StatusCode   = statusCode
    };

    public static Result NotFound(string error) =>
        Failure(error, 404);

    public static Result Forbidden(string error) =>
        Failure(error, 403);

    public static Result Conflict(string error) =>
        Failure(error, 409);
}