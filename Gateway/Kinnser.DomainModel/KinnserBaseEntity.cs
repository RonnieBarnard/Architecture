/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */


using System.Linq;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces.Rules;

namespace Kinnser.DomainModel
{
    /// <summary>
    /// Represents the base of the Kinnser Domain
    /// </summary>
    public abstract class KinnserBaseEntity : IKinnserBaseEntity
    {
        /// <summary>
        /// Returns true if this Entity is valid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsValid<T>(params object[] parameters) where T : IRuleSet
        {
            return Factory.Make<T>().Rules.All(r => r.Check(this, parameters));
        }
    }
}