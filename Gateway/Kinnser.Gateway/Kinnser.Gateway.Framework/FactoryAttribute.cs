/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;

namespace Kinnser.Gateway.Framework
{
    /// <summary>
    /// Factory Attribute to hook into the Factory for making objects
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class FactoryAttribute : Attribute
    {
        /// <summary>
        /// Gets the type of the interface.
        /// </summary>
        /// <value>
        /// The type of the interface.
        /// </value>
        public Type InterfaceType { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is singleton.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is singleton; otherwise, <c>false</c>.
        /// </value>
        public bool IsSingleton { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryAttribute" /> class.
        /// </summary>
        /// <param name="intefaceType">Type of the inteface.</param>
        /// <param name="isSingleton">if set to <c>true</c> the instance will be cached</param>
        public FactoryAttribute(Type intefaceType, bool isSingleton = false)
        {
            InterfaceType = intefaceType;
            IsSingleton = isSingleton;
        }
    }
}
