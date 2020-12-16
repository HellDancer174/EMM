using EMM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.ViewModels
{
    public class PassangerVM
    {
        public int NumberOfTrain { get; set; }
        private DateTime arravalDate;
        private DateTime depatureDate;
        public TimeSpan ArravalTime { get; set; }
        public TimeSpan DepatureTime { get; set; }
        private IPassangerModel passangerModel;
        //public PassangerVM()
        //{

        //}
        public PassangerVM(IPassangerModel passangerModel)
        {
            this.passangerModel = passangerModel;
            this.passangerModel.Publish((number, arraval, depature) =>
            {
                this.NumberOfTrain = number;
                this.ArravalTime = arraval.TimeOfDay;
                this.DepatureTime = depature.TimeOfDay;
            });
        }

        public void GetDatesOfArravalAndDepature(DateTime startDate, DateTime finishDate, TimeSpan start, TimeSpan finish)
        {
            if (start > this.ArravalTime) this.arravalDate = finishDate.Date;
            else this.arravalDate = startDate.Date;
            if (this.DepatureTime > finish) this.depatureDate = startDate.Date;
            else this.depatureDate = finishDate.Date;
        }

        public Passanger ToPassanger(DateTime startDate, DateTime finishDate, TimeSpan start, TimeSpan finish)
        {
            RebuildModel(startDate, finishDate, start, finish);
            return (Passanger)passangerModel;
        }

        public void RebuildModel(DateTime startDate, DateTime finishDate, TimeSpan start, TimeSpan finish)
        {
            GetDatesOfArravalAndDepature(startDate, finishDate, start, finish);
            DateTime arraval, depature;
            if (this.ArravalTime > this.DepatureTime)
            {
                arraval = arravalDate.Date + this.ArravalTime;
                depature = depatureDate.Date + this.DepatureTime;
            }
            else
            {
                arraval = arravalDate.Date + this.ArravalTime;
                depature = arravalDate.Date + this.DepatureTime;
            }

            passangerModel.Rebuild(new Passanger(-1, NumberOfTrain, arraval, depature));
        }
    }
}
