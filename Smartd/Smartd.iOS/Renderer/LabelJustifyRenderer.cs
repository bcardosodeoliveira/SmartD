using Smartd.Bibliotecas.Controls;
using Smartd.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LabelJustifyControl), typeof(LabelJustifyRenderer))]
namespace Smartd.iOS.Renderer
{
    public class LabelJustifyRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var el = Element as LabelJustifyControl;
            if (el != null && el.JustifyText && Control != null)
                Control.TextAlignment = UITextAlignment.Justified;
        }
    }
}