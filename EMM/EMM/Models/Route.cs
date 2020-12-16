using LifeHacks;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using EMM.ViewModels;
using Newtonsoft.Json;
using EMM.Helpers;

namespace EMM.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Route : IEquatable<Route>, IRouteModel
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        private DateTime start;
        [JsonProperty]
        private DateTime finish;
        [JsonProperty]
        protected List<Locomotive> locomotives;
        [JsonProperty]
        protected List<Train> trains;
        [JsonProperty]
        protected List<Station> stations;
        [JsonProperty]
        private List<Passanger> passangers;
        [JsonProperty]
        private string comment;
        [JsonProperty]
        protected bool single;
        public Route()
        {
            this.trains = new List<Train>();
            this.stations = new List<Station>();
            this.passangers = new List<Passanger>();
            this.locomotives = new List<Locomotive>();
            this.id = -1;
            start = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified);
            finish = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified);
            comment = String.Empty;
            single = false;
        }
        public Route(int id, DateTime start, DateTime finish, IEnumerable<Locomotive> locomotives, IEnumerable<Train> trains, IEnumerable<Station> stations, IEnumerable<Passanger> passangers, string comment, bool single)
        {
            this.id = id;
            this.start = start;
            this.finish = finish;
            this.locomotives = new List<Locomotive>(locomotives);
            this.trains = new List<Train>(trains);
            this.stations = new List<Station>(stations);
            this.passangers = new List<Passanger>(passangers);
            this.comment = comment;
            this.single = single;
        }
        public bool Check()
        {
            if (this.start == Settings.DefaultDateTime || this.finish == Settings.DefaultDateTime) return false;
            var maxTime = new TimeSpan(12, 0, 0);
            if (start > finish) return false;
            if (locomotives.Select(locomotive => locomotive.Check()).Contains(false)) return false;
            if (stations.Select(station => station.Check()).Contains(false)) return false;
            if (passangers.Select(passanger => passanger.Check()).Contains(false)) return false;
            if (stations.Count > 1)
            {
                if (trains.Count == 0) return false;
                if (trains.Where(train => train.CheckArraval(stations.First().ToString())).Count() < 1) return false;
                if (trains.Where(train => train.CheckDepature(stations.Last().ToString())).Count() < 1) return false;
                if (trains.Select(train => train.Check()).Contains(false)) return false;
                if (stations.Count == 2)
                {
                    if (!stations[0].CheckFirstStation() && !stations[1].CheckLastStation()) return false;
                }
                else if (stations.Select(1, stations.Count - 2, station => station.CheckExpectFirstAndLastStation()).Contains(false)) return false;
            }
            else if (trains.Count > 0) return false;
            var workTime = finish - start;
            if (stations.Select(station => station.GetDownTime()).Sum() > workTime || stations.Select(station => station.GetDownTime()).Sum() > maxTime) return false;
            if (locomotives.Select(locomotive => locomotive.GetLocomotiveWorkTime()).Sum() > workTime) return false;
            return true;
        }

        public bool Equals(Route other)
        {
            if (id == -1)
            { 
                return base.Equals(other);
                //if (equals == true) return equals;
                //else return start == other.start && finish == other.finish;
            }
            if (this.id == other.id) return true;
            else return false;
        }

        public bool IsEmpty()
        {
            if (locomotives.Count == 0 && trains.Count == 0 && stations.Count == 0 && passangers.Count == 0) return true;
            else return false;
        }

        public void Rebuild(Route route)
        {
            this.start = route.start;
            this.finish = route.finish;
            this.locomotives = route.locomotives;
            this.trains = route.trains;
            this.stations = route.stations;
            this.passangers = route.passangers;
            this.comment = route.comment;
            this.single = route.single;
        }

        public void Publish(Action<DateTime, DateTime, IEnumerable<Locomotive>, IEnumerable<Passanger>, IEnumerable<Train>, IEnumerable<Station>, string, bool> instructionsForMe)
        {
            instructionsForMe(start, finish, locomotives, passangers, trains, stations, comment, single);
        }
        public TResult Publish<TResult>(Func<int, DateTime, DateTime, IEnumerable<Locomotive>, IEnumerable<Passanger>, IEnumerable<Train>, IEnumerable<Station>, string, bool, TResult> instructions)
        {
            return instructions(id, start, finish, locomotives, passangers, trains, stations, comment, single);
        }

        public TimeSpan ToWorkTime(int month, int year)
        {
            var start = new DateTime();
            var finish = new DateTime();
            var correcter = new CorrecterWorkDateTime();
            correcter.Correct(out start, out finish, month, year, this.start, this.finish);
            return finish - start;
            //if (month <= 0 || month > 12 || (start.Month != month && finish.Month != month)||(start.Year != year && finish.Year != year)) return default(TimeSpan);  
            //else if (start.Month != month && finish.Month == month && finish.Year == year) return finish - new DateTime(finish.Year, month, 1);
            //else if (start.Month == month && finish.Month != month && start.Year == year) return new DateTime(start.Year, month, DateTime.DaysInMonth(start.Year, month), 23, 59, 59) - start;
            //else if(start.Year != year || finish.Year != year) return default(TimeSpan);
            //else return ToWorkTime();
        }
        public TimeSpan ToWorkTime()
        {
            return finish - start;
        }
        //public void AddInLocalRoutes(IList<Route> routes)
        //{
        //    id = routes.Count;
        //    routes.Add(this);           
        //}
        //public void UpdateInLocalRoutes(IList<Route> routes) => routes[id] = this;
        public RoutePremium CreateRoutePremium(Directions directions, double rate, int month, int year)
        {
            var start = new DateTime();
            var finish = new DateTime();
            var correcter = new CorrecterWorkDateTime();
            correcter.Correct(out start, out finish, month, year, this.start, this.finish);
            if (start == new DateTime() || finish == new DateTime()) return new RoutePremium();
            var trainsPremium = new TrainsPremium(start, finish, passangers, trains, stations);
            var serviceArea = new ServiceArea(stations.Select(station => station.ToString()).ToList(), directions);
            var pass = new Passangers(start, finish, passangers, locomotives);
            var passPremium = new PassangersPremium(pass);
            return new RoutePremium(start, finish, trainsPremium, new ServiceAreaPremium(serviceArea, ToWorkTime(month, year), pass.CalcWaitPassangersTime() + pass.CalcPassangersTime()), passPremium, pass, rate, this);
        }
        
        //public IDictionary<string, double> ToListOfTechnicalSpeed()
        //{
        //    var dict = new Dictionary<string, double>();
        //    if (stations.Count == 0) return dict;
        //    foreach(var train in trains)
        //    {
        //        dict.Add(train.ToString(), train.ToTechnicalSpeed(stations));
        //    }
        //    return dict;
        //}

    }
}
