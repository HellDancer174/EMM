using EMM.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Services
{
    public class PageBooleanTryCatcher : BooleanTryCatcher
    {
        private ICommandPage page;

        public PageBooleanTryCatcher(ICommandPage page)
        {
            this.page = page;
        }

        protected override bool ReFunction()
        {
            var result = base.ReFunction();
            page.PrintErorAsync("Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети, ошибки DNS, проверки сертификата сервера или времени ожидания");
            return result;
        }
    }
}
