/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;

namespace Kinnser.Gateway.API
{
    /// <summary>
    /// ApiBase is the abstract base class for all API Calls, it instantiates the underlying Data Layer
    /// </summary>
    internal abstract class ApiBase
    {
        /// <summary>
        /// Decodes the specified content.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        protected static T Decode<T>(string content)
        {
            return Factory.Make<ICrypto>().FromJson<T>(content);
        }

        /// <summary>
        /// Encodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected static string Encode(object data)
        {
            return Factory.Make<ICrypto>().ToJson(data);
        }
    }
}
