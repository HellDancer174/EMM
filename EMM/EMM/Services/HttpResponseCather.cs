using EMM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services
{
    public class HttpResponseCather : HttpPageCatcher
    {
        public HttpResponseCather(HttpPageCatcher httpPageCatcher) : base(httpPageCatcher)
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
                HttpResponseReaction(ex);
                return;
            }
            catch
            {
                Reaction();
                return;
            }

        }
        protected virtual void HttpResponseReaction(HttpResponseException ex)
        {
            Reaction();
        }


    }
}
