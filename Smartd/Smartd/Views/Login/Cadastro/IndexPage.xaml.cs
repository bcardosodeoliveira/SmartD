using Smartd.ViewModels.Login.Cadastro;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Login.Cadastro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPage : ContentPage
    {
        public IndexPage()
        {
            InitializeComponent();
            BindingContext = new IndexVM(this);
        }
    }
}