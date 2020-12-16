using EMM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services
{
    public class PrintErorCatcher : HttpResponseCather
    {
        public PrintErorCatcher(HttpResponseCather httpPageCatcher) : base(httpPageCatcher)
        {
        }
        public override async Task ExecuteAsync(Func<Task> action)
        {
            await base.ExecuteAsync(action);
            page.PrintErorAsync("Пароль успешно изменен");
            page.GoBackAsync();
        }
        protected override async void HttpResponseReaction(HttpResponseException ex)
        {
            await ex.PrintEror(page);
        }

    }
}
