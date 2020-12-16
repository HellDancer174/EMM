using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.Helpers;
using EMM.Models;
using EMM.ViewModels;
using Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace EMM.Services
{
    public class LocalRoutes : IRoutes
    {
        private List<Route> routes;

        private string fileName;

        public LocalRoutes()
        {
            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "localRoutes.json");
            GetItemsAsync();
        }

        public Task<bool> AddItemAsync(Route item)
        {
            //item.AddInLocalRoutes(routes);
            routes.Add(item);
            Serialize();
            return Task.FromResult<bool>(true);
        }

        private void Serialize()
        {
            var json = JsonConvert.SerializeObject(routes);
            File.WriteAllText(fileName, json);
            //stream.Write(json);
        }

        public Task<bool> DeleteItemAsync(Route item)
        {
            var isSuccess = routes.Remove(item);
            Serialize();
            return Task.FromResult<bool>(isSuccess);
        }

        public Task<Route> GetItemAsync(Route item)
        {
            return Task.FromResult(routes.Where(route => route == item).FirstOrDefault());
        }

        public Task<IEnumerable<Route>> GetItemsAsync(bool forceRefresh = false)
        {
            //var stream = new StreamReader(Android.App.Application.Context.Assets.Open("localRoutes.json"));
            try
            {
                var jsonRoutes = File.ReadAllText(fileName);
                routes = JsonConvert.DeserializeObject<List<Route>>(jsonRoutes);
                if (routes == null) routes = new List<Route>();
            }
            catch
            {
                File.Create(fileName).Dispose();
                routes = new List<Route>();
            }
            return Task.FromResult<IEnumerable<Route>>(routes);
        }

        public Task GetRouteInfo()
        {
            var locomotivesStream = new StreamReader(Android.App.Application.Context.Assets.Open("Locomotives.json"));
            var stationsStream = new StreamReader(Android.App.Application.Context.Assets.Open("Stations.json"));
            var jsonLocomotives = locomotivesStream.ReadToEnd();
            var jsonStations = stationsStream.ReadToEnd();
            Settings.LocomotivesTypes = JsonConvert.DeserializeObject<IDictionary<string, int>>(jsonLocomotives);
            Settings.StationsNames = JsonConvert.DeserializeObject<IEnumerable<string>>(jsonStations);
            return Task.FromResult<bool>(true);
        }

        public async Task<IEnumerable<StringFromRoute>> ToStringsAsync()
        {
            var result = routes.Select(route => new StringFromRoute(route)).Reverse().ToList();
            return await Task.FromResult(result);
        }

        public Task<bool> UpdateItemAsync(Route item)
        {
            //var updateflag = false;
            //foreach(var route in routes)
            //{
            //    if (route.Equals(item)) updateflag=true;
            //}
            //if (updateflag) item.UpdateInLocalRoutes(routes);
            Serialize();
            return Task.FromResult<bool>(true);
        }
        public bool IsEmpty()
        {
            if (routes.Count == 0) return true;
            else return false;
        }

    }
}
