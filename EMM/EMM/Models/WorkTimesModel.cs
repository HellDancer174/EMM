using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public abstract class WorkTimesModel
    {
        protected readonly DateTime start;
        protected readonly DateTime finish;
        protected List<IWorkTimeModel> unionWorkTime;
        protected TimeSpan workTime;
        protected TimeSpan nonWorkTime;
        protected bool firstPassanger;
        protected bool lastPassanger;

        public WorkTimesModel(DateTime start, DateTime finish, List<Passanger> passangers, List<Locomotive> locomotives)
        {
            var passWorkTime = passangers.Select(pass => pass.ToWorkTime(start, finish)).ToList();
            var locWorkTime = locomotives.Select(loc => loc.ToWorkTime(start, finish)).ToList();
            unionWorkTime = passWorkTime.Union(locWorkTime).ToList();
            Initialize(start, finish);
            this.start = start;
            this.finish = finish;
        }

        private void Initialize(DateTime start, DateTime finish)
        {
            unionWorkTime.Sort(new WorkTimeModelComparer(start));
            if (unionWorkTime.Count != 0)
            {
                var previous = start;
                workTime = new TimeSpan();
                nonWorkTime = new TimeSpan();
                var counter = 0;
                var lastIndex = unionWorkTime.Count - 1;
                foreach (var timeModel in unionWorkTime)
                {
                    TimeSpan time = new TimeSpan();
                    if (counter == lastIndex) time = finish - previous;
                    else time = timeModel.CalcTime(previous);
                    AddTime(timeModel, time, counter, lastIndex);
                    previous += time;
                    counter++;
                }
            }
        }

        protected virtual void AddTime(IWorkTimeModel timeModel, TimeSpan time, int counter, int lastIndex)
        {
            if (timeModel.IsWork() == true) workTime += time;
            else
            {
                nonWorkTime += time;
                if (counter == 0) firstPassanger = true;
                if (counter == lastIndex) lastPassanger = true;
            }
        }

        public WorkTimesModel(DateTime start, DateTime finish, List<Passanger> passangers, List<Train> trains, List<Station> stations)
        {
            var passTime = passangers.Select(pass => pass.ToWorkTime()).ToList();
            var trainTime = trains.Select(train => train.ToWorkTime(stations)).ToList();
            unionWorkTime = passTime.Union(trainTime).ToList();
            Initialize(start, finish);
            this.start = start;
            this.finish = finish;
        }
    }
}
