using Android.App;
using Android.OS;
using System.Threading.Tasks;

namespace Smartd.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        public override void OnBackPressed() { }

        async void SimulateStartup()
        {
            await Task.Delay(500);

            StartActivity(typeof(MainActivity));
        }
    }
}