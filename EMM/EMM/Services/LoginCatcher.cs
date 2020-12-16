using EMM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services
{
    public class LoginCatcher : HttpTryCatcher
    {
        public LoginCatcher(HttpPageCatcher httpPageCatcher) : base(httpPageCatcher)
        {
        }
        public override async Task ExecuteAsync(Func<Task> action)
        {
            try
            {
                await action.Invoke();
            }
            catch (HttpResponseException ex)
            {
                await ex.PrintEror(page);
            }
        }

    }
}
