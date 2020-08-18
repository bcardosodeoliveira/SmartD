using Smartd.ViewModels.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoricoPage : ContentPage
    {
        private readonly HistoricoVM viewModel;
        private bool isFirst;
        public HistoricoPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HistoricoVM(this);
            isFirst = true;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (isFirst)
            {
                await viewModel.CarregarLista();
                isFirst = false;
            }
        }
    }
}