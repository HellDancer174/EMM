using EMM.ViewModels;
using LifeHacks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Locomotive : ILocomotiveModel, IWorkTimeConvertable
    {
        [JsonProperty]
        private string type;
        [JsonProperty]
        private string number;
        [JsonProperty]
        private BackgroundTime times;
        [JsonProperty]
        private List<Meters> meters;
        [JsonProperty]
        private int sectionCount;

        public Locomotive()
        {
            this.times = new BackgroundTime();

        }

        public Locomotive(string type, string number, BackgroundTime times, IEnumerable<Meters> meters, int sectionCount)
        {
            this.type = type;
            this.number = number;
            this.times = times;
            this.meters = new List<Meters>(meters);
            this.sectionCount = sectionCount;
        }
        public TimeSpan GetLocomotiveWorkTime()
        {
            return times.ToLocomotiveWorkTime();
        }
        public bool Check()
        {
            var checkedMeters = meters.Select(meter => meter.Check());
            //id!=0
            return !(String.IsNullOrEmpty(type) || String.IsNullOrEmpty(number) || times == null || meters == null || meters.Count == 0 || sectionCount == 0) && times.Check() && !checkedMeters.Contains(false);
        }
        public void Publish(Action<string, string, BackgroundTime, IEnumerable<Meters>, int> instructionsForMe)
        {
            instructionsForMe(type, number, times, meters, sectionCount);
        }
        public TResult Publish<TResult>(Func<string, string, BackgroundTime, IEnumerable<Meters>, int, TResult> instructions)
        {
            return instructions(type, number, times, meters, sectionCount);
        }

        public override string ToString()
        {
            return type + "-" + number;
        }

        public void Rebuild(Locomotive locomotive)
        {
            this.type = locomotive.type;
            this.number = locomotive.number;
            this.times = locomotive.times;
            this.meters = locomotive.meters;
            this.sectionCount = locomotive.sectionCount;
        }

        public double ToCostFuelEnergy()
        {
            return meters.Select(meter => meter.ToCost(type)).Sum();
        }
        public double Difference(DateTime start)
        {
            return times.Difference(start);
        }
        public IWorkTimeModel ToWorkTime()
        {
            return times.ToWorkTime();
        }
        public IWorkTimeModel ToWorkTime(DateTime start, DateTime finish)
        {
            return times.ToWorkTime(start, finish);
        }


    }
}
