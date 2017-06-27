/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using System.Net;
using System.Security.Authentication;
using Kinnser.Common.Exceptions;
using Kinnser.DomainModel.Interfaces;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;
using Kinnser.Gateway.Interfaces.Repositories;
using Kinnser.Gateway.Interfaces.Rules.User;

namespace Kinnser.Gateway.Repositories
{
    /// <summary>
    /// The user repository implementation
    /// </summary>
    /// <seealso cref="Kinnser.Gateway.Repositories.BaseRepository" />
    /// <seealso cref="Kinnser.Gateway.Interfaces.Repositories.IUserRepository" />
    [Factory(typeof(IUserRepository))]
    internal class UserRepository : BaseRepository, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        public UserRepository() : base(null)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="data">The data layer, if null will create the default</param>
        public UserRepository(IData data = null) 
            : base(data)
        {
        }

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// a user object
        /// </returns>
        dynamic IUserRepository.Login(string username, string password)
        {
            var user = Factory.Make<IUser>().GetByUsernameAndPassword(username, Factory.Make<ICrypto>().EncryptPassword(password));

            // Validate that the user object is in the correct state before returning it to the sysetm
            if (user == null || !user.IsValid<IUserLogonRules>())
                throw Factory.Make<GatewayBaseException>(new AuthenticationException(), HttpStatusCode.Unauthorized, "Security.InvalidLogon");

            return user;
        }
    }
}
