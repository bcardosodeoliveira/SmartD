using Benassi.Services.Interfaces;
using Smartd.Models;
using Smartd.Models.Request.Paciente;
using Smartd.Models.Responses;
using Smartd.Models.Responses.Paciente;
using System.Linq;
using System.Threading.Tasks;

namespace Smartd.Controllers
{
    public class PacienteController : CoreController
    {
        private IPacienteApi _pacienteApi;

        public PacienteController()
        {
            _pacienteApi = RestServiceFor<IPacienteApi>();
        }

        public async Task<AccessTokenResponse.AccessToken> AccessToken(string cpf, string senha) =>
            await ExecuteWithCatches
            (
                new object[]
                {
                    cpf,
                    senha
                },
                async args => await _pacienteApi.AccessToken
                (
                    (string)args[0],
                    (string)args[1]
                ).ConfigureAwait(false)
            );


        public async Task<ResponseCore> Post(PacienteRequest.Paciente paciente) =>
            await ExecuteWithCatches
            (
                new object[]
                {
                    paciente
                },
                async args => await _pacienteApi.Post
                (
                    (PacienteRequest.Paciente)args[0]
                ).ConfigureAwait(false)
            );

        public async Task<Usuario> GetSession() => (await db.GetAll<Usuario>()).FirstOrDefault();
    }
}