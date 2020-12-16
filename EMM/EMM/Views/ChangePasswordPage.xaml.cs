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
	public partial class ChangePasswordPage : ContentPage, ICommandPage
	{
        private ChangePasswordVM viewModel;
		public ChangePasswordPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ChangePasswordVM(this);
		}

        public async void GoBackAsync()
        {
            await Navigation.PopAsync();
        }

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("", message, "Ok");
        }

        private void OldPass_Focused(object sender, FocusEventArgs e)
        {
            if (OldEntryText.IsVisible == true) return;
            OldEntryPassword.IsVisible = false;
            OldEntryText.IsVisible = true;
            OldEntryText.Focus();
        }

        private void OldText_Unfocused(object sender, FocusEventArgs e)
        {
            OldEntryPassword.IsVisible = true;
            OldEntryText.IsVisible = false;
        }

        private void NewPass_Focused(object sender, FocusEventArgs e)
        {
            if (NewEntryText.IsVisible == true) return;
            NewEntryPassword.IsVisible = false;
            NewEntryText.IsVisible = true;
            NewEntryText.Focus();
        }
        private void NewText_UnFocused(object sender, FocusEventArgs e)
        {
            NewEntryPassword.IsVisible = true;
            NewEntryText.IsVisible = false;
        }
        private void ConfirmPass_Focused(object sender, FocusEventArgs e)
        {
            if (ConfirmEntryText.IsVisible == true) return;
            ConfirmEntryPassword.IsVisible = false;
            ConfirmEntryText.IsVisible = true;
            ConfirmEntryText.Focus();
        }
        private void ConfirmText_UnFocused(object sender, FocusEventArgs e)
        {
            ConfirmEntryPassword.IsVisible = true;
            ConfirmEntryText.IsVisible = false;
        }


    }
}