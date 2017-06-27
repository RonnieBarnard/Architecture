namespace Kinnser.Gateway.Interfaces
{
    public interface ICrypto
    {
        /// <summary>
        /// Converts from a Json String
        /// </summary>
        /// <typeparam name="T">The TYPE of the object</typeparam>
        /// <param name="value">The value to convert</param>
        /// <returns>a Deserialized object</returns>
        T FromJson<T>(string value);

        /// <summary>
        /// Convert the value to a Json serialized string
        /// </summary>
        /// <param name="value">The object to serialize</param>
        /// <returns>A serialized object</returns>
        string ToJson(object value);

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        string EncryptPassword(string password);
    }
}

