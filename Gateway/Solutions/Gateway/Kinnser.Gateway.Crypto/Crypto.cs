/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using System.Text;
using Kinnser.Gateway.Crypto.Providers;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;
using Newtonsoft.Json;

namespace Kinnser.Gateway.Crypto
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Kinnser.Gateway.Interfaces.ICrypto" />
    [Factory(typeof(ICrypto))]
    internal class Crypto : ICrypto
    {
        /// <summary>
        /// Converts from a Json String
        /// </summary>
        /// <typeparam name="T">The TYPE of the object</typeparam>
        /// <param name="value">The value to convert</param>
        /// <returns>
        /// a Deserialized object
        /// </returns>
        T ICrypto.FromJson<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(Convert.FromBase64String(value)));
        }

        /// <summary>
        /// Convert the value to a Json serialized string
        /// </summary>
        /// <param name="value">The object to serialize</param>
        /// <returns>
        /// A serialized object
        /// </returns>
        string ICrypto.ToJson(object value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string EncryptPassword(string password)
        {
            return Factory.Make<IOnewayCryptoProvider>().Encrypt(password);
        }
    }
}
