using Smartd.Bibliotecas;
using Smartd.Bibliotecas.Helps;
using Smartd.Bibliotecas.Validations;
using Smartd.Controllers;
using Smartd.Models.Request.Paciente;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Login.Cadastro
{

    public class InicioVM : BaseVM
    {
        #region Variáveis
        private CepController _cepController { get; set; }

        private int _indexWizard;
        public int IndexWizard
        {
            get => _indexWizard;
            set => SetProperty(ref _indexWizard, value);
        }

        private PacienteRequest.Paciente _dados;
        public PacienteRequest.Paciente Dados
        {
            get => _dados;
            set => SetProperty(ref _dados, value);
        }

        public ObservableCollection<Funcoes.Uf> ListaUF { get; set; }
        #endregion

        #region Commands
        public Command cmdVoltar { get; private set; }
        public Command cmdAvancar { get; private set; }
        public Command cmdValidar { get; private set; }
        public Command cmdAbrirTab { get; private set; }
        public Command cmdValidarSenha { get; private set; }
        public Command cmdValidarConfirmacaoSenha { get; private set; }
        public Command cmdValidarUF { get; private set; }
        public Command cmdValidarCEP { get; private set; }
        #endregion

        public InicioVM(Page tela) : base(tela)
        {
            Tela = tela;
            ListaUF = Funcoes.ListaUf(null, null);
            Dados = new PacienteRequest.Paciente();

            _cepController = new CepController();

            cmdVoltar = new Command(async () => await Voltar());
            cmdAvancar = new Command(async () => await Avancar());
            cmdValidarCEP = new Command(async () => await ValidarCEP());
            cmdAbrirTab = new Command<string>(async (index) => await AbrirTab(index.ToInt()));
            cmdValidar = new Command((obj) =>
            {
                if (obj is ValidatableObject<string> campoString)
                    campoString.Validate();
                else if (obj is ValidatableObject<DateTime?> campoDate)
                    campoDate.Validate();
            });
            cmdValidarSenha = new Command(() =>
            {
                Dados.Senha.Validate();
                if (!Dados.ConfirmarSenha.Value.IsNullOrEmpty())
                    Dados.ConfirmarSenha.Validate();
            });
            cmdValidarConfirmacaoSenha = new Command(() =>
            {
                Dados.ConfirmarSenha.Validate();
                if (!Dados.Senha.Value.IsNullOrEmpty())
                    Dados.Senha.Validate();
            });
        }

        #region Métodos
        public async Task Avancar()
        {
            if (!IsBusy)
            {

                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    if (Dados.Validate(IndexWizard))
                    {
                        if (IndexWizard < 4)
                            IndexWizard++;
                        else
                        {
                            if (!Dados.TemResponsavel)
                                Dados.responsavel = null;

                            var response = await _pacienteController.Post(Dados);
                            bool sucesso = await _serverErrorResponseManager.ManageResponseAsync(response, Tela);
                            if (sucesso)
                            {
                                await Tela.FecharModal();
                                Funcoes.Toast("Salvo com sucesso.");
                            }
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

        public async Task AbrirTab(int index)
        {
            //if (!IsBusy)
            //{

            //    try
            //    {
            //        IsBusy = true;

            //        await AsyncHelper.Wait();

            //        if (index > IndexWizard)
            //        {
            //            if (Dados.Validate(IndexWizard))
            //                IndexWizard = index;
            //        }
            //        else
            //            IndexWizard = index;
            //    }
            //    catch (Exception ex)
            //    {
            //        await Tela.Alerta(ex?.Message);
            //    }
            //    finally
            //    {
            //        IsBusy = false;
            //    }
            //}
        }

        public async Task Voltar()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    if (IndexWizard == 0 && await Tela.Confirmacao("Deseja realmente sair?"))
                        await Tela.Voltar();
                    else if (IndexWizard > 0)
                        IndexWizard--;
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

        private async Task ValidarCEP()
        {
            try
            {

                if (Dados.endereco.CEP.Validate())
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    var response = await _cepController.Get(Dados.endereco.CEP.Value);
                    bool canContinue = await _serverErrorResponseManager.ManageResponseAsync(response, Tela);
                    if (canContinue && response != null)
                    {
                        Dados.endereco.Logradouro.Value = response.logradouro;
                        Dados.endereco.complemento = response.complemento;
                        Dados.endereco.Bairro.Value = response.bairro;
                        Dados.endereco.Cidade.Value = response.localidade;
                        Dados.endereco.UF.Value = response.uf;

                        Dados.endereco.Logradouro.Validate();
                        Dados.endereco.Bairro.Validate();
                        Dados.endereco.Cidade.Validate();
                        Dados.endereco.UF.Validate();

                        if (!Dados.endereco.UF.Value.IsNullOrEmpty())
                            Dados.IndexUF = ListaUF.IndexOf(ListaUF.Where(x => x.UF == response.uf).FirstOrDefault());
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
        #endregion
    }
}
