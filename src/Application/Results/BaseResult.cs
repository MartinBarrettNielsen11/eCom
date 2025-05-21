using Application.Results;

namespace Application.Results;

public abstract class BaseResult
{
    public bool IsSuccess { get; }

    public string? ErrorMessage { get; }

    public ErrorType ErrorType { get; }

    private protected BaseResult(ErrorType errorType = ErrorType.None, string? errorMessage = default)
    {
        ErrorType = errorType;
        ErrorMessage = errorMessage;
        IsSuccess = errorType is ErrorType.None;
    }
}
public abstract class BaseResult<TResult> : BaseResult
{
    public TResult? Result { get; }

    private protected BaseResult(TResult? result, ErrorType errorType = ErrorType.None,
        string? errorMessage = default) : base(errorType, errorMessage)
    {
        Result = result;
    }
}