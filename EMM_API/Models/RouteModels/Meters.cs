using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EMM_API.Models.RouteModels
{
    [DataContract]
    public class Meters : IMetersModel
    {
        [DataMember]
        private int id;
        [DataMember]
        private int motorMeterAtInspection;
        [DataMember]
        private int motorMeterAtChange;
        [DataMember]
        private int brakeMeterAtInspection;
        [DataMember]
        private int brakeMeterAtChange;
        [DataMember]
        private int heatingMeterAtInspection;
        [DataMember]
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

    }
}
