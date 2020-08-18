using Benassi.Services.Interfaces;
using Smartd.Models.Responses.Cep;
using System.Threading.Tasks;

namespace Smartd.Controllers
{
    public class CepController : CoreController
    {
        private ICepApi _cepApi;

        public CepController()
        {
            _cepApi = RestServiceFor<ICepApi>("https://viacep.com.br");
        }

        public async Task<CepResponse.Cep> Get(string cep) =>
            await ExecuteWithCatches
            (
                new object[] 
                {
                    cep
                },
                async args => await _cepApi.Get((string)args[0]).ConfigureAwait(false)
            );
    }
}