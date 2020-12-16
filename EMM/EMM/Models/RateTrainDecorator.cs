using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class RateTrainDecorator : TrainDecorator
    {
        public RateTrainDecorator(Train train) : base(train)
        {
        }
        public bool IsRailLubricator()
        {
            return number > 4779 && number < 4798;
        }
        public TimeSpan CalcLocomotivesServiceTime(RateTrainDecorator previous)
        {
            if (previous.locomotive.ToString() == locomotive.ToString()) return new TimeSpan();
            var workTime = locomotive.ToWorkTime();
            return workTime.CalcTime();
        }
        public bool IsLocalFreight()
        {
            return number > 3001 && number < 3998;
        }
    }
}
