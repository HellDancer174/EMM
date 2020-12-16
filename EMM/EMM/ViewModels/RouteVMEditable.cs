using EMM.Models;
using EMM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LifeHacks;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using EMM.Helpers;
using EMM.ViewModels.RouteViewModels;

namespace EMM.ViewModels
{
    public class RouteVMEditable: RouteDetailVM
    {
        public ImageSource SaveIcon { get; set; } = "round_save_24";

        protected CommandsCreater commander;
        protected IDictionary<string, int> locomotiveSection;
        public ObservableCollection<string> LocomotiveTypes { get; set; }
        private string addedLocomotiveType;
        public string AddedLocomotiveType
        {
            get { return addedLocomotiveType; }
            set
            {
                if (value == addedLocomotiveType) return;
                addedLocomotiveType = value;
                OnPropertyChanged();
            }
        }
        private string addedLocomotiveNumber;
        public string AddedLocomotiveNumber
        {
            get { return addedLocomotiveNumber; }
            set
            {
                if (value == addedLocomotiveNumber) return;
                addedLocomotiveNumber = value;
                OnPropertyChanged();
            }
        }
        public bool Checker { get; set; }

        public Command AddPassangerCommand
        {
            get
            {
                return commander.Create(() => AddPassanger());
            }
        }

        public Command RemovePassangerCommand
        {
            get
            {
                return commander.Create(() => RemovePassanger());
            }
        }

        public Command AddLocomotiveCommand
        {
            get
            {
                return commander.Create(() => { if (AddedLocomotiveType == null||AddedLocomotiveNumber==null) return; AddLocomotive(AddedLocomotiveType, AddedLocomotiveNumber, locomotiveSection[AddedLocomotiveType]); });
            }
        }

        //public static explicit operator RouteVMEditable(bool v)
        //{
        //    throw new NotImplementedException();
        //}

        public Command RemoveLocomotiveCommand
        {
            get
            {
                return commander.Create(() => RemoveLocomotive());
            }
        }

        public Command AddTrainCommand
        {
            get
            {
                return commander.Create(() => AddTrain());
            }
        }

        public Command RemoveTrainCommand
        {
            get
            {
                return commander.Create(() => RemoveTrain());
            }
        }

        public Command AddStationCommand
        {
            get
            {
                return commander.Create(() => AddStation());
            }
        }

        public Command RemoveStationCommand
        {
            get
            {
                return commander.Create(() => RemoveStation());
            }
        }
        public string Message;

        public RouteVMEditable(IRouteModel model):base(model)
        {
            commander = new CommandsCreater();
            if(Settings.LocomotivesTypes!=null)
            {
                locomotiveSection = Settings.LocomotivesTypes;
                LocomotiveTypes = new ObservableCollection<string>(locomotiveSection.Keys);
            }
            else
            {
                locomotiveSection = new Dictionary<string, int>();
                LocomotiveTypes = new ObservableCollection<string>();
            }
        }


        public virtual bool Save()
        {
            //Not more 24 hours
            var start = DateOfStart.Date + Start;
            var finish = DateOfFinish.Date + Finish;
            if(finish-start>new TimeSpan(23,59,59))
            {
                Message = "Рабочее время больше 24 часов";
                return false;
            }
            var locomotives = Locomotives.Select(loc=>loc.ToLocomotive(CreateMeters(loc.Name), DateOfStart, DateOfFinish, Start, Finish)).ToList();
            //var meters = new List<Meters>();
            //Join(locomotives);
            var trains = this.Trains.Select(train =>
            {
                var locomotive = locomotives.Where(loc => loc.ToString() == train.Locomotive).FirstOrDefault();
                return train.ToTrain(locomotive);
            }).ToList();
            var passangers = this.Passangers.Select(passanger=>passanger.ToPassanger(DateOfStart, DateOfFinish, Start, Finish)).ToList();
            List<Station> stations = new List<Station>();
            if(Stations.Count>1)
            {
                DateTime arraval, depature;
                //GetStationTime(stations, out arraval, out depature);
                var first = Stations[0];
                //Stations[Stations.Count - 1] = new LastStationVM(Stations[Stations.Count - 1]);
                var last = Stations[Stations.Count - 1];
                if (Start > first.ArravalTime) arraval = DateOfFinish.Date;
                else arraval = DateOfStart.Date;
                if (last.DepatureTime > Finish) depature = DateOfStart.Date;
                else depature = DateOfFinish.Date;
                var currentArraval = first.RebuildModel(arraval, depature, Settings.DefaultDateTime, true, false);
                stations.Add(first.ToStation());
                if (Stations.Count > 2) stations.AddRange(Stations.Select(1, Stations.Count - 2, station =>
                {
                    currentArraval = station.RebuildModel(arraval, depature, currentArraval);
                    return station.ToStation();
                }));
                currentArraval = RebuildLocalLastStation(arraval, depature, last, currentArraval);
                stations.Add(last.ToStation());
                //stations.AddRange(Stations.Select(station => station.ToStation(arraval, depature)).ToList());
            }
            else if (Stations.Count == 1)
            {
                var single = new SingleStationVM(Stations[0]);
                single.RebuildModel(DateOfStart, DateOfFinish, Settings.DefaultDateTime);
                stations.Add(single.ToStation());
            }
            model.Rebuild(new Route(-1, start, finish, locomotives, trains, stations, passangers, Comment, SingleMachinist));
            return true;
        }

        protected virtual DateTime RebuildLocalLastStation(DateTime arraval, DateTime depature, StationVM last, DateTime currentArraval)
        {
            currentArraval = last.RebuildModel(arraval, depature, currentArraval, false, true);
            return currentArraval;
        }

        //private void GetStationTime(List<Station> stations, out DateTime arraval, out DateTime depature)
        //{
        //    //Stations[0] = new FirstStationVM(Stations[0]);
        //    var first = Stations[0];
        //    //Stations[Stations.Count - 1] = new LastStationVM(Stations[Stations.Count - 1]);
        //    var last = Stations[Stations.Count - 1];
        //    if (Start > first.ArravalTime) arraval = DateOfFinish.Date;
        //    else arraval = DateOfStart.Date;
        //    if (last.DepatureTime > Finish) depature = DateOfStart.Date;
        //    else depature = DateOfFinish.Date;
        //    stations.Add(first.ToStation(arraval, depature, true, false));
        //    stations.Add(last.ToStation(arraval, depature, false, true));

        //}

        //protected void Join(List<Locomotive> locomotives)
        //{

        //    foreach (var locomotive in Locomotives)
        //    {
        //        locomotives.Add(locomotive.ToLocomotive(GetMeters(locomotive.Name), DateOfStart, DateOfFinish, Start, Finish));
        //    }
        //}
        protected IEnumerable<Meters> CreateMeters(string locName)
        {
            var metersOfLocomotive = Meters.Where(meter=>meter.LocomotiveName==locName)
                .Select(meter => meter.ToMeters()).ToList();
            return metersOfLocomotive;
        }

    }
}
