using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class BaseMeters
    {
        protected int id;
        protected int motorMeterAtInspection;
        protected int motorMeterAtChange;
        protected int brakeMeterAtInspection;
        protected int brakeMeterAtChange;
        protected int heatingMeterAtInspection;
        protected int heatingMeterAtChange;
        public BaseMeters(int id, int motorInspection, int motorChange, int brakeIncspection, int brakeChange, int heatingInspection, int heatingChange)
        {
            this.id = id;
            this.motorMeterAtInspection = motorInspection;
            this.motorMeterAtChange = motorChange;
            this.brakeMeterAtInspection = brakeIncspection;
            this.brakeMeterAtChange = brakeChange;
            this.heatingMeterAtInspection = heatingInspection;
            this.heatingMeterAtChange = heatingChange;
        }
        public void ToAway(Action<int,int, int, int, int, int, int> instructionsForMe)
        {
            instructionsForMe(id, motorMeterAtInspection, motorMeterAtChange, brakeMeterAtInspection, brakeMeterAtChange, heatingMeterAtInspection, heatingMeterAtChange);
        }

        public TResult ToAway<TResult>(Func<int, int, int, int, int, int, int, TResult> instructions)
        {
            return instructions(id, motorMeterAtInspection, motorMeterAtChange, brakeMeterAtInspection, brakeMeterAtChange, heatingMeterAtInspection, heatingMeterAtChange);
        }

    }
}