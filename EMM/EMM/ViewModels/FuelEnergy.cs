using EMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.ViewModels
{
    public class FuelEnergy: RouteCellVM
    {
        public string Cost { get; set; }

        public FuelEnergy(IRouteModel model):base(model)
        {
        }

        public override void Rebuild()
        {
            model.Publish((worksStart, worksFinish, locomotives, passangers, trains, stations, comment, single) =>
            {
                Date = worksStart.Date;
                if (stations.Count() == 0) Direction = "Отсутствует направление";
                else Direction = stations.FirstOrDefault().ToString() + " - " + stations.LastOrDefault().ToString();
                var cost = locomotives.Select(loc => loc.ToCostFuelEnergy()).Sum();
                Cost = "Расход ТЭР: " + Math.Round(cost, 2);
            });
        }
    }
}
