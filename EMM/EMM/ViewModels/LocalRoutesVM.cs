using EMM.Helpers;
using EMM.Models;
using EMM.Services;
using EMM.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public class LocalRoutesVM : RoutesVM
    {
        public LocalRoutesVM(IRoutes pack, ItemsPage page) : base(pack, page)
        {
        }
        protected override void Subscribe()
        {
            MessagingCenter.Subscribe<LocalNewRouteVM, Route>(this, "AddRouteLocal", async (obj, route) =>
            {
                await Add(route);// It was Async

            });
            MessagingCenter.Subscribe<LocalEditRouteVM, Route>(this, "RefreshRouteLocal", async (obj, route) =>
            {
                await Refresh(route); // It was Async
            });

            MessagingCenter.Subscribe<LoginPopupVM, Route>(this, "RefreshView", (obj, route) =>
            {
                routes.DeleteItemAsync(route);
                ExecuteLoadItemsCommand();
                //MessagingCenter.Unsubscribe<LoginPopupVM>(obj, "ToDataBase");
            });
            MessagingCenter.Subscribe<LocalEditRouteVM, Route>(this, "DeleteRouteLocal", async (obj, route) => await Remove(route));
        }
        protected override bool CheckConnection() => false;
        //private async Task ToDataBase(Route route)
        //{
        //    var services = new ApiServices();
        //    if (!String.IsNullOrEmpty(Settings.AccessToken) && CrossConnectivity.Current.IsConnected)
        //    {
        //        IsBusy = true;
        //        await CreateAndDelete(route, services);
        //        IsBusy = false;
        //        ExecuteLoadItemsCommand();
        //    }
        //}

        //private async Task CreateAndDelete(Route route, ApiServices services)
        //{
        //    var created = await Create(route, services);
        //    if(created) routes.DeleteItemAsync(route);
        //}

        //private async Task<bool> Create(Route route, ApiServices services)
        //{
        //   return await services.CreateRouteAsync(Settings.AccessToken, route);
        //}
    }
}
