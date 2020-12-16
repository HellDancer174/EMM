using EMM.Helpers;
using EMM.Models;
using EMM.Services;
using EMM.ViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AuthenticationPage : ContentPage, INavigational, ICommandPage
	{

        private LoginVM viewModel;
        //private ApiServices apiServices = new ApiServices();

        public AuthenticationPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new LoginVM(this);
		}
        public AuthenticationPage(LoginVM login)
        {
            InitializeComponent();
            BindingContext = viewModel = login;

        }
        public async Task PushAsync(Page page) => await Navigation.PushModalAsync(page);
        public async Task PopAsync() => await Navigation.PopModalAsync();

        //private async void Enter_Clicked(object sender, EventArgs e)
        //{
        //    await GetEror();
        //    ////var data = new Dictionary<string, string>();
        //    ////data.Add("name", viewModel.Login);
        //    ////data.Add("password", viewModel.Password);
        //    ////FormUrlEncodedContent form = new FormUrlEncodedContent(data);
        //    ////HttpResponseMessage message = await client.PostAsync(registerUrl, form);
        //    ////var result = await message.Content.ReadAsStringAsync();
        //    ////await DisplayAlert("Ответ", result, "Ok");
        //    ////if(!String.IsNullOrEmpty(Settings.AccessToken))

        //    //await ToRoutes();
        //}

        //public async Task GetEror()
        //{
        //    if (viewModel.IsSuccess == false)
        //    {
        //        await DisplayAlert("Ответ", viewModel.Message, "Ок");
        //    }
        //}

        //public async Task ToRoutes(Routes routes)
        //{
        //    //if (!CrossConnectivity.Current.IsConnected) PrintErorAsync("Отсутствует подключение к интернету");
        //    //else await Navigation.PushModalAsync(App.ToMasterDetailPage(routes));

        //}

        private void Reg_Clicked(object sender, EventArgs e)
        {
            //if (!CrossConnectivity.Current.IsConnected) PrintErorAsync("Отсутствует подключение к интернету");
            //else Navigation.PushModalAsync(new RegisterPage());
        }

        private void LocalEnter_Clicked(object sender, EventArgs e)
        {
            //var routes = new LocalRoutes();
            //Navigation.PushModalAsync(new NavigationPage(new ItemsPage(routes)));
        }

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("", message, "Ок");
        }

        public void GoBackAsync()
        {
            
        }
    }
}