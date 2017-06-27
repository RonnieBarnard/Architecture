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
    /// A social security number for Kinnser
    /// </summary>
    public class SocialSecurityNumber : KinnserBaseValue<string>
    {
        /// <summary>
        /// The validationrule
        /// </summary>
        private readonly string _Validationrule;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialSecurityNumber" /> class.
        /// </summary>
        /// <param name="value">The value to set the SSN to</param>
        /// <param name="validationrule">The validationrule parameter, if left empty, it will take the default ^\d{3}-?\d{2}-?\d{4}$.</param>
        public SocialSecurityNumber(string value, string validationrule = @"^\d{3}-?\d{2}-?\d{4}$")
            : base(value)
        {
            _Validationrule = validationrule;
        }

        /// <summary>
        /// Returns true if the Social Security Number is valid
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            return new Regex(_Validationrule).IsMatch(Value);
        }
    }
}
