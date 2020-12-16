using EMM.Helpers;
using EMM.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BackgroundTime : IBackgroundTimeModel, IWorkTimeConvertable
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        private DateTime inspection;
        [JsonProperty]
        private DateTime cPExit;
        [JsonProperty]
        private DateTime cPEntrance;
        [JsonProperty]
        private DateTime change;
        public BackgroundTime()
        {
            inspection = Settings.DefaultDateTime;
            cPExit = Settings.DefaultDateTime;
            cPEntrance = Settings.DefaultDateTime;
            change = Settings.DefaultDateTime;
        }
        public BackgroundTime(int id, DateTime inspection, DateTime cPExit, DateTime cPEntrance, DateTime change)
        {
            this.id = id;
            this.inspection = inspection;
            this.cPExit = cPExit;
            this.cPEntrance = cPEntrance;
            this.change = change;
        }
        public bool IsEmpty()
        {
            return inspection == Settings.DefaultDateTime && cPExit == Settings.DefaultDateTime && cPEntrance == Settings.DefaultDateTime && change == Settings.DefaultDateTime;
        }
        public TimeSpan ToLocomotiveWorkTime()
        {
            return change - inspection;
        }
        public bool Check()
        {
            if (inspection == Settings.DefaultDateTime || change == Settings.DefaultDateTime) return false;
            if (this.IsEmpty()) return false;
            if (cPExit != Settings.DefaultDateTime && inspection > cPExit) return false;
            if (cPEntrance != Settings.DefaultDateTime && cPEntrance > change) return false;
            if (inspection >= change) return false;
            return true;
        }
        public void Publish(Action<DateTime, DateTime, DateTime, DateTime> instructions)
        {
            instructions(inspection, cPExit, cPEntrance, change);
        }
        public TResult Publish<TResult>(Func<int, DateTime, DateTime, DateTime, DateTime, TResult> instructions)
        {
            return instructions(id, inspection, cPExit, cPEntrance, change);
        }

        public void Rebuild(BackgroundTime time)
        {
            this.inspection = time.inspection;
            this.cPExit = time.cPExit;
            this.cPEntrance = time.cPEntrance;
            this.change = time.change;
        }
        public double Difference(DateTime start)
        {
            return (change - start).TotalHours;
        }
        public IWorkTimeModel ToWorkTime()
        {
            return new WorkTimeModel(inspection, change, true);
        }
        public WorkTimeModel ToWorkTime(DateTime start, DateTime finish)
        {
            var newInspection = inspection;
            var newChange = change;
            if (newInspection < start) newInspection = start;
            if (newChange < start) newChange = start;
            if (newInspection > finish) newInspection = finish;
            if (newChange > finish) newChange = finish;
            return new WorkTimeModel(newInspection, newChange, true);
        }
    }
}
