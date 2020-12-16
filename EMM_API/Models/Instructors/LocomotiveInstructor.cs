using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMM_API.Models.RouteModels;
using LifeHacks;

namespace EMM_API.Models.Instructors
{
    public class LocomotiveInstructor : DBInstructor
    {
        private IEnumerable<Locomotive> locomotives;
        private List<string> deletingType;
        private List<string> deletingNumber;

        public LocomotiveInstructor(EmmDataContext context, Routes_TT route, IEnumerable<Locomotive> locomotives)
        {
            this.context = context;
            this.route = route;
            this.routeID = route.id;
            this.locomotives = locomotives;
            deletingType = new List<string>();
            deletingNumber = new List<string>();
            deletingType.AddRange(route.Locomotives_TT.Select(loc => loc.locomotiveType).ToList());
            deletingNumber.AddRange(route.Locomotives_TT.Select(loc => loc.locomotiveNumber).ToList());
        }

        public bool CreateLocomotive(string type, string number, BackgroundTime backgroundTime, IEnumerable<Meters> meters, int sectionCount)
        {
            if (context.Locomitive_TS.SingleOrDefault(loc => loc.mk == type && loc.sectionCount == sectionCount) == null) return false;
            Locomotives_TT newLocomotive = ToNewLocomotiveTT(type, number, backgroundTime, meters, sectionCount);
            var backgroundTimeID = backgroundTime.Publish(CreateBackgroundTime);
            if (backgroundTimeID == 0) return false;
            newLocomotive.backgroundTimeID = backgroundTimeID;
            context.Locomotives_TT.InsertOnSubmit(newLocomotive);
            CreateMeters(meters, type, number);
            return true;
        }

        private Locomotives_TT ToNewLocomotiveTT(string type, string number, BackgroundTime backgroundTime, IEnumerable<Meters> meters, int sectionCount)
        {
            return new Locomotives_TT() { locomotiveType = type, locomotiveNumber = number, backgroundTimeID = 0, routeID = routeID };
        }
        public bool CreateLocomotives(IEnumerable<Locomotive> locomotives)
        {
            var list = locomotives.Select(loc => loc.Publish(CreateLocomotive)).ToList();
            if (list.Contains(false)) return false;
            else return true;
        }
        public bool UpdateLocomotives()
        {
            //if (deletingType.Count == 0 || deletingNumber.Count == 0) return false;
            var flag =  !locomotives.Select(loc => loc.Publish(UpdateLocomotive)).ToList().Contains(false);
            FinalUpdateLocomotive();
            return flag;
        }
        public bool UpdateLocomotive(string type, string number, BackgroundTime backgroundTime, IEnumerable<Meters> meters, int sectionCount)
        {
            var necessaryLocomotive = route.Locomotives_TT.SingleOrDefault(loc => loc.locomotiveType == type && loc.locomotiveNumber == number);
            updateCount++;
            if (necessaryLocomotive == null) return CreateLocomotive(type, number, backgroundTime, meters, sectionCount);
            necessaryLocomotive.locomotiveType = type;
            necessaryLocomotive.locomotiveNumber = number;
            if (backgroundTime.Publish(UpdateBackgroundTime)==false) return false;
            if (UpdateMeters(meters) == false) return false;
            if(deletingType.Count>0) deletingType.Remove(type);
            if(deletingNumber.Count>0) deletingNumber.Remove(number);
            return true;

        }
        private bool UpdateBackgroundTime(int id, DateTime inspection, DateTime cPExit, DateTime cPEntrance, DateTime change)
        {
            var necessaryTime = context.BackgroundTime_TT.SingleOrDefault(time => time.id == id);
            if (necessaryTime == null) return false;
            necessaryTime.inspection = inspection;
            necessaryTime.cpExit = cPExit;
            necessaryTime.cpEntrance = cPEntrance;
            necessaryTime.change = change;
            return true;
        }
        
