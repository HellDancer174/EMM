using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Services
{
    public class HttpPageCatcher : PageTryCatcher
    {
        public HttpPageCatcher(PageTryCatcher other) : base(other)
        {
        }
        public HttpPageCatcher(HttpPageCatcher httpPageCatcher) : base(httpPageCatcher)
        {
        }
        protected override void Reaction()
        {
            page.PrintErorAsync("Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети, ошибки DNS, проверки сертификата сервера или времени ожидания");
        }

    }
}
