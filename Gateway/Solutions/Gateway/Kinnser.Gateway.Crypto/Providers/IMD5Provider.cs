/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

namespace Kinnser.Gateway.Crypto.Providers
{
    /// <summary>
    /// One way algorithm Crypto Provider
    /// </summary>
    public interface IOnewayCryptoProvider
    {
        string Encrypt(string value);
    }
}