        private int CreateBackgroundTime(int id, DateTime inspection, DateTime cPExit, DateTime cPEntrance, DateTime change) //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            int? backgroundTimeID = 0;
            context.InsertBackgroundTime(ref backgroundTimeID, inspection, cPExit, cPEntrance, change);
            return backgroundTimeID.GetValueOrDefault();
        }
        private bool UpdateMeters(IEnumerable<Meters> meters)
        {
           return !meters.Select(meter => meter.Publish(UpdateMeterTable)).ToList().Contains(false);
        }
        private void CreateMeters(IEnumerable<Meters> meters, string type, string number)
        {
            var newMeters = meters.Select(meter =>
            {
                var temp = meter.Publish(MeterToNewTable);
                temp.locomotiveType = type;
                temp.locomotiveNumber = number;
                temp.routeID = routeID;
                return temp;
            });
            context.Meters_TT.InsertAllOnSubmit(newMeters);
        }
        private bool UpdateMeterTable(int id, int motorInspection, int motorChange, int brakeIncspection, int brakeChange, int heatingInspection, int heatingChange)
        {
            var necessaryMeter = context.Meters_TT.SingleOrDefault(meter => meter.id == id);
            if (necessaryMeter == null) return false;
            necessaryMeter.motorInspection = motorInspection;
            necessaryMeter.motorChange = motorChange;
            necessaryMeter.brakeInspection = brakeIncspection;
            necessaryMeter.brakeChange = brakeChange;
            necessaryMeter.heatingInspection = heatingInspection;
            necessaryMeter.heatingChange = heatingChange;
            return true;
        }

        private Meters_TT MeterToNewTable(int id, int motorInspection, int motorChange, int brakeIncspection, int brakeChange, int heatingInspection, int heatingChange)
        {
            return new Meters_TT()
            {
                motorInspection = motorInspection,
                motorChange = motorChange,
                brakeInspection = brakeIncspection,
                brakeChange = brakeChange,
                heatingInspection = heatingInspection,
                heatingChange = heatingChange
            };
        }
        public void FinalUpdateLocomotive()
        {
            if (updateCount < locomotives.Count()) throw new InvalidOperationException("Update is't finished. UpdateCount = {0}, locomotives.Count = {1}".Format(updateCount, locomotives.Count()));
            var deletingLocomotive = route.Locomotives_TT.Where(loc => deletingType.Contains(loc.locomotiveType) && deletingNumber.Contains(loc.locomotiveNumber));
            foreach(var loc in deletingLocomotive)
            {
                context.Meters_TT.DeleteAllOnSubmit(loc.Meters_TT);
                var backgroundTime = loc.BackgroundTime_TT;
                context.Locomotives_TT.DeleteOnSubmit(loc);
                context.BackgroundTime_TT.DeleteOnSubmit(backgroundTime);
            }
            deletingNumber.Clear();
            deletingType.Clear();
        }

        public bool DeleteLocomotive(string type, string number, BackgroundTime backgroundTime, IEnumerable<Meters> meters, int sectionCount)
        {
            var necessaryLocomotive = route.Locomotives_TT.SingleOrDefault(loc => loc.locomotiveType == type && loc.locomotiveNumber == number);
            if (necessaryLocomotive == null) return false;
            var necessaryTime = necessaryLocomotive.BackgroundTime_TT;
            context.Meters_TT.DeleteAllOnSubmit(necessaryLocomotive.Meters_TT);
            context.Locomotives_TT.DeleteOnSubmit(necessaryLocomotive);
            context.BackgroundTime_TT.DeleteOnSubmit(necessaryTime);
            return true;
        }
        public bool DeleteLocomotives(IEnumerable<Locomotive> locomotives)
        {
            var deletingLocomotive = locomotives.Select(loc => loc.Publish(ToLocomotiveTT)).Where(loc=>loc!=null).ToList();
            context.Meters_TT.DeleteAllOnSubmit(context.Meters_TT.Where(meter => meter.routeID == this.routeID));
            var deletingBackgroundTime = deletingLocomotive.Select(loc => loc.BackgroundTime_TT);
            context.Locomotives_TT.DeleteAllOnSubmit(deletingLocomotive);
            context.BackgroundTime_TT.DeleteAllOnSubmit(deletingBackgroundTime);
            return true;
        }
        public Locomotives_TT ToLocomotiveTT(string type, string number, BackgroundTime backgroundTime, IEnumerable<Meters> meters, int sectionCount)
        {
            return context.Locomotives_TT.SingleOrDefault(loc => loc.locomotiveType == type && loc.locomotiveNumber == number&&loc.routeID==routeID);
        }

    }
}