using Smartd.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefeicaoPage
    {
        private readonly RefeicaoVM viewModel;

        public RefeicaoPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RefeicaoVM(this);
        }
        private void Enviar(object sender, EventArgs e)
        {
            viewModel.cmdEnviar.Execute(true);
        }
    }
}