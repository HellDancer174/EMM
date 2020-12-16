using EMM.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMM.Models;

namespace EMM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocalRouteEditPage : ContentPage, INavigational, ICommandPage
	{
        public LocalRouteEditPage(LocalEditRouteVM viewmodel)
        {
            InitializeComponent();
            viewModel = viewmodel;
            BindingContext = viewmodel;
        }
        public LocalRouteEditPage()
        {
            InitializeComponent();
            BindingContext = new LocalEditRouteVM(new Route());

        }


        private LocalEditRouteVM viewModel;

        private async void Save_Clicked(object sender, EventArgs e)
        {
            //GoBackAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //GoBackAsync();
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            return;
        }

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("", message, "ok");
        }
        public async Task PushLoginPopup(LoginPopup page)
        {
            await PopupNavigation.Instance.PushAsync(page);
        }
        public async void GoBackAsync()
        {
            await Navigation.PopAsync();
        }
        public async Task PushAsync(Page page) => await Navigation.PushAsync(page);
        public async Task PopAsync() => await Navigation.PopAsync();

        private void ToDataBase_Clicked(object sender, EventArgs e)
        {
            var item = sender as ToolbarItem;
            item.IsEnabled = false;
            if(!viewModel.Save())
            {
                PrintErorAsync(viewModel.Message);
                return;
            }
            PushLoginPopup(new LoginPopup(viewModel, GoBackAsync));
            item.IsEnabled = true;
        }
    }
}