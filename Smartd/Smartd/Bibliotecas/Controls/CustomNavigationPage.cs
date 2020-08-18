using Xamarin.Forms;

namespace Smartd.Bibliotecas.Controls
{
    public class CustomNavigationPage : NavigationPage
    {
        public static readonly BindableProperty SubTitleProperty = BindableProperty.Create("SubTitle", typeof(string), typeof(CustomNavigationPage), default);

        public string SubTitle
        {
            get => (string)GetValue(SubTitleProperty);
            set => SetValue(SubTitleProperty, value);
        }
    }
}
