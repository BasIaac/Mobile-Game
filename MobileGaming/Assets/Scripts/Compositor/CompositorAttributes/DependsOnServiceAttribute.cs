using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class DependsOnServiceAttribute : Attribute
    {}
}