using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EMM_API.Models.RouteModels
{
    [DataContract]
    public class BackgroundTime : IBackgroundTimeModel
    {
        [DataMember]
        private int id;
        [DataMember]
        private DateTime inspection;
        [DataMember]
        private DateTime cPExit;
        [DataMember]
        private DateTime cPEntrance;
        [DataMember]
        private DateTime change;
        public BackgroundTime()
        {
            inspection = new DateTime(1837, 11, 11, 00, 00, 00);
            cPExit = new DateTime(1837, 11, 11, 00, 00, 00);
            cPEntrance = new DateTime(1837, 11, 11, 00, 00, 00);
            change = new DateTime(1837, 11, 11, 00, 00, 00);
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
            return inspection == default(DateTime) && cPExit == default(DateTime) && cPEntrance == default(DateTime) && change == default(DateTime);
        }
        public TimeSpan ToLocomotiveWorkTime()
        {
            return change - inspection;
        }
        public bool Checked()
        {
            if (inspection == default(DateTime) || change == default(DateTime)) return false;
            if (this.IsEmpty()) return false;
            if (cPExit != default(DateTime) && inspection > cPExit) return false;
            if (cPEntrance != default(DateTime) && cPEntrance > change) return false;
            if (inspection > change) return false;
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
    }
}
