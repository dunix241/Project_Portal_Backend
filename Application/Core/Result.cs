namespace Application.Core;

public class Result<T>
{
    public Status Status { get; set; }
    public bool IsSuccess { get => Status == Status.Success; }
    public T? Value { get; set; }
    public string? Error { get; set; }

    public static Result<T> Success(T value)
    {
        return new Result<T> { Status = Status.Success, Value = value };
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T> { Status = Status.BadRequest, Error = error };
    }
    
    public static Result<T> Failure(Status status, string error)
    {
        return new Result<T> { Status = status, Error = error };
    }
}

public enum Status
{
    Success,
    BadRequest,
    Unauthorized,
    Forbid,
}