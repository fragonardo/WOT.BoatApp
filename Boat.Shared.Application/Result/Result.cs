using System.Text.Json.Serialization;

namespace BoatApp.Shared.Application;

public class Result<T> : IResult
{
    [JsonInclude]
    public T Value { get; init; }

    [JsonInclude]
    public ResultStatus Status { get; protected set; }

    public IEnumerable<string> Errors { get; protected set; } = [];

    public bool IsSuccess
    {
        get
        {
            ResultStatus status = Status;
            if ((uint)status <= 1u || status == ResultStatus.NoContent)
            {
                return true;
            }

            return false;
        }
    }

    protected Result()
    {
    }

    public Result(T value)
    {
        Value = value;
    }
        

    protected Result(ResultStatus status)
    {
        Status = status;
    }

    public T? GetValue()
    {
        return Value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public static Result<T> Created(T value)
    {
        return new Result<T>(ResultStatus.Created)
        {
            Value = value
        };
    }

    public static Result<T> Error(string errorMessage)
    {
        Result<T> result = new Result<T>(ResultStatus.Error);
        result.Errors = new string[1] { errorMessage };
        return result;
    }

    public static Result<T> NotFound()
    {
        return new Result<T>(ResultStatus.NotFound);
    }

    public static Result<T> NotFound(params string[] errorMessages)
    {
        return new Result<T>(ResultStatus.NotFound)
        {
            Errors = errorMessages
        };
    }

    public static Result<T> Forbidden()
    {
        return new Result<T>(ResultStatus.Forbidden);
    }

    public static Result<T> Forbidden(params string[] errorMessages)
    {
        return new Result<T>(ResultStatus.Forbidden)
        {
            Errors = errorMessages
        };
    }

    public static Result<T> Unauthorized()
    {
        return new Result<T>(ResultStatus.Unauthorized);
    }

    public static Result<T> Unauthorized(params string[] errorMessages)
    {
        return new Result<T>(ResultStatus.Unauthorized)
        {
            Errors = errorMessages
        };
    }

    public static Result<T> Conflict()
    {
        return new Result<T>(ResultStatus.Conflict);
    }

    public static Result<T> Conflict(params string[] errorMessages)
    {
        return new Result<T>(ResultStatus.Conflict)
        {
            Errors = errorMessages
        };
    }

    public static Result<T> CriticalError(params string[] errorMessages)
    {
        return new Result<T>(ResultStatus.CriticalError)
        {
            Errors = errorMessages
        };
    }

    public static Result<T> Unavailable(params string[] errorMessages)
    {
        return new Result<T>(ResultStatus.Unavailable)
        {
            Errors = errorMessages
        };
    }

    public static Result<T> NoContent()
    {
        return new Result<T>(ResultStatus.NoContent);
    }
}


