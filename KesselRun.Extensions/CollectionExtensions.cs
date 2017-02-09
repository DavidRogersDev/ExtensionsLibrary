using System;
using System.Collections.Generic;
using System.Linq;

namespace KesselRun.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Taken from the WebformsMVP project - http://webformsmvp.com/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> items)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            if (items == null)
                throw new ArgumentNullException("items");

            foreach (var item in items)
                target.Add(item);
        }

        /// <summary>
        /// Taken from the WebformsMVP project - http://webformsmvp.com/
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="createValueCallback"></param>
        /// <returns></returns>
        public static TValue GetOrCreateValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValueCallback)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (!dictionary.ContainsKey(key))
                lock (dictionary)
                    if (!dictionary.ContainsKey(key))
                        dictionary[key] = createValueCallback();

            return dictionary[key];
        }
        

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Taken from the WebformsMVP project - http://webformsmvp.com/
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<TKey,TValue> ToDictionary<TKey,TValue>(this IEnumerable<KeyValuePair<TKey,TValue>> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.ToDictionary(m => m.Key, m => m.Value);
        }

        public static bool Empty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        /// <summary>
        /// Taken from the WebformsMVP project - http://webformsmvp.com/
        /// An order independent version of <see cref="Enumerable.SequenceEqual{TSource}(System.Collections.Generic.IEnumerable{TSource},System.Collections.Generic.IEnumerable{TSource})"/>.
        /// Note, use of the word set in the method name refers to Mathmatical concept of "a set", not the verb "to set".
        /// </summary>
        public static bool SetEqual<T>(this IEnumerable<T> x, IEnumerable<T> y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");
            
            var objectsInX = x.ToList();
            var objectsInY = y.ToList();

            if (objectsInX.Count() != objectsInY.Count())
                return false;

            foreach (var objectInY in objectsInY)
            {
                if (!objectsInX.Contains(objectInY))
                    return false;

                objectsInX.Remove(objectInY);
            }

            return objectsInX.Empty();
        }

        /// <summary>
        /// Operation on a Stack. Pops everything off the stack in one goe and returns them as an IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>Type: IEnumerable&gt;T&lt;. An enumerable containing all of the members popped from the stack.</returns>
        public static IEnumerable<T> PopAllFromStack<T>(this Stack<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            while (source.Count > 0)
            {
                yield return source.Pop();
            }
        }
    }
}
