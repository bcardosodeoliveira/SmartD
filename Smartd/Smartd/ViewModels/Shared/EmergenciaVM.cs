using Rg.Plugins.Popup.Services;
using Smartd.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Shared
{
    public class EmergenciaVM : BaseVM
    {
        #region Váriaveis
        #endregion

        #region Commands
        public Command cmdFechar => new Command(async () => await btnFechar_Click());
        public Command cmdLigarEmergencia => new Command(async () => LigarEmergencia());
        #endregion

        public EmergenciaVM(Page tela) : base(tela)
        {
            Tela = tela;

        }

        #region Métodos
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

        public async Task LigarEmergencia()
        {
            if (!IsBusy)
            {

                try
                {                
                    IsBusy = true;
                    
                    await Task.Delay(100);

                    Device.OpenUri(new Uri($"tel:{192}"));

                }
                catch (Exception ex)
                {
                    await Tela.Alerta(ex.Message);
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
