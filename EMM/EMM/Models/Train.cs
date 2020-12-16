using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Train : ITrainModel
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        protected int number;
        [JsonProperty]
        protected string arravalStation;
        [JsonProperty]
        protected string depatureStation;
        [JsonProperty]
        protected Locomotive locomotive;
        [JsonProperty]
        private string type;
        [JsonProperty]
        private int weight;
        [JsonProperty]
        private int axisCount;
        [JsonProperty]
        private int length;

        public void Publish(Action<int, int, string, string, Locomotive, string, int, int, int> instructionsForMe)
        {
            instructionsForMe(id, number, arravalStation, depatureStation, locomotive, type, weight, axisCount, length);
        }

        public Train(int id, int number, string arravalStation, string depatureStation, Locomotive locomotives, string type, int weight, int axis, int length)
        {
            this.id = id;
            this.number = number;
            this.arravalStation = arravalStation;
            this.depatureStation = depatureStation;
            this.locomotive = locomotives;
            this.type = type;
            this.weight = weight;
            this.axisCount = axis;
            this.length = length;
        }

        public Train()
        {
            id = -1;
            number = 0;
            arravalStation = String.Empty;
            depatureStation = String.Empty;
            locomotive = new Locomotive();
            type = null;
            weight = 0;
            axisCount = 0;
            length = 0;
        }

        public bool CheckArraval(string firstStation)
        {
            if (firstStation == arravalStation) return true;
            return false;
        }
        public bool CheckDepature(string lastStation)
        {
            if (lastStation == depatureStation) return true;
            return false;
        }
        public bool Check()
        {
            var trueData = (weight == 0 && axisCount == 0 && length == 0) || (weight != 0 && axisCount != 0 && length != 0);
            return !(number == 0 || String.IsNullOrEmpty(arravalStation) || String.IsNullOrEmpty(depatureStation) || locomotive == null) && trueData;
        }
        public TResult Publish<TResult>(Func<int, int, string, string, Locomotive, string, int, int, int, TResult> instructions)
        {
            return instructions(id, number, arravalStation, depatureStation, locomotive, type, weight, axisCount, length);
        }

        public void Rebuild(Train train)
        {
            this.number = train.number;
            this.weight = train.weight;
            this.type = train.type;
            this.axisCount = train.axisCount;
            this.length = train.length;
            this.arravalStation = train.arravalStation;
            this.depatureStation = train.depatureStation;
            this.locomotive = train.locomotive;
        }
        public override bool Equals(object obj)
        {
            var otherTrain = obj as Train;
            if (otherTrain == null) return false;
            else return otherTrain.id == this.id;
        }
        public TechSpeedModel ToTechnicalSpeed(IEnumerable<Station> stations, Directions directions)
        {
            var list = new List<double>();
            if (((number > 1000 && number < 2999) || (number > 9000 && number < 9899)) && stations.Count() != 0)
            {
                //var splitter = new SplitterDirection(arravalStation, depatureStation);
                //var directions = splitter.Split();
                var subStationer = CreateSubStationer(stations);
                IEnumerable<Station> ourStations = subStationer.CutStations();
                var ourDirection = directions.ToDirection(ourStations.Select(station => station.ToString()).ToList());
                var splitter = new SplitterDirection(ourDirection.Item1, ourDirection.Item2);
                var traindirections = splitter.Split();
                IStationsTimeCounter timeCounter = new StationsTimeCounter();
                foreach (var direction in traindirections)
                {
                    ISubStationer current = new SubStationer(stations, direction.Item1, direction.Item2);
                    var currentStations = current.CutStations();
                    var downtime = timeCounter.GetDownTime(currentStations);
                    var fulltime = timeCounter.GetFullTime(currentStations);
                    IDistance distance = new TrainDistance(direction.Item1, direction.Item2);
                    list.Add(distance.ToDistance() / (fulltime - downtime));
                }
            }
            return new TechSpeedModel(list, number);
        }

        //public IEnumerable<Station> CutStations(IEnumerable<Station> stations)
        //{
        //    ISubStationer general = new SubStationer(stations, arravalStation, depatureStation);
        //    var ourStations = general.CutStations();
        //    return ourStations;
        //}
        public ISubStationer CreateSubStationer(IEnumerable<Station> stations)
        {
            return new SubStationer(stations, arravalStation, depatureStation);
        }

        public override string ToString()
        {
            return number.ToString();
        }
        public double Difference(DateTime start, IEnumerable<Station> ourStations)
        {
            if (ourStations.Count() == 0) return -1;
            return ourStations.First().Difference(start);
        }
        //public TrainPremium ToTrainPremium(DateTime start, IEnumerable<Station> ourStations)
        //{
        //    var timeCounter = new StationsTimeCounter();
        //    var worktime = timeCounter.GetFullTime(start, ourStations);
        //    return new TrainPremium(number, type, length, start, worktime);
        //}
        //public TrainPremium ToTrainPremium(DateTime start, DateTime finish)
        //{
        //    return new TrainPremium(number, type, length, start, (finish-start).TotalHours);
        //}

        public IWorkTimeModel ToWorkTime(IEnumerable<Station> stations)
        {
            var subStationer = CreateSubStationer(stations);
            var ourStations = subStationer.CutStations();
            var count = ourStations.Count();
            if (count == 0 || count == 1) return new TrainWorkTimeModel(new DateTime(), new DateTime(), true, length, type, number);
            else return ourStations.First().ToWorkTime(ourStations.Last(), length, type, number);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
