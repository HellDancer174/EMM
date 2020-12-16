using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EMM_API.Models.RouteModels
{
    [DataContract]
    public class Passanger : IPassangerModel
    {
        [DataMember]
        private int id;
        [DataMember]
        private int numberOfTrain;
        [DataMember]
        private DateTime arravalTime;
        [DataMember]
        private DateTime depatureTime;
        public Passanger()
        {
            arravalTime = new DateTime(1837, 11, 11, 00, 00, 00);
            depatureTime = new DateTime(1837, 11, 11, 00, 00, 00);
        }
        public Passanger(int id, int number, DateTime arraval, DateTime depature)
        {
            this.id = id;
            this.numberOfTrain = number;
            this.arravalTime = arraval;
            this.depatureTime = depature;
        }
        public TimeSpan ToTravelTime()
        {
            var goodDepatureTime = depatureTime;
            if (arravalTime < goodDepatureTime) goodDepatureTime += new TimeSpan(24, 0, 0);
            return goodDepatureTime - arravalTime;
        }
        public bool IsEmpty()
        {
            return numberOfTrain == 0 && arravalTime == default(DateTime) && depatureTime == default(DateTime);
        }
        public bool Check()
        {
            if (arravalTime > depatureTime) return false;
            return numberOfTrain == 0 || arravalTime == default(DateTime) || depatureTime == default(DateTime);
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
    }
}
