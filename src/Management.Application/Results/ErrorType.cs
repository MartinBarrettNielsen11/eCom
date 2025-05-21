namespace Management.Application.Results;

public enum ErrorType
{
    None,
    BadRequest,
    NotFound,
    Forbidden,
    Validation,
    Conflict,
    Other
}