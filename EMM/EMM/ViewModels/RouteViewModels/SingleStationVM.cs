using System;
using System.Collections.Generic;
using System.Text;
using EMM.Models;

namespace EMM.ViewModels.RouteViewModels
{
    public class SingleStationVM : StationVM
    {
        public SingleStationVM(StationVM stationVM)
        {
            this.Name = stationVM.Name;
            this.DepatureTime = stationVM.DepatureTime;
            this.ArravalTime = stationVM.ArravalTime;
            this.Maneuvers = stationVM.Maneuvers;
            this.StationsNames = stationVM.StationsNames;
            this.StationModel = stationVM.StationModel;
        }
        public SingleStationVM(IStationModel stationModel) : base(stationModel)
        {

        }
        //public override void RebuildModel(DateTime arravalDate, DateTime depatureDate)
        //{
        //    var depature = depatureDate.Date;
        //    var arraval = arravalDate.Date;
        //    var station = new Station(-1, Name, depature, arraval, Maneuvers);
        //    StationModel.Rebuild(station);
        //}
    }
}
