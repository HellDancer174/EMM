using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMM.Views;
using EMM.Services;
using EMM.ViewModels;
using EMM.Helpers;
using System.Collections.Generic;
using Plugin.Connectivity;
using System.Threading.Tasks;
using System.Linq;
using EMM.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EMM
{
    public partial class App : Application
    {
        private ApiServices services = new ApiServices();

        public App()
        {
            InitializeComponent();
            var login = new LoginVM();
            Settings.PositionRates = new Dictionary<string, double>();
            //MainPage = new ItemDetailPage();
            //MainPage = new ChangePasswordPage();
            //Download();
            //MainPage = new NavigationPage(new SettingsPage(new User("Помощник машиниста электровоза (пассажирское движение)", 154.4, 6)));
            //MainPage = new MainPage();
        }

        private async void Download()
        {
            var stack = App.Current.MainPage.Navigation.NavigationStack;
            var modalStack = Current.MainPage.Navigation.ModalStack;
            ICommandPage currentPage = new NullPage();
            if (stack.Count != 0) currentPage = stack.Last() as ICommandPage;
            else if (modalStack.Count != 0) currentPage = modalStack.Last() as ICommandPage;
            else currentPage = App.Current.MainPage as ICommandPage;
            if (currentPage == null) currentPage = new NullPage();
            PageTryCatcher catcher = new HttpTryCatcher(new PageTryCatcher(currentPage));
            if (CrossConnectivity.Current.IsConnected && (Settings.PositionRates == null || Settings.PositionRates.Count == 0))
            {
                Settings.PositionRates = new Dictionary<string, double>();
                await catcher.ExecuteAsync(async () => Settings.PositionRates = await services.GetRateAsync());
            }
            else if (!CrossConnectivity.Current.IsConnected) currentPage.PrintErorAsync("Нет подключения к интернету");
        }
        protected override void OnStart()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) => Download();
            Download();
            if (string.IsNullOrEmpty(Settings.Position)) Settings.Position = "Помощник машиниста электровоза (пассажирское движение)";
            if (Settings.Rate <= 0) Settings.Rate = 144.2;
            if (Settings.QualificationСlass <= 0 || Settings.QualificationСlass > 6) Settings.QualificationСlass = 6;

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
