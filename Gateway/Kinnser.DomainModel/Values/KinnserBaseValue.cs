/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

namespace Kinnser.DomainModel.Values
{
    /// <summary>
    /// Defines a Kinnser Base Value
    /// </summary>
    public abstract class KinnserBaseValue<T>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinnserBaseValue{T}" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal KinnserBaseValue(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns true if the Kinnser Object is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsValid();

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
