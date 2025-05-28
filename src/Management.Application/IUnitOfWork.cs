namespace Management.Application;

/// <summary>
///     Interface for mocking the atomic commit of tracked changes in the current context.
/// </summary>
/// 
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}