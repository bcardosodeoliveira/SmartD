using Rg.Plugins.Popup.Services;
using Smartd.Bibliotecas;
using Smartd.Bibliotecas.Helps;
using Smartd.Controllers;
using Smartd.Models;
using Smartd.Models.Request.Agenda;
using Smartd.Models.Responses;
using Smartd.Models.Responses.Agenda;
using Smartd.Views.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Shared
{
    public class CalendarioVM : BaseVM
    {
        #region Controllers
        private AgendaController _agendaController { get; set; }
        #endregion

        #region Váriaveis   
        private bool hasChanged;
        private ObservableCollection<Horario> horarios;
        private List<DateTime> datasSelecionadas;
        //private List<DateTime> datasBloqueadas;
        private DateTime maxDate;
        private PacienteResponse.Item itemSelecionado;

        public bool HasChanged
        {
            get => hasChanged;
            set => SetProperty(ref hasChanged, value);
        }
        public ObservableCollection<Horario> Horarios
        {
            get => horarios;
            set => SetProperty(ref horarios, value);
        }
        public List<DateTime> DatasSelecionadas
        {
            get => datasSelecionadas;
            set => SetProperty(ref datasSelecionadas, value);
        }
        //public List<DateTime> DatasBloqueadas
        //{
        //    get => datasBloqueadas;
        //    set => SetProperty(ref datasBloqueadas, value);
        //}
        public DateTime MaxDate
        {
            get => maxDate;
            set => SetProperty(ref maxDate, value);
        }
        public PacienteResponse.Item ItemSelecionado
        {
            get => itemSelecionado;
            set => SetProperty(ref itemSelecionado, value);
        }

        public ObservableCollection<PacienteResponse.Item> Items { get; private set; }

        private bool agendadoVisible;
        public bool AgendadoVisible { get => agendadoVisible; set { agendadoVisible = value; OnPropertyChanged(); } }

        private bool selecionarVisible;
        public bool SelecionarVisible { get => selecionarVisible; set { selecionarVisible = value; OnPropertyChanged(); } }

        private bool disponivelVisible;
        public bool DisponivelVisible { get => disponivelVisible; set { disponivelVisible = value; OnPropertyChanged(); } }

        #endregion

        #region Commands
        //public Command cmdMinimizar => new Command<bool>(async (obj) => Minimizar(obj));
        public Command cmdRefeicao => new Command(async () => await btnRefeicao_Click());
        public Command cmdDisponivel => new Command(async (obj) => await Disponivel());
        public Command cmdFinalizar => new Command(async () => await Finalizar());
        public Command cmdVoltar => new Command(async () => await Voltar());
        public Command cmdSelecionarHorario => new Command<Horario>((obj) =>
        {
            Horario horario = Horarios.Where(x => x.check).FirstOrDefault();
            if (horario != null && !obj.Equals(horario))
                horario.check = false;

            obj.check = true;
        });

        #endregion

        public CalendarioVM(Page tela, ObservableCollection<PacienteResponse.Item> items) : base(tela)
        {
            Tela = tela;
            AgendadoVisible = false;
            SelecionarVisible = true;
            DisponivelVisible = false;
            HasChanged = false;
            Horarios = new ObservableCollection<Horario>();
            DatasSelecionadas = new List<DateTime>();
            //DatasBloqueadas = new List<DateTime>();
            Items = items;
            _agendaController = new AgendaController();
            List<DateTime> lista = new List<DateTime>();
            MaxDate = DateTime.Now.Date.AddYears(2);
            ItemSelecionado = new PacienteResponse.Item();

            foreach (var item in Items)
                DatasSelecionadas.Add(item.data);
        }

        public async Task CarregarDados()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    //List<DateTime> lista = new List<DateTime>();

                    //PacienteResponse.Agenda response = await _agendaController.Paciente();
                    //bool canContinue = await _serverErrorResponseManager.ManageResponseAsync(response, Tela);

                    //if (canContinue && response?.Items?.Count > 0)
                    //{
                    //    TimeSpan horaAtual = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));

                    //    foreach (var item in response.Items.Where(x => (x.data.Date == DateTime.Now.Date && TimeSpan.TryParse(x.hora, out TimeSpan hora) && hora > horaAtual) || x.data.Date > DateTime.Now.Date)?.OrderBy(x => x.data.Date))
                    //    {
                    //        DatasSelecionadas.Add(item.data);
                    //    }
                    //}

                    //DatasSelecionadas = lista;
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

        public async Task Selecionar(DateTime date)
        {
            try
            {
                //IsBusy = true;

                await AsyncHelper.Wait();

                ItemSelecionado = Items.Where(x => x.data == date).FirstOrDefault() ?? new PacienteResponse.Item();

                if (!ItemSelecionado.id.IsNullOrEmpty())
                {
                    AgendadoVisible = true;
                    SelecionarVisible = false;
                    DisponivelVisible = false;
                }
                else
                {
                    Funcoes.Toast("Data não encontrada.");
                }
            }
            catch (Exception ex)
            {
                await Tela.Alerta(ex?.Message);
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

        public async Task Disponivel()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    DisponibilidadeResponse.Disponibilidade response = await _agendaController.Disponibilidade(ItemSelecionado.clinica?.id, ItemSelecionado.data.ToString("yyyy-MM-dd"));
                    bool canContinue = await _serverErrorResponseManager.ManageResponseAsync(response, Tela);
                    if (canContinue)
                    {
                        if (response?.Items?.Count > 0)
                        {
                            Horarios.Clear();
                            foreach (string horario in response.Items)
                                Horarios.Add(new Horario(horario, horario == ItemSelecionado.hora));

                            DisponivelVisible = true;
                            SelecionarVisible = false;
                            AgendadoVisible = false;
                        }
                        else
                        {
                            Funcoes.Toast("Nenhum horário disponível para essa data.");
                        }
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

        public async Task Voltar()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    if (HasChanged)
                        MessagingCenter.Send(this, "CarregarHome");

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

        public async Task Finalizar()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    Horario horario = Horarios.Where(x => x.check).FirstOrDefault();


                    if (horario != null && await Tela.Confirmacao("Deseja realmente alterar o horário?"))
                    {
                        ResponseCore response = await _agendaController.ReagendarSessao
                        (
                            ItemSelecionado.id, 
                            new ReagendarSessaoRequest.ReagendarSessao()
                            {
                                clinica = new ReagendarSessaoRequest.Clinica()
                                {
                                    id = ItemSelecionado.clinica?.id,
                                    nome = ItemSelecionado.clinica?.nome
                                },
                                paciente = new ReagendarSessaoRequest.Paciente()
                                {
                                    id = ItemSelecionado.paciente?.id,
                                    nome_completo = ItemSelecionado.paciente?.nome_completo
                                },
                                data = ItemSelecionado.data.ToString("yyyy-MM-dd"),
                                hora = horario.horario
                            }
                        );

                        bool canContinue = await _serverErrorResponseManager.ManageResponseAsync(response, Tela);
                        if (canContinue)
                        {
                            if (!HasChanged && ItemSelecionado.hora != horario.horario)
                                HasChanged = true;

                            ItemSelecionado.hora = horario.horario;

                            AgendadoVisible = true;
                            SelecionarVisible = false;
                            DisponivelVisible = false;

                            await Tela.Alerta(response.mensagem);
                        }
                    }
                    else
                    {
                        await Tela.Alerta("Por favor, informe um Horário");
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
    }
}
