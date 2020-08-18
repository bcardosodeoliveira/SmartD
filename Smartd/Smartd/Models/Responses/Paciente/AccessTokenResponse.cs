namespace Smartd.Models.Responses.Paciente
{
    public class AccessTokenResponse
    {
        public class AccessToken : ResponseCore
        {
            public string token { get; set; }
        }
    }
}
