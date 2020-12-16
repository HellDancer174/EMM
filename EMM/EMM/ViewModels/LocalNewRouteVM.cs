using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EMM.Models;
using EMM.Views;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public class LocalNewRouteVM : RouteVMEditable
    {
        private readonly INavigational page;


        public LocalNewRouteVM(IRouteModel model, INavigational page) : base(model)
        {
            this.page = page;
        }
        public LocalNewRouteVM(IRouteModel model, INavigation previous) : base(model)
        {
            var newPage = new RouteEditPage(this);
            Push(previous, newPage);
            page = newPage;
        }

        protected override DateTime RebuildLocalLastStation(DateTime arraval, DateTime depature, StationVM last, DateTime currentArraval) => last.RebuildModel(arraval, depature, currentArraval);

        public Command SaveCommand
        {
            get
            {
                //return commander.CreateForLocalRoute(() => MessagingCenter.Send(this, "AddRouteLocal", (Route)model), true, page);
                return commander.Create(() =>
                {
                    Checker = true;
                    Save();
                    MessagingCenter.Send(this, "AddRouteLocal", (Route)model);
                    page.PopAsync();

                });
            }
        }
    }
}
