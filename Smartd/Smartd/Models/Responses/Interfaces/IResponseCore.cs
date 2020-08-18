using System;
using System.Collections.Generic;
using System.Text;

namespace Smartd.Models.Responses.Interfaces
{
    public interface IResponseCore
    {
        Enum HTTPStatusCode { get; set; }
        ResponseError Error { get; set; }
        string message { get; set; }
        string mensagem { get; set; }
    }
}
