namespace Management.Application;

/// <summary>
/// Interface for atomic commit of tracked changes.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all tracked changes to the data store as a single atomic operation.
    /// </summary>
    /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
    /// <returns>A task representing the asynchronous save operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}