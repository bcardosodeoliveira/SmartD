using Smartd.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPage : ContentPage
    {
        private IndexVM viewModel;
        private bool isFirst;
        public IndexPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new IndexVM(this);
            isFirst = true;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (isFirst)
            {
                await viewModel.CarregarDados();

                isFirst = false;
            }
        }
    }
}