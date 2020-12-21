using EMM.Helpers;
using EMM.Models;
using EMM.Services;
using EMM.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Trip.Routes
{
    public class InsertableRoute : RouteDecorator
    {
        private readonly ApiServices services;
        private readonly ICommandPage media;

        public InsertableRoute(Route route, ApiServices services, ICommandPage media) : base(route)
        {
            this.services = services;
            this.media = media;
        }

        public override async void Transfer()
        {
            bool result = false;
            try
            {
                result = await services.CreateRouteAsync(Settings.AccessToken, this);
            }
            catch (HttpResponseException ex)
            {
                var isServerEror = ex.StatusCodesEquals(System.Net.HttpStatusCode.InternalServerError);
                if (isServerEror) media.PrintErorAsync("При добавлении маршрута произошла внутренняя ошибка сервера");
                else media.PrintErorAsync("Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети, ошибки DNS, проверки сертификата сервера или времени ожидания");
            }
        }
    }
}
