using EMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using EMM.Helpers;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage, ICommandPage
	{
        private RegisterVM viewModel;
		public RegisterPage()
		{
			InitializeComponent ();
            BindingContext = viewModel = new RegisterVM(this, Settings.PositionRates);
        }

        //private async void Register_Clicked(object sender, EventArgs e)
        //{
        //    if (viewModel.IsSuccess)
        //    {
        //        GoBackAsync();

        //    }
        //    else
        //    {
        //        PrintErorAsync("");
        //    }

        //}

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("", message, "Ok");
        }

        public async void GoBackAsync()
        {
            await DisplayAlert("", "Вы успешно зарегистрированы", "Ok");
            await Navigation.PopModalAsync();
        }
    }
}