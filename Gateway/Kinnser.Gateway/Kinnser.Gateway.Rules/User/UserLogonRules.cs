/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System.Collections.Generic;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces.Rules;

namespace Kinnser.Gateway.Rules.User
{
    /// <summary>
    /// Defines the set of rules that will execute when a user logs onto the kinnser system
    /// </summary>
    /// <seealso cref="Kinnser.Gateway.Rules.User.IUserLogonRules" />
    /// <seealso cref="Kinnser.Gateway.Interfaces.Rules.IRuleSet" />
    [Factory(typeof(IUserLogonRules))]
    internal class UserLogonRules : IUserLogonRules
    {
        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        public IEnumerable<IRule> Rules { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLogonRules" /> class.
        /// </summary>
        public UserLogonRules()
        {
            Rules = new List<IRule>();
            Instance.Off<List<IRule>>(Rules).Add(Factory.Make<IsUserActiveRule>(0, "User.IsActive", "User.IsNotActive"));
            //Instance.Off<List<IRule>>(Rules).Add(Factory.Make<IsUserInsideHours>(1, "User.IsInsideHours", "User.IsOutsideOfActiveHourse"));
        }
    }
}
