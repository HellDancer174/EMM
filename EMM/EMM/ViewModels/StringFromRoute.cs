using EMM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using EMM.Views;

namespace EMM.ViewModels
{
    public class StringFromRoute : IStringFromRoute
    {
        private IRouteModel model;

        public DateTime Date { get; set; }
        public string Direction { get; set; }

        public StringFromRoute(IRouteModel model)
        {
            this.model = model;
            Rebuild();
        }

        public void Rebuild()
        {
            model.Publish((worksStart, worksFinish, locomotives, passangers, trains, stations, comment, single) =>
            {
                Date = worksStart.Date;
                if (stations.Count() == 0) Direction = "Отсутствует направление";
                else Direction = stations.FirstOrDefault().ToString() + " - " + stations.LastOrDefault().ToString();
            });
        }
        public RouteDetailVM ToRouteDetailVM()
        {
            var routeDetail =  new RouteDetailVM(model);
            routeDetail.RebuildYou += Rebuild;
            return routeDetail;
        }
        //public LocalEditRouteVM ToLocalEditVM()
        //{
        //    var localedit = new LocalEditRouteVM(model, Rebuild);
        //    return localedit;
        //}
        public LocalEditRouteVM ToLocalEditVM(INavigation previous)
        {
            var localedit = new LocalEditRouteVM(model, previous);
            return localedit;
        }

    }
}
