using EMM.Helpers;
using EMM.Models;
using EMM.Services;
using EMM.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Trip.Routes
{
    public class InsertableRoute : RouteDecorator
    {
        private readonly ApiServices services;
        private readonly ICommandPage media;

        public InsertableRoute(Route route, Aut, ICommandPage media) : base(route)
        {
            this.services = services;
            this.media = media;
        }

        public override async Task Transfer()
        {
            bool result = false;
            try
            {
                result = await services.CreateRouteAsync(Settings.AccessToken, this);
            }
            catch (HttpResponseException ex)
            {
                await ex.PrintEror(media);
            }
        }
    }
}
