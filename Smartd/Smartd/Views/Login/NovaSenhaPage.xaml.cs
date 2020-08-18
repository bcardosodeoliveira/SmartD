using Smartd.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovaSenhaPage : ContentPage
    {
        public NovaSenhaPage()
        {
            InitializeComponent();
            BindingContext = new NovaSenhaVM(this);
        }
    }
}