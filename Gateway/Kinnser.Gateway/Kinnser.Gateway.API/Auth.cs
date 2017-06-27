/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using System.Net;
using Kinnser.Common.Exceptions;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;
using Kinnser.Gateway.Interfaces.API;
using Kinnser.Gateway.Interfaces.Repositories;

namespace Kinnser.Gateway.API
{
    /// <summary>
    /// Authorization Repository
    /// </summary>
    /// <seealso cref="Kinnser.Gateway.Interfaces.API.IAuth" />
    [Factory(typeof(IAuth))]
    internal class Auth : ApiBase, IAuth
    {
        /// <summary>
        /// Private class to deserialize the incoming detail
        /// </summary>
        private class LoginParameters
        {
            public string Username { get; set; }
            public string Password { get; set; }

            /// <summary>
            /// Returns true if the login parameters is valid.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
            /// </returns>
            public bool Valid()
            {
                return !string.IsNullOrWhiteSpace(Username) &&
                       !string.IsNullOrWhiteSpace(Password);
            }
        }

        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="content">The encrypted content coming from the UI</param>
        /// <returns>The USER object</returns>
        object IAuth.Login(string content)
        {
            var parameters = Decode<LoginParameters>(content);
            if (parameters == null || !parameters.Valid()) throw Factory.Make<GatewayBaseException>(new ArgumentNullException(), HttpStatusCode.PreconditionFailed, "Login.Validation.ServerFailed");
            return Factory.Make<IUserRepository>().Login(parameters.Username, parameters.Password);
        }
    }
}
