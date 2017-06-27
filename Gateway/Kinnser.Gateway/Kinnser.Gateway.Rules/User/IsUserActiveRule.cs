/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using Kinnser.DomainModel.Interfaces;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces.Rules;

namespace Kinnser.Gateway.Rules.User
{
    /// <summary>
    /// Verifies that a User is active
    /// </summary>
    internal class IsUserActiveRule : IRule
    {
        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public int Priority { get; }

        /// <summary>
        /// Gets the name of the rule
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the message should the rule fail
        /// </summary>
        /// <value>
        /// The failure message, should the rule fail the test
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsUserActiveRule" /> class.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public IsUserActiveRule(int priority, string name, string message)
        {
            Priority = priority;
            Name = name;
            Message = message;
        }

        /// <summary>
        /// Checks the specified value against the rule condition
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parameters">any parameters required to execute the rule</param>
        /// <returns></returns>
        public bool Check<T>(T value, params object[] parameters)
        {
            return Instance.Off<IUser>(value).Active;
        }
    }
}
