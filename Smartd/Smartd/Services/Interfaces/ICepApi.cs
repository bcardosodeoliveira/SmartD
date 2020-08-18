using Smartd.Models.Responses.Cep;
using Refit;
using System.Threading.Tasks;

namespace Benassi.Services.Interfaces
{
    [Headers("Content-Type: application/json", "Connection: close")]
    public interface ICepApi
    {
        [Get("/ws/{cep}/json/")]
        Task<CepResponse.Cep> Get([Query] string cep);
    }
}
