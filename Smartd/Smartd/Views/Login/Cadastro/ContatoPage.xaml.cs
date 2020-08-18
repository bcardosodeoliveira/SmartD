using Smartd.ViewModels.Login.Cadastro;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Login.Cadastro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContatoPage : ContentView
    {
        public ContatoPage()
        {
            InitializeComponent();
            //BindingContext = new ContatoVM(this);
        }
    }
}