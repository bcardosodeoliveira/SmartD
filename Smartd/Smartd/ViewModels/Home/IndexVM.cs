using Rg.Plugins.Popup.Services;
using Smartd.Bibliotecas;
using Smartd.Bibliotecas.Helps;
using Smartd.Controllers;
using Smartd.Models.Responses.Agenda;
using Smartd.ViewModels.Shared;
using Smartd.Views.Shared;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Home
{
    public class IndexVM : BaseVM
    {
        #region Controllers
        private AgendaController _agendaController { get; set; }
        #endregion

        #region Variáveis
        private ObservableCollection<PacienteResponse.Item> listaProximas;
        private PacienteResponse.Item ultimaSessao;
        private PacienteResponse.Item proximaSessao;

        public PacienteResponse.Item UltimaSessao
        {
            get => ultimaSessao;
            set
            {
                SetProperty(ref ultimaSessao, value);
                OnPropertyChanged("HasUltimaSessao");
            }
        }
        public PacienteResponse.Item ProximaSessao
        {
            get => proximaSessao;
            set
            {
                SetProperty(ref proximaSessao, value);
                OnPropertyChanged("HasProximaSessao");
            }
        }
        public ObservableCollection<PacienteResponse.Item> ListaProximas
        {
            get => listaProximas;
            set =>SetProperty(ref listaProximas, value);
        }
        public bool HasProximaSessao => ProximaSessao?.id.ToNull() != null;
        public bool HasUltimaSessao => UltimaSessao?.id.ToNull() != null;
        #endregion

        #region Commands
        public Command cmdHistorico => new Command(async () => await btnHistorico_Click());
        public Command cmdAlterarAgenda => new Command(async () => await btnAlterarAgenda_Click());
        public Command cmdRefeicao => new Command(async () => await btnRefeicao_Click());
        public Command cmdEmergencia => new Command(async () => await btnEmergencia_Click());
        #endregion

        public IndexVM(Page tela) : base(tela)
        {
            UltimaSessao = new PacienteResponse.Item();
            ProximaSessao = new PacienteResponse.Item();
            _agendaController = new AgendaController();
            ListaProximas = new ObservableCollection<PacienteResponse.Item>();

            MessagingCenter.Unsubscribe<CalendarioVM>(this, "CarregarHome");
            MessagingCenter.Unsubscribe<CalendarioPage>(this, "CarregarHome");
            MessagingCenter.Subscribe<CalendarioVM>(this, "CarregarHome", async (obj) => {
                await CarregarDados();
            });
            MessagingCenter.Subscribe<CalendarioPage>(this, "CarregarHome", async (obj) => {
                await CarregarDados();
            });
        }

        #region Métodos
        public async Task CarregarDados()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    PacienteResponse.Agenda response = await _agendaController.Paciente();
                    bool canContinue = await _serverErrorResponseManager.ManageResponseAsync(response, Tela);
                    if (canContinue && response?.Items?.Count > 0)
                    {
                        TimeSpan horaAtual = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
                        var proximaSessao = response.Items.Where(x => (x.data.Date == DateTime.Now.Date && TimeSpan.TryParse(x.hora, out TimeSpan hora) && hora > horaAtual) || x.data.Date > DateTime.Now.Date)?.OrderBy(x => x.data.Date)?.FirstOrDefault();

                        if (proximaSessao != null)
                            ProximaSessao = proximaSessao;
                        
                        var ultimaSessao = response.Items.Where(x => (x.data.Date == DateTime.Now.Date && TimeSpan.TryParse(x.hora, out TimeSpan hora) && hora < horaAtual) || x.data.Date < DateTime.Now.Date)?.OrderByDescending(x => x.data.Date)?.FirstOrDefault();
                        
                        if (ultimaSessao != null)
                            UltimaSessao = ultimaSessao;
                        
                        ListaProximas.Clear();
                        foreach (var item in response.Items.Where(x => (x.data.Date == DateTime.Now.Date && TimeSpan.TryParse(x.hora, out TimeSpan hora) && hora > horaAtual) || x.data.Date > DateTime.Now.Date)?.OrderBy(x => x.data.Date))
                            ListaProximas.Add(item);
                    }
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

        public async Task btnHistorico_Click()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await Tela.Navegar(new HistoricoPage());
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
        public async Task btnAlterarAgenda_Click()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await Tela.Navegar(new CalendarioPage(ListaProximas));
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

        public async Task btnRefeicao_Click()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await PopupNavigation.Instance.PushAsync(new RefeicaoPage());
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

        public async Task btnEmergencia_Click()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await PopupNavigation.Instance.PushAsync(new EmergenciaPage());
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
