using Smartd.ViewModels.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private bool isOk;
        private bool _expirou;
        private LoginVM viewModel;
        public LoginPage(bool expirou =false)
        {
            InitializeComponent();
            BindingContext = viewModel  = new LoginVM(this);
            isOk = false;
            _expirou = expirou;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_expirou && !isOk)
            {
                await viewModel.Limpar();
                isOk = true;
            }
        }
    }
}