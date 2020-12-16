using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class TrainsPremium : WorkTimesModel
    {
        //private DateTime start;
        //private readonly DateTime finish;
        //private readonly double rate;
        //private List<Train> trains;
        //private List<Station> stations;
        private List<TrainPremium> trains = new List<TrainPremium>();
        //private List<TrainPremium> order;
        //public TrainsPremium(DateTime start, DateTime finish, double rate, List<Train> trains, List<Station> stations)
        //{
        //    order = new List<TrainPremium>();
        //    trains.Sort(new TrainComparer(start, stations));
        //    var lastIndex = trains.Count - 1;
        //    var lasflag = false;
        //    var counter = 0;
        //    var previousStart = start;
        //    foreach(var train in trains)
        //    {
        //        if (counter == lastIndex) lasflag = true;
        //        counter++;
        //        var subStationer = train.CreateSubStationer(stations);
        //        var currentStations = subStationer.CutStations();
        //        if (currentStations.Count() == 0) continue;
        //        TrainPremium current;
        //        if (lasflag) current = train.ToTrainPremium(previousStart, finish);
        //        else current = train.ToTrainPremium(previousStart, currentStations);
        //        order.Add(current);
        //        if (lasflag) break;
        //        previousStart = current.Finish;
        //    }
        //    //this.start = start;
        //    //this.finish = finish;
        //    this.rate = rate;
        //    //this.trains = trains;
        //    //this.stations = stations;
        //}

        public TrainsPremium(DateTime start, DateTime finish, List<Passanger> passangers, List<Train> trains, List<Station> stations) : base(start, finish, passangers, trains, stations)
        {
        }
        protected override void AddTime(IWorkTimeModel timeModel, TimeSpan time, int counter, int lastindex)
        {
            base.AddTime(timeModel, time, counter, lastindex);
            if(timeModel.IsWork())
            {
                var trainWorktime = timeModel as TrainWorkTimeModel;
                if(trainWorktime != null)
                {
                    trains.Add(trainWorktime.CreateTrainPremium(time));
                }
            }
        }
        public double CalcHeavyWeight(double rate)
        {
            return trains.Select(trainWorkTime => trainWorkTime.CalcHeavyRate(rate)).Sum();
        }
        public double CalcLongLength(double rate)
        {
            return trains.Select(trainWorkTime => trainWorkTime.CalcLongRate(rate)).Sum();
        }
        public double CalcFirstConnect(double rate)
        {
            return trains.Select(trainWorkTime => trainWorkTime.CalcFirstConnect(rate)).Sum();
        }
        public double CalcLastConnect(double rate)
        {
            return trains.Select(trainWorkTime => trainWorkTime.CalcLastConnect(rate)).Sum();
        }



    }
}
