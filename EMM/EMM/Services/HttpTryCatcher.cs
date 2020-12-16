using EMM.Helpers;
using EMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services
{
    public class HttpTryCatcher : HttpPageCatcher
    {
        public HttpTryCatcher(PageTryCatcher catcher) : base(catcher)
        {
        }
        public async Task<bool> TryExecuteRouteRequestAsync(Func<Task> action, BaseLoginViewModel loginVM)
        {
            try
            {
                await action.Invoke();
            }
            catch (HttpResponseException ex)
            {
                if (ex.StatusCodesEquals(HttpStatusCode.Unauthorized))
                {

                    loginVM.Login = String.Empty;
                    loginVM.Password = String.Empty;
                    Settings.Username = String.Empty;
                    Settings.Password = String.Empty;
                    Settings.AccessToken = null;
                    Settings.AccessTokenExpires = Settings.DefaultDateTime;
                    page.PrintErorAsync("Выполнен вход с другого устройства, либо срок действия токена доступа истек. Введите данные заново.");
                }
                else page.PrintErorAsync("Ошибка на сервере");
                return false;
            }
            catch
            {
                Reaction();
                return false;
            }
            return true;
        }
    }
}
