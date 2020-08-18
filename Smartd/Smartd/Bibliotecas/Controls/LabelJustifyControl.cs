using System;
using Xamarin.Forms;

namespace Smartd.Bibliotecas.Controls
{
    public class LabelJustifyControl : Label
    {
        public static readonly BindableProperty JustifyTextProperty =
            BindableProperty.Create(
                propertyName: nameof(JustifyText),
                returnType: typeof(Boolean),
                declaringType: typeof(LabelJustifyControl),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay
         );

        public bool JustifyText
        {
            get { return (Boolean)GetValue(JustifyTextProperty); }
            set { SetValue(JustifyTextProperty, value); }
        }
    }
}
