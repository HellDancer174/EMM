using System;
using System.Collections.Generic;
using System.Text;
using EMM.Models;
using EMM.Views;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public class LocalEditRouteVM : RouteVMEditable
    {
        private INavigational page;

        //new private event Action RebuildYou;

        //public LocalEditRouteVM(IRouteModel model, Action rebuild) : base(model)
        //{
        //    RebuildYou += rebuild;
        //}
        public LocalEditRouteVM(IRouteModel model): base(model)
        {

        }
        public LocalEditRouteVM(IRouteModel model, INavigation previous) : base(model)
        {
            var newPage = new LocalRouteEditPage(this);
            Push(previous, newPage);
            page = newPage;

        }
        protected override DateTime RebuildLocalLastStation(DateTime arraval, DateTime depature, StationVM last, DateTime currentArraval)
        {
            if (last.ArravalTime != default(TimeSpan)) return currentArraval = last.RebuildModel(arraval, depature, currentArraval);
            else return base.RebuildLocalLastStation(arraval, depature, last, currentArraval);
        }

        public Command SaveCommand
        {
            get
            {
                return commander.Create(() =>
                {
                    Checker = true;
                    Save();
                    MessagingCenter.Send(this, "RefreshRouteLocal", (Route)model);
                    //if (RebuildYou != null) RebuildYou.Invoke();
                    page.PopAsync();
                });
            }
        }

        //public Command DeleteCommand
        //{
        //    get
        //    {
        //        return commander.Create(()=> 
        //        {
        //            MessagingCenter.Send(this, "DeleteRouteLocal", (Route)model);
        //        });
        //    }
        //}
        protected override void Delete()
        {
            MessagingCenter.Send(this, "DeleteRouteLocal", (Route)model);
            page.PopAsync();
        }

        public LoginPopupVM ToLoginPopup(ICommandPage page)
        {
            return new LoginPopupVM((Route)model, page);
        }

    }
}
