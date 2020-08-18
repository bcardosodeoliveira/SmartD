using System;
using System.Collections.Generic;
using System.Text;

namespace Smartd.Models.Responses
{
   public class ResponseError
    {
        public const string ERROR_UNAUTHORIZED = "UNAUTHORIZED";
        public const string ERROR_UNKNOWN_API_VERSION = "UNKNOWN_API_VERSION";
        public const string ERROR_ACCOUNT_UNCONFIRMED = "account_unconfirmed";

        public string Code { get; set; }
        public string Message { get; set; }
        public string Debug { get; set; }

        public ResponseError()
        {

        }

        public ResponseError(string pCode, string pMessage)
        {
            Code = pCode;
            Message = pMessage;
        }
    }
}
