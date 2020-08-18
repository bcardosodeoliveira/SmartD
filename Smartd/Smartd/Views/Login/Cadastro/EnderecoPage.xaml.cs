
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Login.Cadastro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnderecoPage : ContentView
    {
        public EnderecoPage()
        {
            InitializeComponent();
            //BindingContext = new EnderecoVM(this);
        }
    }
}