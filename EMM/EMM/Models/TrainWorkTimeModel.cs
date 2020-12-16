using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class TrainWorkTimeModel: WorkTimeModel, IWorkTimeModel
    {
        private readonly int length;
        private readonly string type;
        private readonly int number;

        public TrainWorkTimeModel(DateTime start, DateTime finish, bool isWork, int length, string type, int number) : base(start, finish, isWork)
        {
            this.length = length;
            this.type = type;
            this.number = number;
        }
        public TrainPremium CreateTrainPremium(TimeSpan workTime)
        {
            return new TrainPremium(number, type, length, workTime);
        }


    }
}
