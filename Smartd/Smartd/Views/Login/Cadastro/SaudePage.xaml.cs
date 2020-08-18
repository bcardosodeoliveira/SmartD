using Smartd.ViewModels.Login.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Login.Cadastro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaudePage : ContentView
    {
        public SaudePage()
        {
            InitializeComponent();
            //BindingContext = new SaudeVM(this);
        }
    }
}