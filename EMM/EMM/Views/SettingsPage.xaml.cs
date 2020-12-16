using EMM.Helpers;
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
	public partial class SettingsPage : ContentPage, INavigational, ICommandPage
	{
        public SettingsVM SettingsVM { get; set; }
        public SettingsPage()
        {
            var user = new User("Помощник машиниста электровоза", 154.4, 6);
            Settings.PositionRates = new Dictionary<string, double>();
            InitializeComponent();
            BindingContext = user.CreateSettingsVM(this);
        }
        public SettingsPage(User user)
		{
			InitializeComponent();
            BindingContext = SettingsVM = user.CreateSettingsVM(this);
		}
        //public void Rebuild(SettingsVM settingsVM)
        //{
        //    settingsVM = this.SettingsVM;
        //}
        public async void GoBackAsync() => await Navigation.PopAsync();

        public async void PrintErorAsync(string message) => await DisplayAlert("", message, "Ok");

        public async Task PopAsync()
        {
            await Navigation.PopAsync();
        }

        public async Task PushAsync(Page page)
        {
            await Navigation.PushAsync(page);
        }
    }
}