/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;

namespace Kinnser.Gateway.Interfaces.Repositories
{
    public interface IAnalyticsLoggerRepository
    {
        /// <summary>
        /// Logs the execution time for a component
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="time">The time.</param>
        /// <param name="parameters">The parameters.</param>
        void LogExecutionTime(string root, TimeSpan time, params string[] parameters);
    }
}
