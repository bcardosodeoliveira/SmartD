using Refit;
using Smartd.Models.Request.Paciente;
using Smartd.Models.Responses;
using Smartd.Models.Responses.Paciente;
using System.Threading.Tasks;

namespace Benassi.Services.Interfaces
{
    [Headers("Content-Type: application/json", "Connection: close")]
    public interface IPacienteApi
    {
        [Get("/paciente/access/token")]
        Task<AccessTokenResponse.AccessToken> AccessToken([Header("cpf")] string cpf, [Header("senha")] string senha);

        [Post("/paciente")]
        Task<ResponseCore> Post([Body] PacienteRequest.Paciente param);
    }
}
