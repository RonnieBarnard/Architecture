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
    internal class FactoryItem
    {
        /// <summary>
        /// Gets or sets the type of the instance.
        /// </summary>
        /// <value>
        /// The type of the instance.
        /// </value>
        public Type InstanceType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FactoryItem" /> is signleton.
        /// </summary>
        /// <value>
        ///   <c>true</c> if signleton; otherwise, <c>false</c>.
        /// </value>
        public bool Singleton { get; set; }

        /// <summary>
        /// Gets or sets the instance, only used if the 
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public object Instance { get; set; }
    }
}
