using Smartd.Bibliotecas.Helps;
using Smartd.Models.Responses.Agenda;
using Smartd.ViewModels.Shared;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioPage : ContentPage
    {
        private readonly CalendarioVM viewModel;
        private List<DateTime> SelectedDates;
        public CalendarioPage(ObservableCollection<PacienteResponse.Item> items)
        {
            InitializeComponent();
            BindingContext = viewModel = new CalendarioVM(this, items);
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if (isFirst)
        //    {
        //        await viewModel.CarregarDados();
        //        //calendar.SelectedDates = viewModel.DatasSelecionadas.ToList();
        //        isFirst = false;
        //    }
        //}

        //private void Selecionar(object sender, EventArgs e)
        //{
        //    viewModel.cmdSelecionar.Execute(true);
        //}
        private void Disponivel(object sender, EventArgs e)
        {
            viewModel.cmdDisponivel.Execute(true);
        }

        private void calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        {
            List<DateTime> blackoutDates = new List<DateTime>();
            if (!viewModel.DatasSelecionadas.Contains(e.Date))
            {
                blackoutDates.Add(e.Date);
                calendar.BlackoutDates = blackoutDates;
            }
        }

        private async void calendar_OnCalendarTapped(object sender, CalendarTappedEventArgs e)
        {
            viewModel.IsBusy = true;
            await AsyncHelper.Wait(100);

            calendar.SelectedDates = SelectedDates;
            calendar.Refresh();

            await viewModel.Selecionar(e.DateTime.Date);

            await AsyncHelper.Wait(200);

            viewModel.IsBusy = false;
        }

        private void calendar_MonthChanged(object sender, MonthChangedEventArgs e)
        {
            SelectedDates = new List<DateTime>();
            foreach (var item in e.VisibleDates)
            {
                if (viewModel.DatasSelecionadas.Contains(item.Date))
                    SelectedDates.Add(item.Date);
            }
            calendar.SelectedDates = SelectedDates;
        }

        protected override bool OnBackButtonPressed()
        {
            if (viewModel.HasChanged)
                MessagingCenter.Send(this, "CarregarHome");

            return base.OnBackButtonPressed();
        }
    }
}