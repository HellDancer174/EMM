using System.Collections.Generic;
using System.Threading.Tasks;
using EMM.Models;
using EMM.ViewModels;

namespace EMM.Services
{
    public interface IRoutes
    {
        bool IsEmpty();
        Task<bool> AddItemAsync(Route item);
        Task<bool> DeleteItemAsync(Route item);
        Task<Route> GetItemAsync(Route item);
        Task<IEnumerable<Route>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<StringFromRoute>> ToStringsAsync();
        Task<bool> UpdateItemAsync(Route item);
        Task GetRouteInfo();
    }
}