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
    /// <summary>
    /// Contains all Exception Extention methods
    /// </summary>
    public static class ExceptionExtentions
    {
        /// <summary>
        /// Gets the full stack of messages in the exception.
        /// </summary>
        /// <param name="ex">The exception that happened</param>
        /// <returns>
        /// a string of the concatenated exception stack
        /// </returns>
        public static string GetFullMessage(this Exception ex)
        {
            var res = string.Empty;
            var exception = ex;

            while (exception != null)
            {
                res += $"{exception.Message}\n{exception.StackTrace}";
                exception = exception.InnerException;
            }

            return res;
        }
    }
}
