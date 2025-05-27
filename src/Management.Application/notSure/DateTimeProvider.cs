using Microsoft.Extensions.Logging;

namespace Management.Application.notSure;

/// <summary>
///     Interface for mocking the current UTC date and time.
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <summary>
    ///     Gets the current UTC date and time.
    /// </summary>
    /// <value>The current UTC date and time.</value>
    public DateTime UtcNow => DateTime.UtcNow;
}