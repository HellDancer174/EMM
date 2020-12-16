using EMM.Services;
using EMM.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EMM.Helpers;

namespace EMM.ViewModels
{
    public class BaseLoginViewModel: IndicatorViewModel
    {
        protected ApiServices apiServices = new ApiServices();
        protected LoginCatcher catcher;
        protected ICommandPage page;
        public BaseLoginViewModel()
        {
            page = new NullPage();
            catcher = new LoginCatcher(new HttpPageCatcher(new PageTryCatcher(page)));

        }
        public BaseLoginViewModel(ICommandPage page)
        {

            catcher = new LoginCatcher(new HttpPageCatcher(new PageTryCatcher(page)));
            this.page = page;
        }
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                if (value == login) return;
                login = value;
                OnPropertyChanged();
            }
        }


        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value == password) return;
                password = value;
                OnPropertyChanged();
            }
        }
        protected bool Check()//ДОПИЛИТЬ!!!!
        {
            string message = String.Empty;
            if (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password))
            {
                page.PrintErorAsync("Проверьте правильность введенных данных");
                return false;

            }
            if (!CrossConnectivity.Current.IsConnected)
            {
                page.PrintErorAsync("Отсутствует подключение к интернету");
                return false;
            }
            return true;

        }
        protected async Task<bool> LoginAsync()
        {
            if (String.IsNullOrEmpty(Settings.AccessToken) || Settings.Username != Login || Settings.Password != Password || Settings.AccessTokenExpires.ToUniversalTime() < DateTime.UtcNow.AddHours(1)) //Debug!!!
            {
                await catcher.ExecuteAsync(async () => await apiServices.LoginAsync(Login, Password));
                if (String.IsNullOrEmpty(Settings.AccessToken))
                {
                    //page.PrintErorAsync("Неверный логин или пароль");
                    OffIndicator();
                    return false;
                }
                else
                {
                    Settings.Username = Login;
                    Settings.Password = Password;
                    return true;
                }

            }
            else return true;
        }


    }
}
