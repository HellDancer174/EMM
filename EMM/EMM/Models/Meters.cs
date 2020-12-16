using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EMM.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Meters : IMetersModel
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        private int motorMeterAtInspection;
        [JsonProperty]
        private int motorMeterAtChange;
        [JsonProperty]
        private int brakeMeterAtInspection;
        [JsonProperty]
        private int brakeMeterAtChange;
        [JsonProperty]
        private int heatingMeterAtInspection;
        [JsonProperty]
        private int heatingMeterAtChange;

        public Meters()
        {
            id = -1;
            motorMeterAtInspection = 0;
            motorMeterAtChange = 0;
            brakeMeterAtInspection = 0;
            brakeMeterAtChange = 0;
            heatingMeterAtInspection = 0;
            heatingMeterAtChange = 0;
        }

        public Meters(int id, int motorInspection, int motorChange, int brakeIncspection, int brakeChange, int heatingInspection, int heatingChange)
        {
            this.id = id;
            this.motorMeterAtInspection = motorInspection;
            this.motorMeterAtChange = motorChange;
            this.brakeMeterAtInspection = brakeIncspection;
            this.brakeMeterAtChange = brakeChange;
            this.heatingMeterAtInspection = heatingInspection;
            this.heatingMeterAtChange = heatingChange;
        }
        public bool Check()
        {
            return motorMeterAtChange >= motorMeterAtInspection && brakeMeterAtChange >= brakeMeterAtInspection && heatingMeterAtChange >= heatingMeterAtInspection;
        }

        public void Rebuild(Meters meters)
        {
            this.motorMeterAtInspection = meters.motorMeterAtInspection;
            this.motorMeterAtChange = meters.motorMeterAtChange;
            this.brakeMeterAtInspection = meters.brakeMeterAtInspection;
            this.brakeMeterAtChange = meters.brakeMeterAtChange;
            this.heatingMeterAtInspection = meters.heatingMeterAtInspection;
            this.heatingMeterAtChange = meters.heatingMeterAtChange;
        }

        public void Publish(Action<int, int, int, int, int, int, int> instructionsForMe)
        {
            instructionsForMe(id, motorMeterAtInspection, motorMeterAtChange, brakeMeterAtInspection, brakeMeterAtChange, heatingMeterAtInspection, heatingMeterAtChange);
        }

        public TResult Publish<TResult>(Func<int, int, int, int, int, int, int, TResult> instructions)
        {
            return instructions(id, motorMeterAtInspection, motorMeterAtChange, brakeMeterAtInspection, brakeMeterAtChange, heatingMeterAtInspection, heatingMeterAtChange);
        }

        public double ToCost(string locomotiveType)
        {
            var motor = motorMeterAtChange - motorMeterAtInspection;
            var brake = brakeMeterAtChange - brakeMeterAtInspection;
            if (locomotiveType == "2ЭС6"|| locomotiveType == "2*2ЭС6") return (motor - brake)/10.0;
            else if(locomotiveType == "ЭП2К"|| locomotiveType == "ЧС2К") return (motor - brake) / 100.0;
            else return motor - brake;
        }

    }
}
