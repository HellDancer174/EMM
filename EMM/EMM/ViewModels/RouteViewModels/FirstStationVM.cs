using System;
using System.Collections.Generic;
using System.Text;
using EMM.Models;

namespace EMM.ViewModels.RouteViewModels
{
    public class FirstStationVM : StationVM
    {
        public FirstStationVM(StationVM stationVM)
        {
            this.Name = stationVM.Name;
            this.DepatureTime = stationVM.DepatureTime;
            this.ArravalTime = stationVM.ArravalTime;
            this.Maneuvers = stationVM.Maneuvers;
            this.StationsNames = stationVM.StationsNames;
            this.StationModel = stationVM.StationModel;
        }
        public FirstStationVM(IStationModel stationModel) : base(stationModel)
        {
        }
        //public override void RebuildModel(DateTime arravalDate, DateTime depatureDate)
        //{
        //    var depature = new DateTime(1837, 11, 11, 00, 00, 00);
        //    var arraval = arravalDate.Date + this.ArravalTime;
        //    var station = new Station(-1, this.Name, depature, arraval, this.Maneuvers);
        //    this.StationModel.Rebuild(station);
        //}
    }
}
