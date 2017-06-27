/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */


using System;
using System.IO;
using System.Linq;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;
using Kinnser.Gateway.Interfaces.Repositories;

namespace Kinnser.Gateway.Repositories
{
    [Factory(typeof(IAnalyticsLoggerRepository), true)]
    internal class AnalyticsLoggerRepository : BaseRepository, IAnalyticsLoggerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsLoggerRepository" /> class.
        /// </summary>
        public AnalyticsLoggerRepository()
                    : base(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsLoggerRepository"/> class.
        /// </summary>
        /// <param name="data">The data layer, if null will create the default</param>
        public AnalyticsLoggerRepository(IData data = null)
            : base(data)
        {
            
        }

        /// <summary>
        /// Logs the execution time for a component
        /// </summary>
        /// <param name="root">The root folder where the logfile is to append to</param>
        /// <param name="time">The time</param>
        /// <param name="parameters">The parameters.</param>
        void IAnalyticsLoggerRepository.LogExecutionTime(string root, TimeSpan time, params string[] parameters)
        {
            //NOTE: This can be made smarter, or implement another style of logging
            File.AppendAllText(Path.Combine(root, "logs", $"Analytics-{DateTime.Now.Date:yyyyMMdd}.log"), $"{DateTime.Now:F}~{time:G}~{parameters.Reverse().Aggregate("", (n, c) => c + $"~{n}")}{Environment.NewLine}");
        }
    }
}
