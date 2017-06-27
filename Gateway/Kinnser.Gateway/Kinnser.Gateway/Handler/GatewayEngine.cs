/*
    File Header:
    ------------
    Author              Date            Comment
    ====================================================================================================================
    Ronnie Barnard      6/13/2017       Initial version
 */

using Kinnser.Common;
using Kinnser.Gateway.Framework;
using Kinnser.Gateway.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Kinnser.Common.Exceptions;
using Kinnser.Gateway.Interfaces.Repositories;

namespace Kinnser.Gateway.Handler
{
    /// <summary>
    /// The gateway engine
    /// </summary>
    /// <seealso cref="System.Web.IHttpHandler" />
    public class GatewayEngine : IHttpHandler
    {
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        public bool IsReusable => false;

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom <see langword="HttpHandler" /> that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, <see langword="Request" />, <see langword="Response" />, <see langword="Session" />, and <see langword="Server" />) used to service HTTP requests.</param>
        [HttpPost]
        public void ProcessRequest(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Setup the factory for this instance
            AssemblyLoader.Load(
                context.Server.MapPath("~/bin/"),
                new[] {
                    "Kinnser.Common.dll",
                    "Kinnser.Gateway.Crypto.dll",
                    "Kinnser.DomainModel.dll",
                    "Kinnser.Gateway.Repositories.dll",
                    "Kinnser.Gateway.API.dll",
                    "Kinnser.Gateway.Rules.dll",
                    "Kinnser.Gateway.DataLayer.dll"
                });

            var code = context.Request.QueryString["class"];
            var methodName = context.Request.QueryString["method"];

            if (!ValidateInput(context, code, methodName))
            {
                context.Response.End();
                context.Response.Close();
                return;
            }

            var paramers = context.Request.QueryString["params"];
            if (context.Request.ContentLength > 0)
            {
                using (var stream = Factory.Make<StreamReader>(context.Request.GetBufferedInputStream()))
                {
                    paramers += (string.IsNullOrWhiteSpace(paramers) ? "" : "/") + stream.ReadToEnd();
                }
            }

            if (string.IsNullOrWhiteSpace(paramers))
            {
                Send(context, HttpStatusCode.NotFound, "Unknown Parameters");
                return;
            }

            /*
                Load the Gateway, find the API and the create the entry point to it
             */
            var method = GetMethod(context, code, methodName, paramers);
            if (!method.isvalid) return;

            try
            {
                Send(context, HttpStatusCode.OK, method.method.Invoke(method.api, method.parameters.ToArray()));
            }
            catch (GatewayBaseException be)
            {
                Factory.Make<IExceptionLoggerRepository>().LogException(be);
                Send(context, be.Code, be.I18NError);
            }
            catch (Exception ex)
            {
                Factory.Make<IExceptionLoggerRepository>().LogUnhandledException(ex);
                Send(context, HttpStatusCode.InternalServerError, context.Request.IsLocal ? ex.GetFullMessage() : "System.UnhandledException");
            }

            stopwatch.Stop();

            Safe.Execute(() => Factory.Make<IAnalyticsLoggerRepository>().LogExecutionTime(context.Server.MapPath("~/"), stopwatch.Elapsed, "Kinnser.Gatewway.Call", code, methodName, paramers));

            Factory.Dispose();

            context.Response.End();
            context.Response.Close();
        }

        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="code">The code.</param>
        /// <param name="methodname">The methodname.</param>
        /// <returns></returns>
        private static bool ValidateInput(HttpContext context, string code, string methodname)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                Send(context, HttpStatusCode.NotFound, "Unknown Class");
                return false;
            }

            if (string.IsNullOrWhiteSpace(methodname))
            {
                Send(context, HttpStatusCode.NotFound, "Unknown method");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="code">The code.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="paramers">The paramers.</param>
        /// <returns></returns>
        private static (bool isvalid, MethodInfo method, List<object> parameters, object api) GetMethod(HttpContext context, string code, string methodName, string paramers)
        {
            var assembly = Assembly.LoadFrom(Path.Combine(context.Server.MapPath("~/bin/"), "Kinnser.Gateway.Interfaces.dll"));
            var type = assembly.GetType("Kinnser.Gateway.Interfaces.API.I" + code);
            var api = Factory.Make<object>(type, null);

            if (api == null)
            {
                Send(context, HttpStatusCode.NotFound, "The class you provided could not be instantiated");
                return (false, null, null, null);
            }

            var method = type.GetMethod(methodName);
            if (method == null)
            {
                Send(context, HttpStatusCode.NotFound, "The method could not be found");
                return (false, null, null, null);
            }

            var invokeParams = Factory.Make<List<object>>();
            var urlParams = paramers.Split('/');
            var parameters = method.GetParameters();

            var ctr = 0;
            invokeParams.AddRange(urlParams.Select(param => GetValue(parameters[ctr++], param)));
            return (true, method, invokeParams, api);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="paramType">Type of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <returns></returns>
        private static object GetValue(ParameterInfo paramType, string paramValue)
        {
            var type = Nullable.GetUnderlyingType(paramType.ParameterType) ?? paramType.ParameterType;
            var actual = type == typeof(Guid) ? Guid.Parse(paramValue) : Convert.ChangeType(paramValue, type);
            return actual;
        }

        /// <summary>
        /// Sends the specified response back to the originator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="code">The code.</param>
        /// <param name="response">The response.</param>
        private static void Send<T>(HttpContext context, HttpStatusCode code, T response)
        {
            if (!context.Response.IsClientConnected) return;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            context.Response.Write(JsonConvert.SerializeObject(new Result
            {
                Message = (code == HttpStatusCode.OK ? "" : (string)(object)response),
                Data = code == HttpStatusCode.OK ? Factory.Make<ICrypto>().ToJson(response) : null,
                Code = (int)code
            }));

            context.Response.Flush();
        }
    }
}
