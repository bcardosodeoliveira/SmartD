using Smartd.Bibliotecas;
using Smartd.Views.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Login
{
    public class EmailVM : BaseVM
    {
        #region Variáveis
        #endregion

        #region Commands
        public Command cmdVoltar => new Command(async () => await btnVoltar_Click());
        #endregion

        public EmailVM(Page tela) : base(tela)
        {
            Tela = tela;
        }

        #region Métodos
        public async Task btnVoltar_Click()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await Task.Delay(100);

                    Application.Current.MainPage = new NavigationPage(new LoginPage());
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
