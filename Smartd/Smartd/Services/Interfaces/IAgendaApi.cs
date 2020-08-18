using Refit;
using Smartd.Models.Request.Agenda;
using Smartd.Models.Responses;
using Smartd.Models.Responses.Agenda;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Benassi.Services.Interfaces
{
    [Headers("Content-Type: application/json", "Connection: close")]
    public interface IAgendaApi
    {
        [Get("/agenda/paciente/{paciente_id}")]
        Task<List<PacienteResponse.Item>> Paciente([Query] string paciente_id, [Header("Authorization")] string token);

        [Get("/agenda/disponibilidade")]
        Task<List<string>> Disponibilidade([Header("id_clinica")] string id_clinica, [Header("data")] string data, [Header("Authorization")] string token);

        [Post("/agenda/reagendar_sessao/{id_agenda}")]
        Task<ResponseCore> ReagendarSessao([Query] string id_agenda, [Body] ReagendarSessaoRequest.ReagendarSessao param,  [Header("Authorization")] string token);
    }
}