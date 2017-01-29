
using System;
using Newtonsoft.Json;

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
            return ReferenceEquals(null, source);
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetJsonTypeDescription(this object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            var description = obj.GetType().GetDescription();
            return description.ToJsonString();
        }
    }
}
