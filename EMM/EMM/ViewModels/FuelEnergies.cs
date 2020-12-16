using EMM.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using Plugin.Connectivity;
using EMM.Views;
using System.Diagnostics;

namespace EMM.ViewModels
{
    public class FuelEnergies : ListViewVM
    {
        public ObservableCollection<FuelEnergy> FuelEnergyStrings { get; set; }
        //private FuelEnergyPage page;

        public FuelEnergies(IRoutes routes, FuelEnergyPage page):base(routes, page)
        {
            //this.routes = routes;
            //this.page = page;
            //Load = new Command(async () => await ExecuteLoadItemsCommand());
            //Rebuild();
        }

        protected override async Task Rebuild()
        {
            var newRoutes = await routes.GetItemsAsync();
            FuelEnergyStrings = new ObservableCollection<FuelEnergy>(newRoutes.Select(route => new FuelEnergy(route)).ToList());
            OnPropertyChanged(nameof(FuelEnergyStrings));
        }



        //public async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;
        //    if (!CrossConnectivity.Current.IsConnected)
        //    {
        //        page.PrintErorAsync("Отсутствует подключение к интернету");
        //        return;
        //    }

        //    try
        //    {
        //        await Rebuild();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        OnPropertyChanged(nameof(FuelEnergyStrings));
        //        IsBusy = false;
        //    }
        //}
    }
}
