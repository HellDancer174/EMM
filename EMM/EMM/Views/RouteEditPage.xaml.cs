using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EMM.Models;
using EMM.ViewModels;
using System.Threading.Tasks;

namespace EMM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteEditPage : ContentPage, INavigational, ICommandPage
    {

        public RouteEditPage()
        {
            InitializeComponent();
            var vm = new NewRouteVM(new Route(), this);
            viewModel = vm;
            this.BindingContext = vm;
        }

        //public RouteEditPage(LocalNewRouteVM localnew)
        //{
        //    InitializeComponent();
        //    viewModel = localnew;
        //    this.BindingContext = localnew;
        //}

        public RouteEditPage(RouteDetailVM vm)
        {
            InitializeComponent();
            var editVM = vm.GoToEditRoute(this);
            this.BindingContext = editVM;
            viewModel = editVM;
            
        }
        public RouteEditPage(LocalNewRouteVM localNew)
        {
            this.BindingContext = localNew;
            InitializeComponent();
            viewModel = localNew;

        }

        public RouteEditPage(bool local)
        {
            if (!local) return;
            InitializeComponent();
            var vm = new LocalNewRouteVM(new Route(), this);
            viewModel = vm;
            this.BindingContext = vm;
        }

        private RouteVMEditable viewModel;

        //private async void Save_Clicked(object sender, EventArgs e)
        //{
        //    //if (viewModel.Checker) await Navigation.PopAsync();
        //}

        //async void Cancel_Clicked(object sender, EventArgs e)
        //{
        //    GoBackAsync();
        //}

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            return;
        }

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("Ошибка", message, "ok");
        }
        public async void GoBackAsync()
        {
            await Navigation.PopAsync();
        }

        public async Task PushAsync(Page page) => await Navigation.PushAsync(page);
        public async Task PopAsync() => await Navigation.PopAsync();
    }
}