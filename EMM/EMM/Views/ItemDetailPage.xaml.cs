using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMM.Helpers;
using EMM.Models;
using EMM.ViewModels;
using System.Collections.Generic;

namespace EMM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        private RouteDetailVM viewModel;

        public ItemDetailPage(RouteDetailVM route)
        {
            InitializeComponent();

            BindingContext = this.viewModel = route;
            //viewModel.RefreshView();
        }
        public ItemDetailPage()
        {
            InitializeComponent();
            Meters[] mockMeters = { new Meters(3, 235, 245, 0, 0, 154, 158) };
            var mockLoc = new Locomotive("ЭП2К", "207", new BackgroundTime(2, new DateTime(2021, 01, 1, 1, 20, 0), new DateTime(2021, 01, 1, 1, 49, 0), Settings.DefaultDateTime, new DateTime(2021, 01, 1, 8, 10, 0)), mockMeters, 1);
            var mockTrain = new Train(2, 4301, "Челябинск П", "Карталы", mockLoc, null, 0, 0, 0);
            var firstStation = new Station(8, "Челябинск П", Settings.DefaultDateTime, new DateTime(2021, 01, 1, 2, 1, 0), default(TimeSpan));
            var station = new Station(9, "Троицк", new DateTime(2021, 01, 1, 4, 10, 0), new DateTime(2021, 01, 1, 5, 15, 0), default(TimeSpan));
            var lastStation = new Station(9, "Карталы", new DateTime(2021, 01, 1, 7, 30, 0), Settings.DefaultDateTime, default(TimeSpan));
            var mockLocs = new List<Locomotive>();
            var mockTrains = new List<Train>();
            var mockStations = new List<Station>();
            var mockPass = new List<Passanger>();
            mockLocs.Add(mockLoc);
            mockTrains.Add(mockTrain);
            mockStations.Add(firstStation);
            mockStations.Add(station);
            mockStations.Add(lastStation);
            mockPass.Add(new Passanger(1, 366, new DateTime(2021, 01, 1, 8, 20, 0), new DateTime(2021, 01, 1, 11, 20, 0)));
            var route = new Route(1, new DateTime(2020, 12, 31, 23, 55, 0), new DateTime(2021, 01, 1, 8, 20, 0), mockLocs, mockTrains, mockStations, mockPass, "", false);
            BindingContext = viewModel = new RouteDetailVM(route);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RouteEditPage(viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}