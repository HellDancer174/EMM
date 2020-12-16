using EMM.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.ViewModels;
using Plugin.Connectivity;
using EMM.Helpers;

namespace EMM.Services
{
    public class Routes : IDataStore<Route>, IRoutes
    {
        private IList<Route> routes;
        private ApiServices services = new ApiServices();

        public Routes()//Do method "Refresh"
        {
            if (!CrossConnectivity.Current.IsConnected) return;
            routes = new List<Route>();
            GetItemsAsync();
        }

        public Routes(IEnumerable<Route> result)
        {
            if (!CrossConnectivity.Current.IsConnected) return;
            routes = new List<Route>();
            var routeArray = result.ToArray();
            for(int i = routeArray.Length-1; i >= 0; i--)
            {
                routes.Add(routeArray[i]);
            }
        }

        public async Task<bool> AddItemAsync(Route item)  // Добавление элемента в routes
        {
            var result = await services.CreateRouteAsync(Settings.AccessToken, item);
            if(result) await GetItemsAsync();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateItemAsync(Route item) // Обновление элемента
        {
            var oldItem = routes.Where((Route arg) => arg.Equals(item)).FirstOrDefault();
            await services.RefreshRouteAsync(Settings.AccessToken, item);
            oldItem = item;          
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Route item) // Удаление элемента
        {
            var oldItem = routes.Where((Route arg) => arg.Equals(item)).FirstOrDefault();
            var deleted = await services.DeleteRouteAsync(Settings.AccessToken, item);
            if(deleted) routes.Remove(oldItem);
            return await Task.FromResult(deleted);
        }

        public async Task<Route> GetItemAsync(Route item) // Получение нужного элемента из списка
        {
            return await Task.FromResult(routes.FirstOrDefault(arg => arg.Equals(item)));
        }

        public async Task<IEnumerable<Route>> GetItemsAsync(bool forceRefresh = false)
        {
            var list = await services.GetRoutesAsync(Settings.AccessToken);
            var routesArray = list.ToArray();
            routes.Clear();
            for(int i = routesArray.Length-1; i >= 0; i--)
            {
                routes.Add(routesArray[i]);
            }
            return await Task.FromResult(routes);
        }
        public async Task<IEnumerable<StringFromRoute>> ToStringsAsync()// В строку с списке маршрутов
        {
            var result = routes.Select(route => new StringFromRoute(route)).ToList();
            return await Task.FromResult(result);
        }
        public async Task GetRouteInfo()
        {
            var services = new ApiServices();
            Settings.LocomotivesTypes = await services.GetLocomotivesAsync();
            Settings.StationsNames = await services.GetStationsAsync();
        }

        public async Task<List<WorkTime>> ToWorkTimes()
        {
            var workTimes = routes.Select(route => new WorkTime(route)).ToList();
            return await Task.FromResult(workTimes);
        }

        public bool IsEmpty()
        {
            if (routes.Count == 0) return true;
            else return false;
        }
        //public RouteDetailVM RouteToView(StringFromRoute @string)
        //{
        //    var index = routes.Select(route => route.Equals(@string)).ToList().IndexOf(true);
        //    Route newRoute;
        //    if (index == -1) newRoute = new Route();
        //    else newRoute = routes[index];
        //    return new RouteDetailVM(newRoute);

        //}

    }
}
