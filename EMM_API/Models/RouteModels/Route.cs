using LifeHacks;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Runtime.Serialization;

namespace EMM_API.Models.RouteModels
{
    [DataContract]
    public class Route : IEquatable<Route>, IEquatable<StringFromRoute>, IRouteModel
    {
        [DataMember]
        private int id;
        [DataMember]
        private DateTime start;
        [DataMember]
        private DateTime finish;
        [DataMember]
        private List<Locomotive> locomotives;
        [DataMember]
        private List<Train> trains;
        [DataMember]
        private List<Station> stations;
        [DataMember]
        private List<Passanger> passangers;
        [DataMember]
        private string comment;
        [DataMember]
        private bool single;

        public Route()
        {
            this.trains = new List<Train>();
            this.stations = new List<Station>();
            this.passangers = new List<Passanger>();
            this.id = -1;
            start = DateTime.Today;
            finish = DateTime.Today;
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
        public bool Check()//Доделать
        {
            if (this.start == default(DateTime) || this.finish == default(DateTime)) return false;
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
            if (this.id == other.id) return true;
            else return false;
        }

        public bool Equals(StringFromRoute other)
        {
            if (this.id == other.id) return true;
            return false;
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
        public TResult Publish<TResult>(Func<int, DateTime, DateTime, IEnumerable<Locomotive>, IEnumerable<Passanger>, IEnumerable<Train>, IEnumerable<Station>, string, bool,TResult> instructions)
        {
            return instructions(id, start, finish, locomotives, passangers, trains, stations, comment, single);
        }

    }
}
