/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System.Collections.Generic;

namespace Kinnser.Gateway.Interfaces
{
    /// <summary>
    /// Data layer contract
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// Retrieves the first record from a dataset or no record at all
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T FirstOrDefault<T>(string statementName, params object[] parameters);

        /// <summary>
        /// Enumerates the specified statement name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<T> Enumerate<T>(string statementName, params object[] parameters);
    }
}
