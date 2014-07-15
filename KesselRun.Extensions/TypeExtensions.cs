using System;

namespace KesselRun.Extensions
{
    public static class TypeExtensions
    {
        public static TypeDescription GetDescription(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return new TypeDescription
            {
                AssemblyQualifiedName = type.AssemblyQualifiedName,
                FullName = type.FullName
            };
        }
    }

    public class TypeDescription
    {
        public string FullName { get; set; }
        public string AssemblyQualifiedName { get; set; }
    }
}
