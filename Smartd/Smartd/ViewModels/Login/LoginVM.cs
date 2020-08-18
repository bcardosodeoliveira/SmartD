using Smartd.Bibliotecas;
using Smartd.Bibliotecas.Helps;
using Smartd.Models;
using Smartd.Models.Responses.Paciente;
using Smartd.Views.Login;
using Smartd.Views.Login.Cadastro;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Login
{
    public class LoginVM : BaseVM
    {
        #region Variáveis
        private Usuario dados;

        public Usuario Dados
        {
            get => dados;
            set => SetProperty(ref dados, value);
        }
        #endregion

        #region Commands
        public Command cmdEntrar { get; private set; }
        public Command cmdEsqueciSenha { get; private set; }
        public Command cmdCadastro { get; private set; }
        public Command cmdValidarLogin { get; private set; }
        public Command cmdValidarSenha { get; private set; }
        #endregion

        public LoginVM(Page tela) : base(tela)
        {
#if DEBUG
            Dados = new Usuario()
            {
                login = "222.222.222-22",
                senha = "123@trocar"
            };
#else
            Dados = new Usuario();
#endif

            cmdEntrar = new Command(async () => await Entrar());
            cmdEsqueciSenha = new Command(async () => await EsqueciSenha());
            cmdCadastro = new Command(async () => await Cadastro());
            cmdValidarLogin = new Command(() => Dados.Login.Validate());
            cmdValidarSenha = new Command(() => Dados.Senha.Validate());
        }

        #region Métodos
        public async Task Entrar()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    if (Dados.Validate())
                    {
                        AccessTokenResponse.AccessToken response = await _pacienteController.AccessToken(Dados.cpf, Dados.Senha.Value);
                        bool canContinue = await _serverErrorResponseManager.ManageResponseAsync(response, Tela);
                        if (canContinue && !response.token.IsNullOrEmpty())
                        {
                            await db.DropTable<Usuario>();
                            //await db.DropTable<Login>();

                            Dados.token = response.token;

                            JwtSecurityToken token = Funcoes.decodeHS256(Dados.token);

                            Dados.idPaciente = token.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                            Dados.nome = token.Claims.FirstOrDefault(c => c.Type == "nome_completo")?.Value;
                            Dados.email = token.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                            Dados.validadeToken = token.ValidTo;

                            await db.Save(Dados);

                            //if (Dados.manterConectado)
                            //{
                            //    await db.Save
                            //    (
                            //        new Login
                            //        {
                            //            login = Dados.Login.Value,
                            //            senha = Dados.Senha.Value
                            //        }
                            //    );
                            //}

                            Application.Current.MainPage = new NavigationPage(new Views.Home.IndexPage());
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

        public async Task EsqueciSenha()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await Tela.AbrirModal(new NavigationPage(new NovaSenhaPage()));
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

        public async Task Cadastro()
        {
            if (!IsBusy)
            {

                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await Tela.AbrirModal(new NavigationPage(new IndexPage()));

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

        public async Task Limpar()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await db.DropTable<Usuario>();

                    await Tela.Alerta("Sessão expirada!\nFaça o login novamente.");
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
