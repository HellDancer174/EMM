using EMM.Helpers;
using EMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services
{
    public class RegisterPageCatcher : HttpResponseCather
    {
        private readonly RegisterVM registerVM;

        public RegisterPageCatcher(HttpResponseCather other, RegisterVM registerVM) : base(other)
        {
            this.registerVM = registerVM;
        }

        public override async Task ExecuteAsync(Func<Task> action)
        {
            try
            {
                await action.Invoke();
            }
            catch (HttpResponseException ex)
            {
                HttpResponseReaction(ex);
                return;
            }
            catch
            {
                Reaction();
                return;
            }
            Settings.Username = registerVM.Email;
            Settings.Password = registerVM.Password;
            page.GoBackAsync();

        }
        protected override async void HttpResponseReaction(HttpResponseException ex)
        {
            await ex.PrintEror(page);
        }

    }
}
