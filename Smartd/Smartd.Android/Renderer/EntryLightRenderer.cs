using Acr.UserDialogs.Infrastructure;
using Android.Content;
using Java.IO;
using Smartd.Bibliotecas.Controls;
using Smartd.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(EntryLight), typeof(EntryLightRenderer))]
namespace Smartd.Droid.Renderer
{
    public class EntryLightRenderer : EntryRenderer
    {
        public EntryLightRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //GradientDrawable gd = new GradientDrawable();
                //gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);

                //Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.Gray));
            }
        }
    }
}