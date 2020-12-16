using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public abstract class TrainDecorator: Train
    {
        public TrainDecorator(Train train)
        {
            Rebuild(train);
        }
    }
}
