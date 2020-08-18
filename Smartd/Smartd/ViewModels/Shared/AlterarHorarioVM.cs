using Smartd.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.ViewModels.Shared
{
    public class AlterarHorarioVM : BaseVM
    {

        #region Váriaveis
        private bool disponivelVisible;
        public bool DisponivelVisible { get => disponivelVisible; set { disponivelVisible = value; OnPropertyChanged(); } }

        private bool selecionarVisible;
        public bool SelecionarVisible { get => selecionarVisible; set { selecionarVisible = value; OnPropertyChanged(); } }
        #endregion

        #region Commands

        public Command cmdSelecionar => new Command<bool>(async (obj) => Selecionar(obj));
        public Command cmdCancelar => new Command(async () => await btnCancelar_Click());

        #endregion

        public AlterarHorarioVM(Page tela) : base(tela)
        {
            Tela = tela;
            DisponivelVisible = false;
            SelecionarVisible = true;
        }

        #region Métodos
        public void Selecionar(bool visible)
        {
            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    SelecionarVisible = false;
                    DisponivelVisible = visible;

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public async Task btnCancelar_Click()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    await Task.Delay(100);

                    await Tela.FecharModal();
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

