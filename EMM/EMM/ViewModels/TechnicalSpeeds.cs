using EMM.Models;
using EMM.Services;
using EMM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace EMM.ViewModels
{
    public class TechnicalSpeeds : ListViewVM
    {
        public ObservableCollection<TechnicalSpeedVM> TechSpeed { get; set; }
        private Directions directions;
        private TryCatcher catcher;
        private ApiServices services;
        public TechnicalSpeeds(IRoutes model, ICommandPage page) : base(model, page)
        {
            catcher = new HttpTryCatcher(new PageTryCatcher(page));
            services = new ApiServices();
        }

        protected override async Task Rebuild()
        {
            IEnumerable<Direction> dir = await services.GetDirectionsAsync();
            //await catcher.Execute(async() => dir = await services.GetDirectionsAsync());
            IEnumerable<Route> newRoutes = await routes.GetItemsAsync();
            directions = new Directions(dir.ToList());
            TechSpeed = new ObservableCollection<TechnicalSpeedVM>(newRoutes.Select(route => new TechnicalSpeedVM(route, directions)).ToList());
            OnPropertyChanged(nameof(TechSpeed));
        }
    }
}
