using Rg.Plugins.Popup.Services;
using Smartd.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Shared
{
   public class RefeicaoVM : BaseVM
    {
        #region Váriaveis
        private bool enviarVisible;
        public bool EnviarVisible { get => enviarVisible; set { enviarVisible = value; OnPropertyChanged(); } }

        //private bool finalizadoVisible;
        //public bool FinalizadoVisible { get => finalizadoVisible; set { finalizadoVisible = value; OnPropertyChanged(); } }

        #endregion

        #region Commands

        public Command cmdEnviar => new Command<bool>(async (obj) => Enviar(obj));
        public Command cmdFechar => new Command(async () => await btnFechar_Click());

        #endregion

        public RefeicaoVM(Page tela) : base(tela)
        {
            Tela = tela;

            EnviarVisible = false;
            //FinalizadoVisible = false;
        }

        #region Métodos
        public void Enviar(bool visible)
        {
            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    EnviarVisible = visible;
                    //FinalizadoVisible = visible;

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public async Task btnFechar_Click()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await Task.Delay(100);

                    await PopupNavigation.Instance.PopAsync();
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
