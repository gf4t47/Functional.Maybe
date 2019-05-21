// Laserfiche  - Copyright(c) 1993-2016 Compulink Management Center, Inc.
namespace Laserfiche.Spark.Extensions.Option
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Looks in a dictionary for a given key.
    /// </summary>
    public static class MaybeDictionary
    {
        /// <summary>
        /// Tries to get value from Dictionary safely
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Maybe<T> Lookup<TK, T>(this IDictionary<TK, T> dictionary, TK key)
        {
            Contract.Requires(dictionary != null);

            var getter = MaybeFunctionalWrappers.Wrap<TK, T>(dictionary.TryGetValue);
            return getter(key);
        }
    }
}