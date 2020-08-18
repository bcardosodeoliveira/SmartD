using Android.Content;
using Android.OS;
using Android.Text;
using Smartd.Bibliotecas.Controls;
using Smartd.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LabelJustifyControl), typeof(LabelJustifyRenderer))]
namespace Smartd.Droid.Renderer
{
    public class LabelJustifyRenderer : LabelRenderer
    {
        public LabelJustifyRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var el = Element as LabelJustifyControl;
            if (el != null && el.JustifyText && Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    Control.JustificationMode = JustificationMode.InterWord;
            }
        }
    }
}