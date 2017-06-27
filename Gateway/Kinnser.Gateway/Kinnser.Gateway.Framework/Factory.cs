/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Kinnser.Gateway.Framework
{
    /// <summary>
    /// This is the main factory class. In this architecture a Developer never touch the 'new' keyword. All items must be created by using the Factory.Make method
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// The actual factory
        /// </summary>
        private static readonly Dictionary<Type, FactoryItem> _Factory = new Dictionary<Type, FactoryItem>();

        /// <summary>
        /// Makes the specified type given the set of parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static T Make<T>(params object[] parameters)
        {
            if (!_Factory.ContainsKey(typeof(T)))
                return (T)Activator.CreateInstance(typeof(T), parameters);

            var item = _Factory[typeof(T)];
            if (item.Singleton && item.Instance != null) return (T)item.Instance;

            var temp = Activator.CreateInstance(item.InstanceType, parameters);
            if (item.Singleton && item.Instance == null)
            {
                item.Instance = temp;
            }

            return (T)temp;
        }

        /// <summary>
        /// Makes the specified type given the set of parameters
        /// </summary>
        /// <typeparam name="T">The return type expected from the TYPE parameters</typeparam>
        /// <param name="type">The type to create</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static T Make<T>(Type type, params object[] parameters)
        {
            var item = _Factory[type];
            if (item.Singleton && item.Instance != null) return (T)item.Instance;

            var temp = Activator.CreateInstance(item.InstanceType, parameters);
            if (item.Singleton && item.Instance == null)
            {
                item.Instance = temp;
            }

            return (T)temp;
        }

        /// <summary>
        /// Adds the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="item">The item.</param>
        internal static void Add(Type type, FactoryItem item)
        {
            if (_Factory.ContainsKey(type)) return;
            _Factory.Add(type, item);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public static void Dispose()
        {
            // Clear out all the singleton instances
            foreach (var factoryValue in _Factory.Values)
            {
                if (factoryValue.Singleton)
                    factoryValue.Instance = null;
            }
            _Factory.Clear();
            GC.Collect();
        }
    }
}
