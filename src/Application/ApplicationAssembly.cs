using System.Reflection;

namespace Application;

public static class ApplicationAssembly
{
    public static Assembly Get() => typeof(ApplicationAssembly).Assembly;
}