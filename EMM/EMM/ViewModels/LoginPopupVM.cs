using EMM.Models;
using EMM.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EMM.Helpers;
using Xamarin.Forms;
using Plugin.Connectivity;
using EMM.Views;

namespace EMM.ViewModels
{
    public class LoginPopupVM: BaseLoginViewModel
    {
        private CommandsCreater commander = new CommandsCreater();
        //private ApiServices apiServices = new ApiServices();
        //private HttpTryCatcher catcher;
        //public string Login { get; set; }
        //public string Password { get; set; }
        private Route route;
        //private ICommandPage page;
        public LoginPopupVM(Route route, ICommandPage page)
        {
            this.route = route;
            this.page = page;
            Login = Settings.Username;
            Password = Settings.Password;
            catcher = new LoginCatcher(new HttpPageCatcher(new PageTryCatcher(page)));
        }
        public bool Checker { get; set; }
        public Command ToDataBase
        {
            get
            {
                return commander.Create(async() => await ToDataBaseAsync());
            }
        }
        private async Task ToDataBaseAsync()
        {
            if (IsBusy || Check() == false) return;
            OnIndicator();
            //Settings.Username = Login;
            //Settings.Password = Password;
            if (await LoginAsync() == false) return;
            bool created = false;
            await catcher.TryExecuteRouteRequestAsync(async() => created = await apiServices.CreateRouteAsync(Settings.AccessToken, route), this);
            if (created)
            {
                 MessagingCenter.Send(this, "RefreshView", route);
            }
                //else
                //{
                //    page.PrintErorAsync("Ошибка сервера");
                //}

            OffIndicator();
            if(created) page.GoBackAsync();
        }

    }
}
