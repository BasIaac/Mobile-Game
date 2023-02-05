using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class OnTickAttribute : Attribute
    {}
    
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class OnUpdateAttribute : Attribute
    {}
}


