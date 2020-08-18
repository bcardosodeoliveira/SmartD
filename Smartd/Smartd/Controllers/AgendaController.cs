using Benassi.Services.Interfaces;
using Smartd.Models.Request.Agenda;
using Smartd.Models.Responses;
using Smartd.Models.Responses.Agenda;
using System.Threading.Tasks;

namespace Smartd.Controllers
{
    public class AgendaController : CoreController
    {
        private IAgendaApi _agendaApi;
        private PacienteController _pacienteController;

        public AgendaController()
        {
            _agendaApi = RestServiceFor<IAgendaApi>();
            _pacienteController = new PacienteController();
        }

        public async Task<PacienteResponse.Agenda> Paciente() =>
            await ExecuteWithCatches
            (
                new object[]
                {
                },
                async args =>
                {
                    var sessao = await _pacienteController.GetSession();

                    return new PacienteResponse.Agenda()
                    {
                        Items = await _agendaApi.Paciente
                        (
                            sessao.idPaciente,
                            sessao.tokenHeader
                        ).ConfigureAwait(false)
                    };
                }
            );

        public async Task<DisponibilidadeResponse.Disponibilidade> Disponibilidade(string id_clinica, string data) =>
            await ExecuteWithCatches
            (
                new object[]
                {
                    id_clinica,
                    data,
                },
                async args =>
                {
                    var sessao = await _pacienteController.GetSession();

                    return new DisponibilidadeResponse.Disponibilidade()
                    {
                        Items = await _agendaApi.Disponibilidade
                        (
                            (string)args[0],
                            (string)args[1],
                            sessao.tokenHeader
                        ).ConfigureAwait(false)
                    };
                }
            );

        public async Task<ResponseCore> ReagendarSessao(string id_agenda, ReagendarSessaoRequest.ReagendarSessao reagendarSessao) =>
            await ExecuteWithCatches
            (
                new object[]
                {
                    id_agenda,
                    reagendarSessao,
                },
                async args =>
                {
                    var sessao = await _pacienteController.GetSession();

                    return await _agendaApi.ReagendarSessao
                    (
                        (string)args[0],
                        (ReagendarSessaoRequest.ReagendarSessao)args[1],
                        sessao.tokenHeader
                    ).ConfigureAwait(false);
                }
            );
    }
}