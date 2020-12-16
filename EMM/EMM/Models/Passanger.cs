using EMM.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Passanger : IPassangerModel, IWorkTimeConvertable
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        private int numberOfTrain;
        [JsonProperty]
        private DateTime arravalTime;
        [JsonProperty]
        private DateTime depatureTime;
        public Passanger()
        {
            id = -1;
            arravalTime = Settings.DefaultDateTime;
            depatureTime = Settings.DefaultDateTime;
        }

        public Passanger(int id, int number, DateTime arraval, DateTime depature)
        {
            this.id = id;
            this.numberOfTrain = number;
            this.arravalTime = arraval;
            this.depatureTime = depature;
        }
        public Passanger(IEnumerable<Passanger> passangers)
        {
            id = -1;
            if(passangers.Count() == 0)
            {
                arravalTime = Settings.DefaultDateTime;
                depatureTime = Settings.DefaultDateTime;
            }
            else
            {
                arravalTime = passangers.First().arravalTime;
                depatureTime = passangers.Last().depatureTime;
            }
        }
        public TimeSpan ToTravelTime()
        {
            return depatureTime - arravalTime;
        }
        public bool IsEmpty()
        {
            return numberOfTrain == 0 && arravalTime == Settings.DefaultDateTime && depatureTime == Settings.DefaultDateTime;
        }
        public bool Check()
        {
            if (arravalTime > depatureTime) return false;
            return !(numberOfTrain == 0 || arravalTime == Settings.DefaultDateTime || depatureTime == Settings.DefaultDateTime);
        }
        public void Publish(Action<int, DateTime, DateTime> instructionsForMe)
        {
            instructionsForMe(numberOfTrain, arravalTime, depatureTime);
        }

        public TResult Publish<TResult>(Func<int, int, DateTime, DateTime, TResult> instructions)
        {
            return instructions(id, numberOfTrain, arravalTime, depatureTime);
        }

        public void Rebuild(Passanger passanger)
        {
            this.arravalTime = passanger.arravalTime;
            this.depatureTime = passanger.depatureTime;
            this.numberOfTrain = passanger.numberOfTrain;
        }
        public double ToPassangerTime()
        {
            return (depatureTime - arravalTime).TotalHours;
        }
        public double Difference(DateTime start)
        {
            return (depatureTime - start).TotalHours;
        }
        public IWorkTimeModel ToWorkTime()
        {
            return new WorkTimeModel(arravalTime, depatureTime, false);
        }
        public IWorkTimeModel ToWorkTime(DateTime start, DateTime finish)
        {
            var newArraval = arravalTime;
            var newDepature = depatureTime;
            if (newArraval < start) newArraval = start;
            if (newDepature < start) newDepature = start;
            if (newArraval > finish) newArraval = finish;
            if (newDepature > finish) newDepature = finish;
            return new WorkTimeModel(newArraval, newDepature, false);
        }


    }
}
