/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;

namespace Kinnser.Gateway.Interfaces.Repositories
{
    /// <summary>
    /// Exception Logger Interface
    /// </summary>
    public interface IExceptionLoggerRepository
    {
        /// <summary>
        /// Logs the exception by sending it to IT Helpdesk for information
        /// </summary>
        /// <param name="exception">The exception to log</param>
        void LogException(IGatewayBaseException exception);

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void LogUnhandledException(Exception exception);
    }
}
