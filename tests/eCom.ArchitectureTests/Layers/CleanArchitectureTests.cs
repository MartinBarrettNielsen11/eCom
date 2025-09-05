using NetArchTest.Rules;
using Shouldly;

namespace ArchitectureTests.Layers;

public class CleanArchitectureTests : BaseTest
{
    [Fact]
    public void Domain_layer_should_not_depend_on_any_of_layer()
    {
        TestResult? result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOnAny(
                ApplicationAssembly.GetName().Name,
                InfrastructureAssembly.GetName().Name,
                PersistenceAssembly.GetName().Name,
                PresentationAssembly.GetName().Name
                )
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
