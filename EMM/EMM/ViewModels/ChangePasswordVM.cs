using EMM.Services;
using EMM.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public class ChangePasswordVM: BaseViewModel
    {
        private string oldpass;
        public string OldPassword
        {
            get { return oldpass; }
            set
            {
                if (value == oldpass) return;
                oldpass = value;
                OnPropertyChanged();
            }
        }
        private string pass;
        public string Password
        {
            get { return pass; }
            set
            {
                if (value == pass) return;
                pass = value;
                OnPropertyChanged();
            }
        }
        private string confirmPass;
        public string ConfirmPassword
        {
            get { return confirmPass; }
            set
            {
                if (value == confirmPass) return;
                confirmPass = value;
                OnPropertyChanged();
            }
        }




        private ApiServices services = new ApiServices();
        private readonly ICommandPage page;
        private PrintErorCatcher catcher;

        public ChangePasswordVM(ICommandPage page)
        {
            this.page = page;
            catcher = new PrintErorCatcher(new HttpResponseCather(new HttpPageCatcher(new PageTryCatcher(page))));
        }


        public Command ChangePasswordCommand
        {
            get
            {
                return new Command(ChangePassword);
            }
        }
        private async void ChangePassword()
        {
            await catcher.ExecuteAsync(async() => await services.ChangePasswordAsync(OldPassword, Password, ConfirmPassword));
        }


    }
}
