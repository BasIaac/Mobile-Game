using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ServiceInitAttribute: Attribute
    {}
}