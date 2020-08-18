using Smartd.Bibliotecas;
using Smartd.Bibliotecas.Helps;
using Smartd.Views.Login.Cadastro;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Login.Cadastro
{
    public class IndexVM : BaseVM
    {
        #region Variáveis
        #endregion

        #region Commands
        public Command cmdIniciarCadastro { get; private set; }

        public Command cmdFechar { get; private set; }
        #endregion

        public IndexVM(Page tela) : base(tela)
        {
            cmdIniciarCadastro = new Command(async () => await IniciarCadastro());
            cmdFechar = new Command(async () => await Fechar());
        }

        #region Métodos
        public async Task IniciarCadastro()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await AsyncHelper.Wait();

                    await Tela.Navegar(new InicioPage());
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public async Task Fechar()
        {
            if (!IsBusy)
            {
                try
                {
                    await Tela.FecharModal();
                }
                catch (Exception ex)
                {
                    await Tela.Alerta(ex?.Message);
                }
            }
        }
        #endregion
    }
}
