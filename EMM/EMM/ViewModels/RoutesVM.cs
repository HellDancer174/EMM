using EMM.Helpers;
using EMM.Models;
using EMM.Services;
using EMM.Views;
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
    public class RoutesVM: ListViewVM
    {
        public ObservableCollection<StringFromRoute> StringRoutes { get; set; }
        //public IRoutes pack;
        //private INavigational page;
        public RoutesVM(IRoutes pack, ItemsPage page) : base(pack, page)
        {
            this.StringRoutes = new ObservableCollection<StringFromRoute>();
            Subscribe();
            //OnChangeView();

        }



        protected virtual void Subscribe()
        {
            MessagingCenter.Subscribe<NewRouteVM, Route>(this, "AddRoute", async (obj, route) =>
            {
                await Add(route);
            });
            MessagingCenter.Subscribe<EditRouteVM, Route>(this, "RefreshRoute", async (obj, route) =>
            {
                await Refresh(route);
            });
            MessagingCenter.Subscribe<RouteDetailVM, Route>(this, "DeleteRoute", (obj, route) => Remove(route));
        }

        protected async Task Refresh(Route route)
        {
            var newRoute = route;
            await routes.UpdateItemAsync(newRoute); // It was Async
            //await OnChangeView();
        }

        protected async Task Add(Route route)
        {
            var newRoute = route;
            var isSuccess = await routes.AddItemAsync(newRoute);
            if(isSuccess)
            {
                StringRoutes.Add(new StringFromRoute(newRoute));
                OnPropertyChanged(nameof(StringRoutes));
            }
        }

        protected async Task Remove(Route route)
        {
            var isSuccess = await routes.DeleteItemAsync(route);
            if (isSuccess) await OnChangeView();
        }

        //public async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        await routes.GetRouteInfo();
        //        await routes.GetItemsAsync();
        //        await OnChangeView();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        private async Task OnChangeView()
        {
            //if (IsBusy) return;
            //IsBusy = true;
            StringRoutes.Clear();
            var strings = await routes.ToStringsAsync();
            foreach (var route in strings)
            {
                StringRoutes.Add(route);
            }
            //IsBusy = false;
        }

        protected override async Task Rebuild()
        {
            await routes.GetRouteInfo();
            await routes.GetItemsAsync();
            await OnChangeView();
        }

    }
}
