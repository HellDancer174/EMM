using EMM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EMM.ViewModels
{
    public class RouteVM : BaseViewModel
    {
        protected IRouteModel model;
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfFinish { get; set; }
        public TimeSpan Start { get; set;}
        public TimeSpan Finish { get; set; }
        public ObservableCollection<PassangerVM> Passangers { get; set; }
        public ObservableCollection<LocomotiveVM> Locomotives { get; set; }
        public ObservableCollection<MetersVM> Meters { get; set; }
        public ObservableCollection<TrainVM> Trains { get; set; }
        public ObservableCollection<StationVM> Stations { get; set; }
        public string Comment { get; set; }
        public bool SingleMachinist { get; set; }
        private event Action OnAddLocomotive;
        private event Action OnRemoveLocomotive;
        public RouteVM(IRouteModel routeModel)
        {
            this.model = routeModel;
            RebuildMe();
        }

        public virtual void RebuildMe()
        {
            model.Publish((worksStart, worksFinish, locomotives, passangers, trains, stations, comment, single) =>
            {
                DateOfStart = worksStart.Date;
                DateOfFinish = worksFinish.Date;
                Start = worksStart.TimeOfDay;
                Finish = worksFinish.TimeOfDay;
                var newMeters = new List<MetersVM>();
                Locomotives = new ObservableCollection<LocomotiveVM>(locomotives.Select(loc =>
                {
                    var locMeters = loc.Publish((type, number, times, meters, sections) => meters).ToList();
                    newMeters.AddRange(locMeters.Select(meter => new MetersVM(meter, loc.ToString())).ToList());
                    return new LocomotiveVM(loc);
                }).ToList());
                Meters = new ObservableCollection<MetersVM>(newMeters);
                Passangers = new ObservableCollection<PassangerVM>(passangers.Select(pass => new PassangerVM(pass)).ToList());
                Stations = new ObservableCollection<StationVM>(stations.Select(station => new StationVM(station)).ToList());
                Trains = new ObservableCollection<TrainVM>(trains.Select(train => new TrainVM(Stations, Locomotives, train)).ToList());
                Comment = comment;
                SingleMachinist = single;
            });
            foreach (var train in Trains)
            {
                AddChangeNamesReference(train);
            }
        }

        public void AddPassanger()
        {
            if (this.Passangers.Count == 10) return;
            this.Passangers.Add(new PassangerVM(new Passanger()));
        }
        public void RemovePassanger()
        {
            if (Passangers.Count == 0) return;
            Passangers.RemoveAt(Passangers.Count - 1);
        }
        public void AddLocomotive(string type, string number, int sectionCount)
        {
            if (this.Locomotives.Count == 5) return;
            var other = number.ToString();
            var parsedNumber = number.Split(other.ToCharArray().Where(symbol => !Char.IsDigit(symbol) || !Char.IsNumber(symbol)).ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var goodNumber = String.Join("/", parsedNumber);
            if (sectionCount == 1) goodNumber = String.Concat(number.ToCharArray().Where(symbol => Char.IsDigit(symbol) || Char.IsNumber(symbol)).ToArray());
            var locomotive = new LocomotiveVM(new Locomotive(type, goodNumber, new BackgroundTime(), new List<Meters>(),sectionCount));
            this.Locomotives.Add(locomotive);
            for (int i = 0; i < sectionCount; i++)
            {
                Meters.Add(new MetersVM(new Meters(),locomotive.Name));
            }
            if (OnAddLocomotive != null) OnAddLocomotive.Invoke();
        }

        public void RemoveLocomotive()
        {
            if (this.Locomotives.Count == 0) return;
            var sectionCount = this.Locomotives[Locomotives.Count - 1].SectionCount;
            this.Locomotives.RemoveAt(this.Locomotives.Count - 1);
            for (int i = 0; i < sectionCount; i++)
            {
                this.Meters.RemoveAt(this.Meters.Count - 1);
            }
            if (OnRemoveLocomotive != null) OnRemoveLocomotive.Invoke();
        }

        public void AddStation()
        {
            if (Locomotives.Count == 0) return;
            var station = new StationVM(new Station());
            AddChangeNamesReference(station);
            this.Stations.Add(station);
        }

        private void AddChangeNamesReference(StationVM station)
        {
            foreach (var train in Trains)
            {
                station.ChangeName += train.ResetStationsName;
            }
        }

        public void RemoveStation()
        {
            if (this.Stations.Count > 0) this.Stations.RemoveAt(this.Stations.Count - 1);
            foreach (var train in Trains)
            {
                train.ResetStationsName();
            }
        }
        public void AddTrain()
        {
            if (Locomotives.Count == 0) return;
            var train = new TrainVM(this.Stations, this.Locomotives, new Train());
            AddChangeNamesReference(train);
            OnAddLocomotive += train.RefreshLocomotiveName;
            OnRemoveLocomotive += train.RefreshLocomotiveName;
            this.Trains.Add(train);

        }

        private void AddChangeNamesReference(TrainVM train)
        {
            foreach (var station in Stations)
            {
                station.ChangeName += train.ResetStationsName;
            }
        }

        public void RemoveTrain()
        {
            if (this.Trains.Count == 0) return;
            this.Trains.RemoveAt(this.Trains.Count - 1);
        }
    }
}
