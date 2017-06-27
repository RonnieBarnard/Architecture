/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using System;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Kinnser.Gateway.DataLayer
{
    /// <summary>
    /// implementation of the Data Contract
    /// </summary>
    /// <seealso cref="Kinnser.Gateway.Interfaces.IData" />
    [Factory(typeof(IData), true)]
    internal class Data : IData
    {
        private readonly Datasource _DataStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="Data" /> class.
        /// </summary>
        public Data() : this("Datasource")
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Data" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="conn">The connection, required for tests</param>
        public Data(string connectionString, Datasource conn = null)
        {
            _DataStore = conn ?? new Datasource(connectionString);
        }

        /// <summary>
        /// Retrieves the first record from a dataset or no record at all
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T IData.FirstOrDefault<T>(string statementName, params object[] parameters)
        {
            return Instance.Off<IData>(this).Enumerate<T>(statementName, parameters).FirstOrDefault();
        }

        /// <summary>
        /// Enumerates the specified statement name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        IEnumerable<T> IData.Enumerate<T>(string statementName, params object[] parameters)
        {
            var sql = GetStatementForNamedSql(statementName);
            if (string.IsNullOrWhiteSpace(sql)) throw new Exception($"Named statement does not exist: {statementName}");
            return _DataStore.Database.SqlQuery<T>(sql, parameters);
        }

        /// <summary>
        /// Gets the statement for named event.
        /// </summary>
        /// <param name="statementName">Name of the statement.</param>
        /// <returns>
        /// returns the named statement
        /// </returns>
        private string GetStatementForNamedSql(string statementName)
        {
            // this function returns the versioned SQL Statement stored in the database, given the Named isntance

            return File.ReadAllText(Path.Combine(HttpContext.Current.Server.MapPath("~/SQL/"), $"{statementName}.sql"));

            //return _DataStore.Database.SqlQuery<string>("select top 1 statement from [Statement] where Name = {0} order by Version desc", statementName).FirstOrDefault();
        }
    }
 }
