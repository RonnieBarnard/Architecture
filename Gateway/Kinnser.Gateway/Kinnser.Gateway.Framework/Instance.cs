/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

namespace Kinnser.Gateway.Framework
{
    /// <summary>
    /// Type converter utility class
    /// </summary>
    public static class Instance
    {
        /// <summary>
        /// Offs the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T Off<T>(object value)
        {
            return (T)value;
        }
    }
}
