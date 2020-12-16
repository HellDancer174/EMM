using EMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.ViewModels
{
    public class WorkTime : RouteCellVM
    {
        //private IRouteModel model;
        //public DateTime Date { get; set; }
        //public string Direction { get; set; }
        private TimeSpan hours;
        public string Hours
        {
            get
            {
                return "Рабочее время: " + hours.ToString();
            }
        }
        public WorkTime(IRouteModel model):base(model)
        {
        }
        public override void Rebuild()
        {
            model.Publish((worksStart, worksFinish, locomotives, passangers, trains, stations, comment, single) =>
            {
                Date = worksStart.Date;
                if (stations.Count() == 0) Direction = "Отсутствует направление";
                else Direction = stations.FirstOrDefault().ToString() + " - " + stations.LastOrDefault().ToString();
                hours = worksFinish - worksStart;
            });

        }
        public TimeSpan ToTimeSpan()
        {
            return hours;
        }
    }
}
