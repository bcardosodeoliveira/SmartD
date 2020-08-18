using Smartd.Bibliotecas;
using Smartd.Bibliotecas.Helps;
using Smartd.Controllers;
using Smartd.Models;
using Smartd.Views;
using Smartd.Views.Login;
using System;
using Xamarin.Forms;

namespace Smartd
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzAyNTc2QDMxMzgyZTMyMmUzMGZFTW1CYjBteUgrRFNXRzREbjc3ZXRJUk5IRUpEZ3JONlc5SnNscWUxeDA9");

            InitializeComponent();

            MainPage = new LoadingPage();
        }

        protected async override void OnStart()
        {
            try
            {
                await AsyncHelper.Wait();

                PacienteController pacienteController = new PacienteController();

                Usuario usuario = await pacienteController.GetSession();

                if (usuario?.validadeToken > DateTime.MinValue && usuario?.validadeToken > DateTime.UtcNow)
                    MainPage = new NavigationPage(new Views.Home.IndexPage());
                else
                    MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {
                MainPage = new NavigationPage(new LoginPage());
                Log.Error(ex);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
