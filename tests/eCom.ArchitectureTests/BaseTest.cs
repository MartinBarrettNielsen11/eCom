using System.Reflection;
using Domain.Orders;
using Management.Application.Messaging;
using Management.Infrastructure.Time;
using Management.Persistence.Configurations;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Order).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(DateTimeProvider).Assembly;
    protected static readonly Assembly PersistenceAssembly = typeof(CosmosSettings).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(ProgramApi).Assembly;
}
