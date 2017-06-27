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
    public static class Safe
    {
        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="fail">The fail.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static T Execute<T>(Func<object[], T> action, Action<Exception> fail = null, params object[] parameters)
        {
            try
            {
                return action(parameters);
            }
            catch (Exception exception)
            {
                fail?.Invoke(exception);
                return default(T);
            }
        }
}
}
