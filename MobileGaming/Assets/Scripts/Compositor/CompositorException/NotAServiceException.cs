using System;
using System.Reflection;

namespace Exception
{
    public class NotAServiceException : System.Exception
    {
        public NotAServiceException(object dependant, FieldInfo fieldInfo, Type type) :
            base($"Field {fieldInfo.Name} of {type.Name} is marked with [DependsOnService], but has type {fieldInfo.FieldType.Name} instead of an interface derived from IService")
        {}
    }
}