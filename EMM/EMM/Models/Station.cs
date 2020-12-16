using EMM.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EMM.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Station : IStationModel
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        private string name;
        [JsonProperty]
        private DateTime depatureTime;
        [JsonProperty]
        private DateTime arravalTime;
        [JsonProperty]
        private TimeSpan maneuvers;

        public Station()
        {
            id = -1;
            depatureTime = Settings.DefaultDateTime;
            arravalTime = Settings.DefaultDateTime;
        }
        public Station(int id, string name, DateTime depature, DateTime arraval, TimeSpan maneuvers)
        {
            this.id = id;
            this.name = name;
            this.depatureTime = depature;
            this.arravalTime = arraval;
            this.maneuvers = maneuvers;
        }
        public TimeSpan GetFullTime(Station first) => this.depatureTime - first.arravalTime;
        public TimeSpan GetFullTime(DateTime start) => this.depatureTime - start;

        public TimeSpan GetDownTime()
        {
            if (CheckFirstStation() || CheckLastStation()) return default(TimeSpan);
            return arravalTime - depatureTime;
        }
        public TimeSpan ToManeuverTime()
        {
            return maneuvers;
        }
        public bool HasMaeuvers()
        {
            return maneuvers != default(TimeSpan);
        }
        public bool OnlyMaeuvers()
        {
            return !String.IsNullOrEmpty(name) && depatureTime == Settings.DefaultDateTime && arravalTime == Settings.DefaultDateTime && maneuvers != default(TimeSpan);
        }
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(name) && depatureTime == Settings.DefaultDateTime && arravalTime == Settings.DefaultDateTime && maneuvers == default(TimeSpan);
        }
        public bool CheckExpectFirstAndLastStation()
        {
            return !(depatureTime == Settings.DefaultDateTime || arravalTime == Settings.DefaultDateTime);
        }
        public bool CheckFirstStation()
        {
            return (depatureTime == Settings.DefaultDateTime && arravalTime != Settings.DefaultDateTime);
        }
        public bool CheckLastStation()
        {
            return (depatureTime != Settings.DefaultDateTime && arravalTime == Settings.DefaultDateTime);
        }
        public bool Check()//Доделать
        {
            if (depatureTime > arravalTime&& arravalTime!=Settings.DefaultDateTime) return false;
            if (arravalTime - depatureTime > new TimeSpan(12, 0, 0)&&depatureTime!=Settings.DefaultDateTime) return false;
            if (String.IsNullOrEmpty(name) || (depatureTime == Settings.DefaultDateTime && arravalTime == Settings.DefaultDateTime && maneuvers == default(TimeSpan))) return false;
            if ((depatureTime != Settings.DefaultDateTime && arravalTime != Settings.DefaultDateTime) && maneuvers > arravalTime - depatureTime) return false;
            else return true;
        }

        public override string ToString() => name;

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
        public double Difference(DateTime start)
        {
            return (arravalTime - start).TotalHours;
        }
        public IWorkTimeModel ToWorkTime(Station last, int length, string type, int number)
        {
            return new TrainWorkTimeModel(arravalTime, last.depatureTime, true, length, type, number);
        }
     }
}
