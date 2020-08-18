using Smartd.Bibliotecas;
using Smartd.Bibliotecas.Helps;
using Smartd.Controllers;
using Smartd.Models.Responses.Agenda;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Shared
{
    public class HistoricoVM : BaseVM
    {
        #region Controllers
        private AgendaController _agendaController { get; set; }
        #endregion

        #region Váriaveis

        private ObservableCollection<PacienteResponse.Item> lista;
        public ObservableCollection<PacienteResponse.Item> Lista
        {
            get => lista;
            set => SetProperty(ref lista, value);
        }
        #endregion

        #region Commands
        public Command cmdVoltar => new Command(async () => await btnVoltar_Click());
        public Command cmdCarregarLista => new Command(async () => await CarregarLista());

        #endregion

        public HistoricoVM(Page tela) : base(tela)
        {
            IsFirst = true;
            Lista = new ObservableCollection<PacienteResponse.Item>();
            _agendaController = new AgendaController();
        }

        #region Métodos
        public async Task CarregarLista()
        {
            if (!IsBusy)
            {
                try
                {
                    if (IsFirst)
                        IsBusy = true;
                    else
                        IsLoading = true;

                    await AsyncHelper.Wait();

                    ObservableCollection<PacienteResponse.Item> lista = new ObservableCollection<PacienteResponse.Item>();

                    PacienteResponse.Agenda response = await _agendaController.Paciente();
                    bool canContinue = await _serverErrorResponseManager.ManageResponseAsync(response, Tela);

                    if (canContinue && response?.Items?.Count > 0)
                    {
                        TimeSpan horaAtual = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));

                        foreach (var item in response.Items.Where(x => (x.data.Date == DateTime.Now.Date && TimeSpan.TryParse(x.hora, out TimeSpan hora) && hora < horaAtual) || x.data.Date < DateTime.Now.Date)?.OrderByDescending(x => x.data.Date))
                        {
                            lista.Add(item);
                        }
                    }

                    Lista = lista;
                }
                catch (Exception ex)
                {
                    await Tela.Alerta(ex?.Message);
                }
                finally
                {
                    if (IsFirst)
                        IsBusy = false;
                    else
                        IsLoading = false;

                    IsFirst = false;
                    IsEmpty = Lista.Count == 0;
                }
            }
        }

        public async Task btnVoltar_Click()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await Tela.Voltar();
                }
                catch (Exception ex)
                {
                    await Tela.Alerta(ex?.Message);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        #endregion
    }
}

