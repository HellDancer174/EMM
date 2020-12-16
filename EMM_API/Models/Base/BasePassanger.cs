using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class BasePassanger
    {
        protected int id;
        protected int numberOfTrain;
        protected DateTime arravalTime;
        protected DateTime depatureTime;
        public BasePassanger()
        {

        }
        public BasePassanger(int id, int number, DateTime arraval, DateTime depature)
        {
            this.id = id;
            this.numberOfTrain = number;
            this.arravalTime = arraval;
            this.depatureTime = depature;
        }
        public void ToAway(Action<int, int, DateTime, DateTime> instructionsForMe)
        {
            instructionsForMe(id, numberOfTrain, arravalTime, depatureTime);
        }

        public TResult ToAway<TResult>(Func<int, int, DateTime, DateTime, TResult> instructions)
        {
            return instructions(id, numberOfTrain, arravalTime, depatureTime);
        }

    }
}