/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using Kinnser.DomainModel.Values;

namespace Kinnser.DomainModel.Interfaces
{
    /// <summary>
    /// User Object
    /// </summary>
    /// <seealso cref="Kinnser.DomainModel.IKinnserBaseEntity" />
    public interface IUser : IKinnserBaseEntity
    {
        #region Properties / Attributes

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IUser"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the default page.
        /// </summary>
        /// <value>
        /// The default page.
        /// </value>
        string DefaultPage { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        EmailAddress EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        string EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the password changed date.
        /// </summary>
        /// <value>
        /// The password changed date.
        /// </value>
        DateTimeOffset PasswordChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the signature hash.
        /// </summary>
        /// <value>
        /// The signature hash.
        /// </value>
        string SignatureHash { get; set; }

        /// <summary>
        /// Gets or sets the social security number.
        /// </summary>
        /// <value>
        /// The social security number.
        /// </value>
        SocialSecurityNumber SocialSecurityNumber { get; set; }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>
        /// The suffix.
        /// </value>
        string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        string Username { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Gets the USER object given a username and encryped password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <returns>
        /// A user object, or NULL if the username and password do not match
        /// </returns>
        IUser GetByUsernameAndPassword(string userName, string passWord);

        #endregion
    }
}