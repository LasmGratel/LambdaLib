using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LambdaLib
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetExportedTypes<T>(this Assembly assembly)
        {
            return from type in assembly.ExportedTypes where type.IsClass && !type.IsAbstract && type.IsAssignableFrom(typeof(T)) select type;
        } 
    }
}
