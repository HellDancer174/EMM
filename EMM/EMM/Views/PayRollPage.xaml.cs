using EMM.Helpers;
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
	public partial class PayRollPage : ContentPage, ICommandPage
	{
        private readonly PayRollVM viewModel;

        public PayRollPage(User user, IRoutes routes)
		{
			InitializeComponent();
            this.viewModel = new PayRollVM(routes, this, user);
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
            if(viewModel.PremiumStrings == null || viewModel.PremiumStrings.Count == 0) viewModel.Load.Execute(null);
        }

    }
}