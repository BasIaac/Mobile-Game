using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class OnTickAttribute : Attribute
    {}
}


