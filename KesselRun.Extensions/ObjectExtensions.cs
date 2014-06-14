
using System;

namespace KesselRun.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// A method which just evaluates whether a variable which is declared as a reference type is null.
        /// </summary>
        /// <remarks>
        /// This is intended to be used for reference types only. Nullable value types are excluded, as 
        /// they can be evaluated for null by using their HasValue property.
        /// </remarks>
        /// <param name="source">The object to evaluate.</param>
        /// <returns>bool</returns>
        public static bool IsNull(this object source)
        {
            if(source is ValueType)
                throw new NotSupportedException("The object cannot be a value type.");

            return ReferenceEquals(null, source);
        }
    }
}
