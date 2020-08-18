using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Smartd.ascascaDroid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(ShowHidePassEffect), "ShowHidePassEffect")]

namespace Smartd.ascascaDroid.Effects
{
    public class ShowHidePassEffect : PlatformEffect
    {
        private Android.Graphics.Color cor { get; set; }
        protected override void OnAttached()
        {
            ConfigureControl();
        }

        protected override void OnDetached()
        {
        }

        private void ConfigureControl()
        {
            EditText editText = ((EditText)Control);
            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.avd_hide_password, 0);
            editText.SetOnTouchListener(new OnDrawableTouchListener());

            foreach (Drawable drawable in editText.GetCompoundDrawables())
            {
                var color = new Android.Graphics.Color(editText.CurrentTextColor);

                if (drawable != null)
                    drawable.SetColorFilter(new PorterDuffColorFilter(color, PorterDuff.Mode.SrcIn));
            }
        }
    }

    public class OnDrawableTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (v is EditText && e.Action == MotionEventActions.Up)
            {
                EditText editText = (EditText)v;
                if (e.RawX >= (editText.Right - editText.GetCompoundDrawables()[2].Bounds.Width()))
                {
                    if (editText.TransformationMethod == null)
                    {
                        editText.TransformationMethod = PasswordTransformationMethod.Instance;
                        editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.avd_hide_password, 0);
                        editText.SetSelection(editText.Length());
                    }
                    else
                    {
                        editText.TransformationMethod = null;
                        editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.avd_show_password, 0);
                        editText.SetSelection(editText.Length());
                    }

                    foreach (Drawable drawable in editText.GetCompoundDrawables())
                    {
                        var color = new Android.Graphics.Color(editText.CurrentTextColor);
                        if (drawable != null)
                            drawable.SetColorFilter(new PorterDuffColorFilter(color, PorterDuff.Mode.SrcIn));
                    }

                    return true;
                }
            }

            return false;
        }
    }

}