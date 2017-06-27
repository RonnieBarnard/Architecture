/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using Kinnser.DomainModel.Interfaces;
using Kinnser.DomainModel.Values;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;

namespace Kinnser.DomainModel.Entities
{
    /// <summary>
    /// User base class that describes the common features of a particular entity
    /// </summary>
    /// <seealso cref="IUser" />
    /// <seealso cref="Kinnser.DomainModel.KinnserBaseEntity" />
    [Factory(typeof(IUser))]
    public class User : KinnserBaseEntity, IUser
    {
        #region Attributes / Properties
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>
        /// The suffix.
        /// </value>
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the social security number.
        /// </summary>
        /// <value>
        /// The social security number.
        /// </value>
        public SocialSecurityNumber SocialSecurityNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public EmailAddress EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password changed date.
        /// </summary>
        /// <value>
        /// The password changed date.
        /// </value>
        public DateTimeOffset PasswordChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the signature hash.
        /// </summary>
        /// <value>
        /// The signature hash.
        /// </value>
        public string SignatureHash { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the default page.
        /// </summary>
        /// <value>
        /// The default page.
        /// </value>
        public string DefaultPage { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Gets the USER object given a username and encrypted password
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <returns>
        /// A userobject or NULL if the passWord do not match
        /// </returns>
        IUser IUser.GetByUsernameAndPassword(string userName, string passWord)
        {
            return Factory.Make<IData>().FirstOrDefault<User>("User.GetByUsernameAndPassword", userName, passWord);
        }


        #endregion
    }
}