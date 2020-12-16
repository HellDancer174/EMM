using EMM.Helpers;
using EMM.Models;
using EMM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EMM.ViewModels
{
    public class StationVM
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                if (ChangeName != null) ChangeName.Invoke();
            }

        }
        public TimeSpan DepatureTime { get; set; }
        public TimeSpan ArravalTime { get; set; }
        public TimeSpan Maneuvers { get; set; }
        public event Action ChangeName;
        public ObservableCollection<string> StationsNames { get; set; }
        public IStationModel StationModel { get; set; }
        public StationVM(IStationModel stationModel)
        {
            
            this.StationModel = stationModel;
            stationModel.Publish((id, name, depature, arraval, maneuvers) =>
            {
                this.Name = name;
                this.DepatureTime = depature.TimeOfDay;
                this.ArravalTime = arraval.TimeOfDay;
                this.Maneuvers = maneuvers;
            });

            if(Settings.StationsNames!=null) StationsNames = new ObservableCollection<string>(Settings.StationsNames);
            else StationsNames = new ObservableCollection<string>() { Name };
        }
        public StationVM()
        {
        }

        public Station ToStation(/*DateTime arravalDate, DateTime depatureDate, bool first = false, bool last = false*/)
        {
            //RebuildModel(arravalDate, depatureDate, first, last);
            return (Station)StationModel;
        }

        public virtual DateTime RebuildModel(DateTime arravalDate, DateTime depatureDate, DateTime previousArraval, bool first=false, bool last=false)//Можно воспользоваться полиморфизмом
        {
            DateTime depature, arraval;
            Station station;
            if (first)
            {
                depature = new DateTime(1837, 11, 11, 00, 00, 00);
                arraval = arravalDate.Date + this.ArravalTime;
                station = new Station(-1, this.Name, depature, arraval, this.Maneuvers);
            }
            else if (last)
            {
                arraval = new DateTime(1837, 11, 11, 00, 00, 00);
                depature = depatureDate.Date + this.DepatureTime;
                station = new Station(-1, this.Name, depature, arraval, this.Maneuvers);
            }

            else MiddleRebuild(arravalDate, depatureDate, previousArraval, out depature, out arraval, out station);
            StationModel.Rebuild(station);
            return arraval;
        }

        protected void MiddleRebuild(DateTime arravalDate, DateTime depatureDate, DateTime previousArraval, out DateTime depature, out DateTime arraval, out Station station)
        {
            if (this.DepatureTime > this.ArravalTime)
            {
                depature = arravalDate.Date + this.DepatureTime;
                arraval = depatureDate.Date + this.ArravalTime;
                station = new Station(-1, this.Name, depature, arraval, this.Maneuvers);
            }
            else if (DepatureTime < previousArraval.TimeOfDay || previousArraval.Date > arravalDate.Date)
            {
                depature = depatureDate.Date + this.DepatureTime;
                arraval = depatureDate.Date + this.ArravalTime;
                station = new Station(-1, this.Name, depature, arraval, this.Maneuvers);
            }
            else
            {
                depature = arravalDate.Date + this.DepatureTime;
                arraval = arravalDate.Date + this.ArravalTime;
                station = new Station(-1, this.Name, depature, arraval, this.Maneuvers);
            }
        }
    }
}
