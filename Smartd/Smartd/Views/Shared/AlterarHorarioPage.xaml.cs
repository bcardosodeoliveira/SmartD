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
    public partial class AlterarHorarioPage : ContentPage
    {
        private readonly AlterarHorarioVM viewModel;
        public AlterarHorarioPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AlterarHorarioVM(this);
        }
        private void Selecionar(object sender, EventArgs e)
        {
            viewModel.cmdSelecionar.Execute(true);
        }
    }
}