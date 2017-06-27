/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System.IO;
using System.Reflection;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;

namespace Kinnser.Gateway.Repositories
{
    /// <summary>
    /// Abstract layer for repository
    /// </summary>
    internal abstract class BaseRepository
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        protected IData Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository" /> class.
        /// </summary>
        /// <param name="data">The data layer, if null will create the default</param>
        protected BaseRepository(IData data)
        {
            Data = data ?? Factory.Make<IData>();
        }
    }
}
