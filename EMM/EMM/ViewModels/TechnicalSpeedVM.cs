using EMM.Models;
using EMM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.ViewModels
{
    public class TechnicalSpeedVM: RouteCellVM
    {
        private ApiServices services = new ApiServices();
        private Directions directions;
        public ObservableCollection<string> Speeds { get; set; }

        public TechnicalSpeedVM(IRouteModel model, Directions directions):base(model)
        {
            this.directions = directions;
            Rebuild();
        }
        public override void Rebuild()
        {
            if (directions == null) return;
            model.Publish((worksStart, worksFinish, locomotives, passangers, trains, stations, comment, single) =>
            {
                Date = worksStart.Date;
                if (stations.Count() == 0) Direction = "Отсутствует направление";
                else Direction = stations.FirstOrDefault().ToString() + " - " + stations.LastOrDefault().ToString();
                var techspeeds = trains.Select(train => train.ToTechnicalSpeed(stations, directions).ToString());
                Speeds = new ObservableCollection<string>(techspeeds);
            });
        }

    }
}
