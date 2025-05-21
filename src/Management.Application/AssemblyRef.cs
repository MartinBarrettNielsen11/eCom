using System.Reflection;

namespace Service;

public static class AssemblyRef
{
    public static Assembly Assembly => typeof(AssemblyRef).Assembly;
}