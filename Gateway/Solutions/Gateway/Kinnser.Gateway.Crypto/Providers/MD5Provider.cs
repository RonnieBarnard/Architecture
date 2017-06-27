/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */


using System.Security.Cryptography;
using System.Text;
using Kinnser.Gateway.Framework;

namespace Kinnser.Gateway.Crypto.Providers
{
    /// <summary>
    /// MD5 Hash Cypher Provider
    /// </summary>
    /// <seealso cref="Kinnser.Gateway.Crypto.Providers.IOnewayCryptoProvider" />
    [Factory(typeof(IOnewayCryptoProvider))]
    internal class Md5Provider : IOnewayCryptoProvider
    {
        /// <summary>
        /// Encrypt the value and returns the Hash
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : Encoding.UTF8.GetString(EncryptValue(value));
        }

        /// <summary>
        /// Encrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private byte[] EncryptValue(string value)
        {
            var encryptor = MD5.Create();
            encryptor.ComputeHash(Encoding.Unicode.GetBytes(value));
            return encryptor.Hash;
        }
    }
}