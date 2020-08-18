namespace Smartd.Controllers
{
    public class DefaultErrorHandler
    {

        public void NoInternetConnection()
        {
            // ToDo Dispatch Event
        }

        /// <summary>
        /// When the server is offline after some interface request.
        /// </summary>
        public void ServerOffline()
        {
            // ToDo Dispatch Event
        }

        /// <summary>
        /// When the API version, which was send in the request header, is invalide.
        /// </summary>
        public void UnknownAPIVersion()
        {
            // ToDo Dispatch Event
        }

        /// <summary>
        /// When some access protected interface is called but the token is expired or invalide.
        /// </summary>
        public void Unauthorized()
        {
            // ToDo Dispatch Event
        }

        /// <summary>
        /// When the OAuth 2 Token expires, we make a re login with the refresh token.
        /// When this re login fails, this error will be executed.
        /// Example: In the OS application you can switch to the login screen for example.
        /// </summary>
        public void ReloginFailed()
        {
            // ToDo Dispatch Event
            // 
        }
    }
}
