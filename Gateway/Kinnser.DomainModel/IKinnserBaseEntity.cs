/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using Kinnser.Gateway.Interfaces.Rules;

namespace Kinnser.DomainModel
{
    public interface IKinnserBaseEntity
    {
        /// <summary>
        /// Returns true if this instance is valid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <c>true</c> if the specified parameters is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValid<T>(params object[] parameters) where T : IRuleSet;
    }
}