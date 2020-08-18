using System;
using Xamarin.Forms;

namespace Smartd.Bibliotecas.Controls
{
    public class ListViewHeaderFix : ListView
    {
        public event EventHandler EventScrollToTop;

        //-------------------------------------------------------------------
        public void ScrollToTop(bool animate = true)
        //-------------------------------------------------------------------
        {
            //bool animate is not used at this stage, it's always animated.
            EventScrollToTop?.Invoke(this, EventArgs.Empty);
        }
    }
}
