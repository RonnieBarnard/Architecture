/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Kinnser.Gateway.Framework
{
    /// <summary>
    /// Assembly loader wraps the functionality required to load interface types from an assembly into the factory
    /// </summary>
    public class AssemblyLoader
    {
        /// <summary>
        /// Initializes the <see cref="AssemblyLoader" /> class.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="assemblies">The list of assemblies to load</param>
        public static void Load(string root, string[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                Process<FactoryAttribute>(Assembly.LoadFrom(Path.Combine(root, assembly)));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyLoader" /> class.
        /// </summary>
        static AssemblyLoader()
        {
            Process<FactoryAttribute>(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Processes the specified assembly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly">The assembly.</param>
        public static void Process<T>(Assembly assembly)
        {
            LoadTypes(GetAttributes<T>(assembly));
        }

        /// <summary>
        /// Loads the types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mappedTypes">The mapped types.</param>
        private static void LoadTypes<T>(List<MappedType<T>> mappedTypes)
        {
            mappedTypes.ForEach(mt => mt.TargetAttributes.ForEach(t => Add(mt.TargetType, (FactoryAttribute)(object)t)));
        }

        /// <summary>
        /// Adds the specified obj.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="factoryAttribute">The factory attribute.</param>
        private static void Add(Type targetType, FactoryAttribute factoryAttribute)
        {
            Factory.Add(factoryAttribute.InterfaceType, new FactoryItem { InstanceType = targetType, Singleton = factoryAttribute.IsSingleton });
        }

        /// <summary>
        /// Mapped Types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal struct MappedType<T>
        {
            /// <summary>
            /// Gets or sets the type of the target.
            /// </summary>
            /// <value>
            /// The type of the target.
            /// </value>
            public Type TargetType { get; set; }
            /// <summary>
            /// Gets or sets the target attributes.
            /// </summary>
            /// <value>
            /// The target attributes.
            /// </value>
            public List<T> TargetAttributes { get; set; }
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        private static List<MappedType<T>> GetAttributes<T>(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var attribs = (from type in types
                           let attrs = type.GetCustomAttributes(typeof(T), false).Cast<T>().ToList()
                           where (attrs != null && attrs.Count > 0)
                           select new MappedType<T> { TargetType = type, TargetAttributes = attrs }
                          ).ToList();


            var assattribs = assembly.GetCustomAttributes(typeof(T), false);
            if (assattribs.Length > 0)
            {
                attribs.Add(new MappedType<T> { TargetType = assembly.GetType(), TargetAttributes = assattribs.Cast<T>().ToList() });
            }
            return attribs;
        }
    }
}
