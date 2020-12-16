using EMM.Services;
using EMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FuelEnergyPage : ContentPage, ICommandPage
	{
        private FuelEnergies viewModel;
		public FuelEnergyPage(IRoutes routes)
		{
			InitializeComponent();
            this.viewModel = new FuelEnergies(routes, this);
            BindingContext = viewModel;
		}

        public async void GoBackAsync()
        {
            await Navigation.PopAsync();
        }

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("Ошибка", message, "Ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.FuelEnergyStrings == null || viewModel.FuelEnergyStrings.Count == 0) viewModel.Load.Execute(null);
        }
    }
}