namespace Kinnser.Gateway.Interfaces.API
{
    /// <summary>
    /// All Authentication Functionality
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="content">The encrypted content coming from the UI</param>
        /// <returns></returns>
        object Login(string content);
    }
}
