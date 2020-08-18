using Smartd.Bibliotecas;
using Smartd.Views.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Login
{
   public class NovaSenhaVM : BaseVM
    {
        #region Variáveis
        #endregion

        #region Commands
        public Command cmdEnviar => new Command(async () => await btnEnviarEmail_Click());
        public Command cmdVoltar => new Command(async () => await btnVoltar_Click());
        #endregion

        public NovaSenhaVM(Page tela) : base(tela)
        {
            Tela = tela;
        }

        #region Métodos
        public async Task btnVoltar_Click()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    await Task.Delay(100);
                    await Tela.FecharModal();


                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public async Task btnEnviarEmail_Click()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    await Task.Delay(100);
                    await Tela.AbrirModal(new NavigationPage(new EmailPage()));


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
