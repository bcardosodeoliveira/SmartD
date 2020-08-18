using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Smartd.Bibliotecas.Controls;
using Smartd.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(PickerLight), typeof(PickerLightRenderer))]
namespace Smartd.Droid.Renderer
{
    public class PickerLightRenderer : PickerRenderer
    {
        public PickerLightRenderer(Context context) : base(context)
        {
        }

        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(5, 5, 5, 5);
                SetPadding(5, 5, 5, 5);
            }
        }
    }
}