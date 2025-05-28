namespace Management.Application.notSure;

/// <summary>
///     Interface for mocking the current UTC date and time.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    ///     Gets the current UTC date and time.
    /// </summary>
    /// <value>The current UTC date and time.</value>
    DateTime UtcNow { get; }
}