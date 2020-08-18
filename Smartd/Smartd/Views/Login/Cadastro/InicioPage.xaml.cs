using Smartd.ViewModels.Login.Cadastro;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Login.Cadastro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioPage : ContentPage
    {
        public InicioPage()
        {
            InitializeComponent();
            BindingContext = new InicioVM(this);
        }

        protected override bool OnBackButtonPressed() => true;
    }
}