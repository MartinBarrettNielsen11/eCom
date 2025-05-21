namespace Application.Results;

public static class CommandResult
{
    public static CommandResult<TResult> Success<TResult>(TResult result) => new(result);

    public static CommandResult<EmptyResult> Success() => new(EmptyResult.Default);

    public static CommandResult<TResult> Failure<TResult>(ErrorType errorType) => new(default, errorType);

    public static CommandResult<TResult> Failure<TResult>(ErrorType errorType, string errorMessage) =>
        Failure<TResult>(default, errorType, errorMessage);

    public static CommandResult<TResult> Failure<TResult>(TResult? result, ErrorType errorType, string errorMessage) =>
        new(result, errorType, errorMessage);
}

public class CommandResult<TResult> : BaseResult<TResult>
{
    internal CommandResult(TResult? result, ErrorType errorType = ErrorType.None, string? errorMessage = default) : base(result, errorType, errorMessage)
    {
    }
}