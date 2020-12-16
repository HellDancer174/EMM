using EMM.Helpers;
using EMM.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.Services
{
    public class TryHttpClient : BaseClientDecorator
    {
        private readonly static HttpClient client = new HttpClient();
        private readonly ICommandPage page;

        public TryHttpClient(ICommandPage page)
        {
            this.page = page;
        }
        public TryHttpClient()
        {
            page = new NullPage();
        }

        public override async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.DeleteAsync(url);
            }
            catch
            {
                //if (page != null) page.PrintErorAsync("Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети, ошибки DNS, проверки сертификата сервера или времени ожидания");
                throw new InvalidRequestException();
            }
            return response;
        }

        public override async Task<HttpResponseMessage> GetAsync(string url)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(url);
            }
            catch
            {
                throw new InvalidRequestException();

            }
            return response;

        }

        public override async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync(url, content);
            }
            catch
            {
                throw new InvalidRequestException();

            }
            return response;

        }

        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request);
            }
            catch
            {
                throw new InvalidRequestException();

            }
            return response;

        }
    }
}
