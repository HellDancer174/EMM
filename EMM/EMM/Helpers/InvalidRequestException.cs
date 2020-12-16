using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EMM.Helpers
{
    public class InvalidRequestException: HttpRequestException
    {
        public new string Message { get; private set; }
        public InvalidRequestException()
        {
            Message = "Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети, ошибки DNS, проверки сертификата сервера или времени ожидания";
        }
    }
}
