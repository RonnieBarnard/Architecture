/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

namespace Kinnser.Gateway.Interfaces.Rules
{
    public interface IRule
    {
        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        int Priority { get; }

        /// <summary>
        /// Gets the name of the rule
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the message should the rule fail
        /// </summary>
        /// <value>
        /// The failure message, should the rule fail the test
        /// </value>
        string Message { get; }

        /// <summary>
        /// Checks the specified value against the rule condition
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parameters">any parameters required to execute the rule</param>
        /// <returns></returns>
        bool Check<T>(T value, params object[] parameters);
    }
}