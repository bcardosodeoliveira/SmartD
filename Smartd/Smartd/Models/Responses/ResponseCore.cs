using Smartd.Models.Responses.Interfaces;
using System;

namespace Smartd.Models.Responses
{
    public class ResponseCore : IResponseCore
    {
        public Enum HTTPStatusCode { get; set; }
        public ResponseError Error { get; set; }
        public string message { get; set; }
        public string mensagem { get; set; }
        public bool? sucesso { get; set; }
    }
}
