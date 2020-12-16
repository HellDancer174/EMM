using System;
using System.Collections.Generic;
using System.Text;
using EMM.Models;

namespace EMM.ViewModels.RouteViewModels
{
    public class LastStationVM : StationVM
    {
        public LastStationVM(StationVM stationVM)
        {
            this.Name = stationVM.Name;
            this.DepatureTime = stationVM.DepatureTime;
            this.ArravalTime = stationVM.ArravalTime;
            this.Maneuvers = stationVM.Maneuvers;
            this.StationsNames = stationVM.StationsNames;
            this.StationModel = stationVM.StationModel;
        }

        public LastStationVM(IStationModel stationModel) : base(stationModel)
        {
        }
        //public override void RebuildModel(DateTime arravalDate, DateTime depatureDate)
        //{
        //    var arraval = new DateTime(1837, 11, 11, 00, 00, 00);
        //    var depature = depatureDate.Date + this.DepatureTime;
        //    var station = new Station(-1, this.Name, depature, arraval, this.Maneuvers);
        //    StationModel.Rebuild(station);
        //}
    }
}
