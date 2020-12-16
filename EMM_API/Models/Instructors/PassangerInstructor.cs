using EMM_API.Models.RouteModels;
using LifeHacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models.Instructors
{
    public class PassangerInstructor:DBInstructor
    {
        private IEnumerable<Passanger> passangers;
        private List<int> deletingID;
        public PassangerInstructor(EmmDataContext context, Routes_TT route, IEnumerable<Passanger> passangers)
        {
            this.context = context;
            this.route = route;
            this.routeID = route.id;
            this.passangers = passangers;
            deletingID = new List<int>();
            deletingID.AddRange(route.Passangers_TT.Select(pass=>pass.id).ToList());
        }
        //public int ToPassangerID(int id, int number, DateTime arraval, DateTime depature)
        //{
        //    return id;
        //}
        //private List<int> GetNotDeletingPassangers()
        //{
        //    var notDeletingID = new List<int>();
        //    var passID = route.Passangers_TT.Select(pass => pass.id).ToList();
        //    foreach (var passanger in this.passangers)
        //    {
        //        if (passID.Contains(passanger.ToAway(ToPassangerID)))
        //            notDeletingID.Add(passanger.ToAway(ToPassangerID));
        //    }
        //    return notDeletingID;
        //}
        public void CreatePassangers(IEnumerable<Passanger> passangers)
        {
            var list = passangers.Select(pass => pass.Publish(ToCreatingPassanger)).ToList();
            context.Passangers_TT.InsertAllOnSubmit(list);
        }
        public bool CreatePassanger(int id, int number, DateTime arraval, DateTime depature)
        {
            Passangers_TT newPassanger = ToCreatingPassanger(id, number, arraval, depature);
            context.Passangers_TT.InsertOnSubmit(newPassanger);
            return true;

        }
        private Passangers_TT ToCreatingPassanger(int id, int number, DateTime arraval, DateTime depature)
        {
            return new Passangers_TT()
            {
                routeID = this.routeID,
                train_s_number = number,
                arravalTime = arraval,
                depatureTime = depature
            };
        }
        public bool UpdatePassangers()
        {
            //if (deletingID.Count == 0) return false;
            var flag = !passangers.Select(pass => pass.Publish(UpdatePassanger)).ToList().Contains(false);
            FinalUpdatePassangers();
            return flag;
        }
        public bool UpdatePassanger(int id, int number, DateTime arraval, DateTime depature)
        {
            var necessaryPassanger = context.Passangers_TT.SingleOrDefault(passanger => passanger.id == id);
            updateCount++;
            if (necessaryPassanger == null) return CreatePassanger(id, number, arraval, depature);
            necessaryPassanger.train_s_number = number;
            necessaryPassanger.arravalTime = arraval;
            necessaryPassanger.depatureTime = depature;
            if (deletingID.Count > 0)
                deletingID.Remove(necessaryPassanger.id);
            return true;
        }

        public void DeletePassangers()
        {
            context.Passangers_TT.DeleteAllOnSubmit(route.Passangers_TT);
        }

        public bool DeletePassanger(int id, int number, DateTime arraval, DateTime depature)
        {
            Passangers_TT necessaryPassanger = route.Passangers_TT.SingleOrDefault(pass => pass.id == id);
            if (necessaryPassanger == null) return false;
            context.Passangers_TT.DeleteOnSubmit(necessaryPassanger);
            return true;
        }

        //private Passangers_TT ToDeletingPassanger(int id, int number, DateTime arraval, DateTime depature)
        //{
        //    return route.Passangers_TT.SingleOrDefault(pass => pass.id == id);
        //}

        public bool FinalUpdatePassangers()
        {
            if (updateCount < passangers.Count()) throw new InvalidOperationException("Update is't finished. UpdateCount = {0}, passangers.Count = {1}".Format(updateCount, passangers.Count()));
            var deletingPassangers = route.Passangers_TT.Where(pass => deletingID.Contains(pass.id)).ToList();
            context.Passangers_TT.DeleteAllOnSubmit(deletingPassangers);
            deletingID.Clear();
            return true;
        }
    }
}