/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;
using Kinnser.Gateway.Interfaces.Repositories;

namespace Kinnser.Gateway.Repositories
{
    /// <summary>
    /// Exception logger for Gateway Exception Boundary
    /// </summary>
    /// <seealso cref="Kinnser.Gateway.Repositories.BaseRepository" />
    /// <seealso cref="Kinnser.Gateway.Interfaces.Repositories.IExceptionLoggerRepository" />
    [Factory(typeof(IExceptionLoggerRepository), true)]
    internal class ExceptionLoggerRepository : BaseRepository, IExceptionLoggerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLoggerRepository" /> class.
        /// </summary>
        public ExceptionLoggerRepository() 
            : base(null)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLoggerRepository" /> class.
        /// </summary>
        /// <param name="data">The data layer, if null will create the default</param>
        public ExceptionLoggerRepository(IData data = null)
            : base(data)
        {

        }

        /// <summary>
        /// Logs the exception by sending it to IT Helpdesk for information
        /// </summary>
        /// <param name="exception">The exception to log</param>
        void IExceptionLoggerRepository.LogException(IGatewayBaseException exception)
        {
            var fullErrorStack = exception.Exception.GetFullMessage();
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void IExceptionLoggerRepository.LogUnhandledException(Exception exception)
        {
            var fullErrorStack = exception.GetFullMessage();
        }
    }
}
