using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace EMM_API.Models.RouteModels
{
    [DataContract]
    public class Station : IStationModel
    {
        [DataMember]
        private int id;
        [DataMember]
        private string name;
        [DataMember]
        private DateTime depatureTime;
        [DataMember]
        private DateTime arravalTime;
        [DataMember]
        private TimeSpan maneuvers;
        public Station()
        {
            id = -1;
            depatureTime = new DateTime(1837, 11, 11, 00, 00, 00);
            arravalTime = new DateTime(1837, 11, 11, 00, 00, 00);
        }
        public Station(int id, string name, DateTime depature, DateTime arraval, TimeSpan maneuvers)
        {
            this.id = id;
            this.name = name;
            this.depatureTime = depature;
            this.arravalTime = arraval;
            this.maneuvers = maneuvers;
        }

        public TimeSpan GetDownTime()
        {
            var goodArraval = arravalTime;
            if (depatureTime > goodArraval) goodArraval += new TimeSpan(24, 0, 0);
            return goodArraval - depatureTime;
        }
        public TimeSpan ToManeuverTime()
        {
            return maneuvers;
        }
        public bool OnlyMaeuvers()
        {
            return !String.IsNullOrEmpty(name) && depatureTime == default(DateTime) && arravalTime == default(DateTime) && maneuvers != default(TimeSpan);
        }
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(name) && depatureTime == default(DateTime) && arravalTime == default(DateTime) && maneuvers == default(TimeSpan);
        }
        public bool CheckExpectFirstAndLastStation()
        {
            return !(depatureTime == default(DateTime) || arravalTime == default(DateTime));
        }
        public bool CheckFirstStation()
        {
            return (depatureTime == default(DateTime) && arravalTime != default(DateTime));
        }
        public bool CheckLastStation()
        {
            return (depatureTime != default(DateTime) && arravalTime == default(DateTime));
        }
        public bool Check()//Доделать
        {
            //id!=0
            var goodArraval = arravalTime;
            if (depatureTime > goodArraval) goodArraval += new TimeSpan(24, 0, 0);
            if (goodArraval - depatureTime > new TimeSpan(12, 0, 0)) return false;
            if (String.IsNullOrEmpty(name) || (depatureTime == default(DateTime) && arravalTime == default(DateTime) && maneuvers == default(TimeSpan))) return false;
            if ((depatureTime != default(DateTime) && arravalTime != default(DateTime)) && maneuvers > arravalTime - depatureTime) return false;
            else return true;
        }

        public override string ToString()
        {
            return name;
        }

        public void Publish(Action<int, string, DateTime, DateTime, TimeSpan> instructionsForMe)
        {
            instructionsForMe(id, name, depatureTime, arravalTime, maneuvers);
        }
        public TResult Publish<TResult>(Func<int, string, DateTime, DateTime, TimeSpan, TResult> instructions)
        {
            return instructions(id, name, depatureTime, arravalTime, maneuvers);
        }

        public void Rebuild(Station station)
        {
            this.depatureTime = station.depatureTime;
            this.arravalTime = station.arravalTime;
            this.maneuvers = station.maneuvers;
            this.name = station.name;
        }
    }
}
