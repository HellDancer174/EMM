using EMM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.ViewModels
{
    public class LocomotiveVM
    {
        private string name;
        public string Type { get; set; }
        public string Number { get; set; }
        public TimeSpan Inspection { get; set; }
        public TimeSpan CPExit { get; set; }
        public TimeSpan CPEntrance { get; set; }
        public TimeSpan Change { get; set; }
        public int SectionCount;
        public string Name { get => Type+"-"+Number; set => name = value; }
        private DateTime dateInspection;
        private DateTime dateChange;
        private ILocomotiveModel model;
        private IBackgroundTimeModel backgroundTime;
       
        //public LocomotiveVM(string type, string number, int sectionCount)
        //{
        //    this.Type = type;
        //    this.Number = number;
        //    this.SectionCount = sectionCount;
        //}
        public LocomotiveVM(ILocomotiveModel locomotiveModel)
        {
            this.model = locomotiveModel;
            model.Publish((type, number, times, meters, sections) =>
            {
                this.Type = type;
                this.Number = number;
                this.SectionCount = sections;
                if (times == null) this.backgroundTime = new BackgroundTime();
                else this.backgroundTime = times;
                times.Publish((inspection, cpExit, cpEntrance, change) =>
                {
                    this.Inspection = inspection.TimeOfDay;
                    this.CPExit = cpExit.TimeOfDay;
                    this.CPEntrance = cpEntrance.TimeOfDay;
                    this.Change = change.TimeOfDay;
                });

            });

        }
        public void GetDatesOfInspectionAndChange(DateTime startDate, DateTime finishDate, TimeSpan start, TimeSpan finish)
        {
            if (start > this.Inspection) this.dateInspection = finishDate;
            else this.dateInspection = startDate.Date;
            if (this.Change > finish) this.dateChange = startDate.Date;
            else this.dateChange = finishDate.Date;
        }
        public void RebuildModel(IEnumerable<Meters> meters, DateTime startDate, DateTime finishDate, TimeSpan start, TimeSpan finish)
        {
            GetDatesOfInspectionAndChange(startDate, finishDate, start, finish);
            DateTime inspection, cpExit, cpEntrance, change;
            inspection = dateInspection.Date + this.Inspection;
            change = dateChange.Date + this.Change;
            if(this.CPExit==default(TimeSpan)&&this.CPEntrance==default(TimeSpan))
            {
                cpExit = new DateTime(1837, 11, 11, 00, 00, 00);
                cpEntrance = new DateTime(1837, 11, 11, 00, 00, 00);
            }
            else if(CPExit==default(TimeSpan)&&CPEntrance!=default(TimeSpan))
            {
                cpExit = new DateTime(1837, 11, 11, 00, 00, 00);
                if (this.CPEntrance > this.Change) cpEntrance = dateInspection.Date + this.CPEntrance;
                else cpEntrance = dateChange.Date + this.CPEntrance;
            }
            else if(CPExit != default(TimeSpan) && CPEntrance == default(TimeSpan))
            {
                cpEntrance = new DateTime(1837, 11, 11, 00, 00, 00);
                if (this.Inspection > this.CPExit) cpExit = dateChange.Date + this.CPExit;
                else cpExit = dateInspection.Date + this.CPExit;
            }
            else
            {
                if (this.CPEntrance > this.Change) cpEntrance = dateInspection.Date + this.CPEntrance;
                else cpEntrance = dateChange.Date + this.CPEntrance;
                if (this.Inspection > this.CPExit) cpExit = dateChange.Date + this.CPExit;
                else cpExit = dateInspection.Date + this.CPExit;
            }
            backgroundTime.Rebuild(new BackgroundTime(-1, inspection, cpExit, cpEntrance, change));
            model.Rebuild(new Locomotive(this.Type, this.Number, (BackgroundTime)backgroundTime, meters, this.SectionCount));
        }
        public Locomotive ToLocomotive(IEnumerable<Meters> meters, DateTime startDate, DateTime finishDate, TimeSpan start, TimeSpan finish)
        {
            RebuildModel(meters, startDate, finishDate, start, finish);
            return (Locomotive)model;

        }
    }
}
