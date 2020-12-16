using EMM.Models;
using EMM.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
	public partial class LoginPopup : PopupPage, ICommandPage
	{
        private Action OnLocalRoutePage;

        public LoginPopup (LocalEditRouteVM viewmodel, Action OnLocalRoutePage)
		{
			InitializeComponent();
            this.OnLocalRoutePage = OnLocalRoutePage;
            BindingContext = viewmodel.ToLoginPopup(this);
		}

        public void GoBackAsync()
        {
            PopupNavigation.Instance.PopAsync();
            if (OnLocalRoutePage != null) OnLocalRoutePage.Invoke();
        }

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("Ошибка", message, "Ok");
        }

        private void Login_Clicked(object sender, EventArgs e)
        {

        }
    }
}