/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System.Collections.Generic;

namespace Kinnser.Gateway.Interfaces.Rules
{
    /// <summary>
    /// Represents a Set of business rules to run against an entity, to define whether that entity is valid or not
    /// </summary>
    public interface IRuleSet
    {
        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        IEnumerable<IRule> Rules { get; }
    }
}
