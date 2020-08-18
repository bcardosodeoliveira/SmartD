using Smartd.Bibliotecas;
using Smartd.Constances;
using Smartd.Models.Responses;
using Smartd.Models.Responses.Interfaces;
using Smartd.Storage;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Smartd.Controllers
{
    public abstract class CoreController
    {
        public DatabaseManager db { get; private set; }

        protected DefaultErrorHandler DefaultErrorHandler;

        protected CoreController()
        {
            db = new DatabaseManager();
            DefaultErrorHandler = new DefaultErrorHandler();
        }

        protected T RestServiceFor<T>(string pstrUrl = null)
        {
            if (pstrUrl.IsNullOrEmpty())
                pstrUrl = Configuracao.UrlApi;

            return RestService.For<T>
            (
                new HttpClient
                {
                    BaseAddress = new Uri(pstrUrl),
                    Timeout = Configuracao.TimeoutApi
                },
                GetRefitJsonSettings()
            );
        }

        protected async Task<IResponseCore> CheckForConnectivityProblems(IResponseCore response, Exception exception)
        {
            if (await IsInternetConnectionActive())
            {
                if (exception is TaskCanceledException || exception is OperationCanceledException)
                {
                    response = GetResponseCoreError(MHttpStatusCode.TIMEOUT, null, response);
                }
                else
                {
                    bool serverOnline = await IsServerReachable(Configuracao.UrlApi);
                    if (serverOnline)
                        response = GetResponseCoreError(MHttpStatusCode.UNKNOWN_ERROR, (exception?.Message.IsNullOrEmpty() == false ? exception.Message + "\n" : "") + exception?.StackTrace, response);
                    else
                        response = GetResponseCoreError(MHttpStatusCode.SERVER_OFFLINE, (exception?.Message.IsNullOrEmpty() == false ? exception.Message + "\n" : "") + exception?.StackTrace, response);
                }
            }
            else
            {
                response = GetResponseCoreError(MHttpStatusCode.NO_INTERNET_CONNECTION, (exception?.Message.IsNullOrEmpty() == false ? exception.Message + "\n" : "") + exception?.StackTrace, response);
            }

            return response;
        }

        public async Task<bool> CheckForResponseDefaultErrorsAndOptinalRelogin(IResponseCore response, bool makeReloginIfNeeded = true)
        {
            if (response == null || response.Error == null)
                return await Task.FromResult(false);

            Log.Error<CoreController>("CheckForDefaultErrors",
                $"Error in request.\nHTTPStatus Code: {response.HTTPStatusCode}\nError Message: {response.Error.Message}");

            switch (response.HTTPStatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    DefaultErrorHandler.Unauthorized();
                    break;
                case MHttpStatusCode.NO_INTERNET_CONNECTION:
                    DefaultErrorHandler.NoInternetConnection();
                    break;
                case MHttpStatusCode.SERVER_OFFLINE:
                    DefaultErrorHandler.ServerOffline();
                    break;
                case MHttpStatusCode.UNKNOWN_API_VERSION:
                    DefaultErrorHandler.UnknownAPIVersion();
                    break;
            }

            return await Task.FromResult(false);
        }

        protected IResponseCore ParseResponseErrorJSON<T>(ApiException ex)
        {
            IResponseCore error;

            if (ex == null)
            {
                return GetResponseCoreError(MHttpStatusCode.UNKNOWN_ERROR);
            }

            if (string.IsNullOrWhiteSpace(ex.Content))
            {
                error = GetResponseCoreError(ex.StatusCode);
            }
            else
            {
                try
                {
                    if (ex.Content != "Unauthorized")
                    {
                    error = JsonConvert.DeserializeObject<T>(ex.Content,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) as ResponseCore;
                    error.HTTPStatusCode = ex.StatusCode;
                    if (error.Error != null && !string.IsNullOrEmpty(error.Error.Message))
                    {
                        error.Error.Message = error.Error.Message;
                    }

                    if (!string.IsNullOrEmpty(error.message))
                    {
                        if (error.Error == null)
                            error.Error = new ResponseError();

                        error.Error.Message = error.message;
                    }

                    if (!string.IsNullOrEmpty(error.mensagem))
                    {
                        if (error.Error == null)
                            error.Error = new ResponseError();

                        error.Error.Message = error.mensagem;
                        }
                    }
                    else
                    {
                        return GetResponseCoreError(HttpStatusCode.Unauthorized);
                    }


                }
                catch (JsonException jEx)
                {
                    error = GetResponseCoreError(MHttpStatusCode.JSON_SERIALIZE_ERROR, jEx.StackTrace);
                    Log.Error<T>("ParseResponseErrorObject", jEx.StackTrace);
                }
            }

            return error;
        }

        protected IResponseCore GetResponseCoreError(Enum code, string message = null, IResponseCore response = null)
        {
            if (response == null)
                response = new ResponseCore();

            response.HTTPStatusCode = code;

            if (!string.IsNullOrWhiteSpace(message))
            {
                response.Error = new ResponseError { Message = message };
            }

            return response;
        }

        public async Task<bool> IsInternetConnectionActive()
        {
            return await ConnectionCheck.HasInternet("google.com", 2000);
            //return CrossConnectivity.Current.IsConnected;
        }

        /// <summary>
        /// Check if the given server url is reachable.
        /// </summary>
        /// <returns>The server reachable.</returns>
        /// <param name="url">Server URL.</param>
        public async Task<bool> IsServerReachable(string url = "https://www.google.com", int msTimeout = 2000)
        {
            bool reachable = false;
            try
            {
                CrossConnectivity.Dispose();
                reachable = await ConnectionCheck.IsRemoteReachable(url, 80, msTimeout);
            }
            catch (Exception ex)
            {
                Log.Error<CoreController>("IsServerReachable", ex.Message);
            }

            return reachable;
        }

        /// <summary>
        /// Creates new refit settings for ignoring json null values 
        /// </summary>
        /// <returns>The refit settings</returns>
        public RefitSettings GetRefitJsonSettings()
        {
            return new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer
                (
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                )
            };
        }

        protected async Task<TResponse> ExecuteWithCatches<TResponse>(object[] args, Func<object[], Task<TResponse>> action)
            where TResponse : ResponseCore, new()
        {
            TResponse result;
            try
            {
                result = await action(args);
                if (result == null)
                    result = new TResponse();

                result.HTTPStatusCode = HttpStatusCode.OK;
            }
            catch (ApiException e)
            {
                var resultado = ParseResponseErrorJSON<TResponse>(e);
                result = new TResponse()
                {
                    Error = resultado.Error,
                    HTTPStatusCode = resultado.HTTPStatusCode
                };
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e.StackTrace);
                result = new TResponse();
                await CheckForConnectivityProblems(result, e);
            }

            return result;
        }
    }
}
