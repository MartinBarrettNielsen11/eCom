namespace Management.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
