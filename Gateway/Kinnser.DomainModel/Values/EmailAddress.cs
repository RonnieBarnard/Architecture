/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System.Text.RegularExpressions;

namespace Kinnser.DomainModel.Values
{
    /// <summary>
    /// Represents an Email Address in the Kinnser system
    /// </summary>
    /// <seealso cref="Kinnser.DomainModel.Values.KinnserBaseValue{string}" />
    public sealed class EmailAddress : KinnserBaseValue<string>
    {
        private readonly string _ValidationRule;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress" /> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="validationRule">The validation rule to use for validating the email address, if empty, it will use the default email validation rule accroding to the RFC rules of email addresses</param>
        public EmailAddress(string email, string validationRule = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                                                  + "@"
                                                                  + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")
            : base(email)
        {
            _ValidationRule = validationRule;
        }

        /// <summary>
        /// Returns true if the Value of the emailaddress is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            return new Regex(_ValidationRule).IsMatch(Value);
        }
    }
}
