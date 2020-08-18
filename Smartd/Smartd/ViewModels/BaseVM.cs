using Smartd.Bibliotecas;
using Smartd.Controllers;
using Smartd.Models;
using Smartd.Models.Base;
using Smartd.Storage;
using Smartd.Views.Login;
using System;
using Xamarin.Forms;

namespace Smartd.ViewModels
{
    public class BaseVM : ObservableBaseObject
    {
        #region Controllers
        public PacienteController _pacienteController { get; private set; }
        public ServerErrorResponseManager _serverErrorResponseManager { get; private set; }
        #endregion

        #region Variáveis
        public DatabaseManager db { get; private set; }

        private bool isBusy;
        private bool isLoading;
        private bool isLoadingPagination;
        private bool isEmpty;
        private bool isFirst;
        private string title;
        private string subTitle;
        private Usuario sessao;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public bool IsLoadingPagination
        {
            get => isLoadingPagination;
            set => SetProperty(ref isLoadingPagination, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string SubTitle
        {
            get => subTitle;
            set => SetProperty(ref subTitle, value);
        }

        public bool IsEmpty
        {
            get => isEmpty;
            set => SetProperty(ref isEmpty, value);
        }

        public bool IsFirst
        {
            get => isFirst;
            set => SetProperty(ref isFirst, value);
        }

        public Usuario Sessao
        {
            get => sessao;
            set => SetProperty(ref sessao, value);
        }

        public Page Tela { get; set; }
        #endregion

        public BaseVM(Page page)
        {
            db = new DatabaseManager();
            Tela = page;

            //Controllers
            _pacienteController = new PacienteController();
            _serverErrorResponseManager = new ServerErrorResponseManager();

            Sessao = _pacienteController.GetSession().Result;
            if (!(Tela is LoginPage) && Sessao?.validadeToken != null && !(Sessao.validadeToken > DateTime.MinValue && Sessao.validadeToken > DateTime.UtcNow))
                Application.Current.MainPage = new NavigationPage(new LoginPage(true));
        }
    }
}
