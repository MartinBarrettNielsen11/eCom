namespace Management.Persistence;

internal sealed class UnitOfWork(OrderContext orderContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await orderContext.SaveChangesAsync(cancellationToken);
}