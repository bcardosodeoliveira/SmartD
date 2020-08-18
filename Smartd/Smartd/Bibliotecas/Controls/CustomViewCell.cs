using Xamarin.Forms;

namespace Smartd.Bibliotecas.Controls
{
    public class CustomViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedItemBackgroundColorProperty = BindableProperty.Create("SelectedItemBackgroundColor", typeof(Color), typeof(CustomViewCell), default);

        public Color SelectedItemBackgroundColor
        {
            get => (Color)GetValue(SelectedItemBackgroundColorProperty);
            set => SetValue(SelectedItemBackgroundColorProperty, value);
        }
    }
}
