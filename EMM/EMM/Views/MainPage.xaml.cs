using EMM.Helpers;
using EMM.Models;
using EMM.Services;
using EMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage, ICommandPage
    {
        private Dictionary<string, NavigationPage> MenuPages = new Dictionary<string, NavigationPage>();
        private Routes pack;
        private User user;
        private SettingsVM settings;
        private string currentPage = string.Empty;

        public MainPage(Routes pack, User user)
        {
            InitializeComponent();
            this.pack = pack;
            this.user = user;
            settings = new SettingsVM(new NullPage(), user, "", "", 0d);
            Master = new MenuPage(this);
            Detail = new NavigationPage(new ItemsPage(pack));
            MasterBehavior = MasterBehavior.Popover;
            IsPresentedChanged += PresentChanged;
            MenuPages.Add("Маршруты", (NavigationPage)Detail);
        }

        public async void GoBackAsync()
        {
            await settings.Save();
            App.Current.MainPage = new NavigationPage(new AuthenticationPage());
        }
        private async void PresentChanged(object sender, EventArgs e)
        {
            if (IsPresented && currentPage == "Настройки") await settings.Save();
        }
        protected override bool OnBackButtonPressed()
        {
            settings.Save();
            Settings.AccessToken = string.Empty;
            return base.OnBackButtonPressed();
        }
        public async Task NavigateFromMenu(HomeMenuItem menuItem)
        {
            if (!MenuPages.ContainsKey(menuItem.ToString()))
            {
                switch (menuItem.ToString())
                {
                    case "Маршруты":
                        MenuPages.Add("Маршруты", new NavigationPage(new ItemsPage(pack)));
                        break;
                    //case "О нас":
                    //    MenuPages.Add("О нас", new NavigationPage(new AboutPage()));
                    //    break;
                    case "Рабочее время":
                        MenuPages.Add("Рабочее время", new NavigationPage(new WorkTimesPage(pack)));
                        break;
                    case "Расход ТЭР":
                        MenuPages.Add("Расход ТЭР", new NavigationPage(new FuelEnergyPage(pack)));
                        break;
                    case "Техническая скорость":
                        MenuPages.Add("Техническая скорость", new NavigationPage(new TechnicalSpeedPage(pack)));
                        break;
                    case "Расчетные листы":
                        MenuPages.Add("Расчетные листы", new NavigationPage(new PayRollPage(user, pack)));
                        break;
                    case "Настройки":
                        var page = new SettingsPage(user);
                        settings = page.SettingsVM;
                        MenuPages.Add("Настройки", new NavigationPage(page));
                        break;


                    case "Выход":
                        GoBackAsync();
                        Settings.AccessToken = "";
                        Settings.AccessTokenExpires = Settings.DefaultDateTime;
                        return;
                }
            }

            var newPage = MenuPages[menuItem.ToString()];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;
                currentPage = menuItem.ToString();
                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        public async void PrintErorAsync(string message)
        {
            await Detail.DisplayAlert("", message, "Ok");
        }
    }
}