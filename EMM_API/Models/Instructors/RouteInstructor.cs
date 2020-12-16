using EMM_API.Models.Instructors;
using EMM_API.Models.RouteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM_API.Models.Instructionrs
{
    public class RouteInstructor:DBInstructor
    {
        private PassangerInstructor passInstructor;
        private StationInstructor stationInstructor;
        private TrainInstructor trainInstructor;
        private LocomotiveInstructor locomotiveInstructor;
        private string userID;
        public RouteInstructor(EmmDataContext context, string userID)
        {
            this.routeID = -1;
            this.context = context;
            this.userID = userID;
        }
        public bool Refresh(int id, DateTime start, DateTime finish, IEnumerable<Locomotive> locomotives, IEnumerable<Passanger> passangers, IEnumerable<Train> trains, IEnumerable<Station> stations, string comment, bool single)
        {
            using (context)
            {
                route = context.Routes_TT.SingleOrDefault(element => element.id == id);
                if (route == null) return false;
                this.routeID = id;
                InitializeInstructors(locomotives, passangers, trains, stations);
                route.start = start;
                route.finish = finish;
                route.comment = comment;
                route.single = Convert.ToInt32(single);
                if (!passInstructor.UpdatePassangers()) return false;
                if (!locomotiveInstructor.UpdateLocomotives()) return false;
                if (!stationInstructor.UpdateStations()) return false;
                if (!trainInstructor.UpdateTrains()) return false;
                context.SubmitChanges();
            }
            return true;
        }

        private void InitializeInstructors(IEnumerable<Locomotive> locomotives, IEnumerable<Passanger> passangers, IEnumerable<Train> trains, IEnumerable<Station> stations)
        {
            passInstructor = new PassangerInstructor(context, route, passangers);
            stationInstructor = new StationInstructor(context, route, stations);
            trainInstructor = new TrainInstructor(context, route, trains);
            locomotiveInstructor = new LocomotiveInstructor(context, route, locomotives);
        }

        public bool Create(int id, DateTime start, DateTime finish, IEnumerable<Locomotive> locomotives, IEnumerable<Passanger> passangers, IEnumerable<Train> trains, IEnumerable<Station> stations, string comment, bool single)
        {
            using (context)
            {
                int? newRouteID = -1;
                context.InsertRoute(ref newRouteID, userID, start, finish, comment, Convert.ToInt32(single));
                if (newRouteID==null||newRouteID<1) return false;
                routeID = newRouteID.Value;
                route = context.Routes_TT.SingleOrDefault(element => element.id == routeID);
                InitializeInstructors(locomotives, passangers, trains, stations);
                passInstructor.CreatePassangers(passangers);
                locomotiveInstructor.CreateLocomotives(locomotives);
                stationInstructor.CreateStations(stations);
                context.SubmitChanges();
                trainInstructor.CreateTrains(trains);
                context.SubmitChanges();
            }
            return true;
        }
        public bool Delete(int id, DateTime start, DateTime finish, IEnumerable<Locomotive> locomotives, IEnumerable<Passanger> passangers, IEnumerable<Train> trains, IEnumerable<Station> stations, string comment, bool single)
        {
            using (context)
            {
                routeID = id;
                route = context.Routes_TT.SingleOrDefault(element => element.id == routeID);
                InitializeInstructors(locomotives, passangers, trains, stations);
                trainInstructor.DeleteTrains();
                stationInstructor.DeleteStations();
                locomotiveInstructor.DeleteLocomotives(locomotives);
                passInstructor.DeletePassangers();
                context.Routes_TT.DeleteOnSubmit(route);
                context.SubmitChanges();
            }
            return true;
        }





    }
}
