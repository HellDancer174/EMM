using EMM.Services;
using System;
using System.Collections.Generic;
using System.Text;
using EMM.Helpers;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using Plugin.Connectivity;
using EMM.Views;
using System.Windows.Input;
using EMM.Models;
using System.Net.Http;
using System.Net;

namespace EMM.ViewModels
{
    public class LoginVM: BaseLoginViewModel
    {
        //private CommandsCreater commander = new CommandsCreater();
        //private string login;
        //public string Login
        //{
        //    get { return login; }
        //    set
        //    {
        //        if (value == login) return;
        //        login = value;
        //        OnPropertyChanged();
        //    }
        //}


        //private string password;
        //public string Password
        //{
        //    get { return password; }
        //    set
        //    {
        //        if (value == password) return;
        //        password = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool isEnable = false;
        //public bool IsEnable
        //{
        //    get { return isEnable; }
        //    set
        //    {
        //        if (isEnable == value) return;
        //        isEnable = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool isVisible = false;
        //public bool IsVisible
        //{
        //    get { return isVisible; }
        //    set
        //    {
        //        if (isVisible == value) return;
        //        isVisible = value;
        //        OnPropertyChanged();
        //    }
        //}


        private new AuthenticationPage page;

        public LoginVM(AuthenticationPage page): base(page)
        {
            this.Login = Settings.Username;
            this.Password = Settings.Password;
            this.page = page;
            //IsBusy = false;
            //IsEnable = true;
            //catcher = new LoginCatcher(new HttpPageCatcher(new PageTryCatcher(page)));
        }
        public LoginVM()
        {
            page = new AuthenticationPage(this);
            base.page = page;
            catcher = new LoginCatcher(new HttpPageCatcher(new PageTryCatcher(page)));
            App.Current.MainPage = page;
            this.Login = Settings.Username;
            this.Password = Settings.Password;
        }

        public string AccessToken { get; set; }
        public Command LoginCommand //Debug!!!//Debug!!!//Debug!!!//Debug!!!
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsBusy||Check() == false) return;
                    OnIndicator();
                    //var isLogin = await LoginAsync();
                    if (await LoginAsync() == false) return;
                    //if (String.IsNullOrEmpty(Settings.AccessToken) || Settings.Username != Login || Settings.Password != Password || Settings.AccessTokenExpires < DateTime.UtcNow.AddHours(1)) //Debug!!!
                    //{
                    //    await catcher.ExecuteAsync(async () => await apiServices.LoginAsync(Login, Password));
                    //    if (String.IsNullOrEmpty(Settings.AccessToken))
                    //    {
                    //        page.PrintErorAsync("Неверный логин или пароль");
                    //        OffIndicator();
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        Settings.Username = Login;
                    //        Settings.Password = Password;
                    //    }

                    //}
                    IEnumerable<Route> list = new List<Route>();
                    var success = await catcher.TryExecuteRouteRequestAsync(async() => list = await apiServices.GetRoutesAsync(Settings.AccessToken), this);
                    //try
                    //{
                    //    list = await apiServices.GetRoutesAsync(Settings.AccessToken);
                    //}
                    //catch(HttpResponseException ex)
                    //{
                    //    if (ex.StatusCodesEquals(HttpStatusCode.Unauthorized))
                    //    {

                    //        Login = String.Empty;
                    //        Password = String.Empty;
                    //        Settings.Username = String.Empty;
                    //        Settings.Password = String.Empty;
                    //        Settings.AccessToken = null;
                    //        Settings.AccessTokenExpires = Settings.DefaultDateTime;
                    //        page.PrintErorAsync("Выполнен вход с другого устройства, либо срок действия токена доступа истек. Введите данные заново.");
                    //    }
                    //    else page.PrintErorAsync("Ошибка на сервере");
                    //}
                    //catch(InvalidRequestException ex)
                    //{
                    //    page.PrintErorAsync(ex.Message);
                    //}
                    //finally
                    //{
                    OffIndicator();
                    //}
                    if (String.IsNullOrEmpty(Settings.AccessToken)||success == false) return;//if (success == false) return;
                    var routes = new Routes(list);
                    var user = await apiServices.GetUserAsync(Settings.AccessToken);
                    await page.PushAsync(new MainPage(routes, user));
                });
            }
        }

        public Command RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsBusy == true) return;
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        page.PrintErorAsync("Нет подключения к интернету");
                        return;
                    }
                    if (Settings.PositionRates == null || Settings.PositionRates.Count == 0)
                    {
                        await catcher.ExecuteAsync(async() => Settings.PositionRates = await apiServices.GetRateAsync());
                        return;
                    }
                    IsBusy = true;
                    await page.PushAsync(new RegisterPage());
                    IsBusy = false;
                });
            }
        }
        public Command LocalCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsBusy == true) return;
                    IsBusy = true;
                    var routes = new LocalRoutes();
                    await page.PushAsync(new NavigationPage(new ItemsPage(routes)));
                    IsBusy = false;
                });
            }
        }


        //public bool IsSuccess;
        //public string Message { get; set; }
        //private bool Check()//ДОПИЛИТЬ!!!!
        //{
        //    string message = String.Empty;
        //    if (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password))
        //    {
        //        page.PrintErorAsync("Проверьте правильность введенных данных");
        //        return false;

        //    }
        //    if (!CrossConnectivity.Current.IsConnected)
        //    {
        //        page.PrintErorAsync("Отсутствует подключение к интернету");
        //        return false;
        //    }
        //    return true;

        //}


    }
}
