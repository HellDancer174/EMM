using LifeHacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EMM_API.Models.RouteModels
{
    [DataContract]
    public class Locomotive : ILocomotiveModel
    {
        [DataMember]
        private string type;
        [DataMember]
        private string number;
        [DataMember]
        private BackgroundTime times;
        [DataMember]
        private List<Meters> meters;
        [DataMember]
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
            return !(String.IsNullOrEmpty(type) || String.IsNullOrEmpty(number) || times == null || meters == null || meters.Count == 0 || sectionCount == 0) && times.Checked() && !checkedMeters.Contains(false);
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
    }
}
