/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */


using System;
using System.Net;

namespace Kinnser.Gateway.Interfaces
{
    /// <summary>
    /// The Gateway Exception boundary
    /// </summary>
    public interface IGatewayBaseException
    {
        /// <summary>
        /// Gets the code that the UI will use to map errors
        /// </summary>
        /// <value>
        /// The code, typically it is HttpStatus.OK (250)
        /// </value>
        HttpStatusCode Code { get; }

        /// <summary>
        /// Gets the exception that was thrown, this never makes it to the UI environment, is for internal logging purposes
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        Exception Exception { get; }

        /// <summary>
        /// Gets the i18N error handler for UI display purpose
        /// </summary>
        /// <value>
        /// The i18n error code
        /// </value>
        string I18NError { get; }
    }
}