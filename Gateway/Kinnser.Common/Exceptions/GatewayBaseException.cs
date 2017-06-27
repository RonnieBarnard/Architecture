/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using Kinnser.Gateway.Interfaces;
using System;
using System.Net;

namespace Kinnser.Common.Exceptions
{
    /// <summary>
    /// Gateway Exception that is thrown
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="Kinnser.Gateway.Interfaces.IGatewayBaseException" />
    public class GatewayBaseException : Exception, IGatewayBaseException
    {
        /// <summary>
        /// Gets the exception that was thrown, this never makes it to the UI environment, is for internal logging purposes
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; }
        
        /// <summary>
        /// Gets the code that the UI will use to map errors
        /// </summary>
        /// <value>
        /// The code, typically it is HttpStatus.OK (250)
        /// </value>
        public HttpStatusCode Code { get; }

        /// <summary>
        /// Gets the i18N error handler for UI display purpose
        /// </summary>
        /// <value>
        /// The i18n error code
        /// </value>
        public string I18NError { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayBaseException" /> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="code">The code.</param>
        /// <param name="i18NError">The i18 n error.</param>
        public GatewayBaseException(Exception exception, HttpStatusCode code, string i18NError)
        {
            Exception = exception;
            Code = code;
            I18NError = i18NError;
        }
    }
}
