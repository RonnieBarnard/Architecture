/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

namespace Kinnser.Gateway.Interfaces.Repositories
{
    /// <summary>
    /// Represents the User Repository for dealing with all USER related features
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Attempts a user login given a username and password combination
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>
        /// a user object
        /// </returns>
        dynamic Login(string username, string password);
    }
}
