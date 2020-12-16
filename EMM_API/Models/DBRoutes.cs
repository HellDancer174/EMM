using EMM_API.Models.Instructionrs;
using EMM_API.Models.RouteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class DBRoutes
    {
        EmmDataContext context = new EmmDataContext();
        public IEnumerable<Route> GetRoutes(string userID)
        {
            using (context)
            {
                var routes = context.Routes_TT.Where(route => route.userID == userID).OrderBy(routeDTO => routeDTO.start);
                var result = new List<Route>();
                foreach(var route in routes)
                {
                    result.Add(GetRoute(route));
                }
                return result;
            }
        }
        public bool CreateRoute(Route route, string userID)
        {
            var instructor = new RouteInstructor(context, userID);
            return route.Publish(instructor.Create);
        }
        public bool UpdateRoute(Route route, string userID)
        {
            var instructor = new RouteInstructor(context, userID);
            return route.Publish(instructor.Refresh);
        }
        public bool DeleteRoute(Route route, string userID)
        {
            var instructor = new RouteInstructor(context, userID);
            return route.Publish(instructor.Delete);
        }


        private Route GetRoute(Routes_TT route)
        {
            var routeID = route.id;
            var locomotives = context.Locomotives_TT
            .Where(locomotive => locomotive.routeID == routeID)
            .Select(locomotive => new Locomotive(locomotive.locomotiveType, locomotive.locomotiveNumber, new BackgroundTime(locomotive.backgroundTimeID, locomotive.BackgroundTime_TT.inspection, locomotive.BackgroundTime_TT.cpExit.GetValueOrDefault(), locomotive.BackgroundTime_TT.cpEntrance.GetValueOrDefault(), locomotive.BackgroundTime_TT.change),
            locomotive.Meters_TT.Select(meter => new Meters(meter.id, meter.motorInspection, meter.motorChange, meter.brakeInspection, meter.brakeChange, meter.heatingInspection, meter.heatingChange)), locomotive.Locomitive_TS.sectionCount))
            .ToList();
            var passangers = context.Passangers_TT
            .Where(passanger => passanger.routeID == routeID)
            .Select(passanger => new Passanger(passanger.id, passanger.train_s_number, passanger.arravalTime, passanger.depatureTime)).ToList();
            var stations = context.Stations_TT
            .Where(station => station.routeID == routeID)
            .Select(station => new Station(station.id, context.Stations_TS.SingleOrDefault(stationTS => stationTS.id == station.stationID).name, GetDateTime(station.depatureTime), GetDateTime(station.arravalTime), GetManeuvers(station)))
            .ToList();
            var trains = context.Trains_TT
            .Where(train => train.routeID == routeID)
            .Select(train => new Train(train.id, train.number, train.Stations_TS.name, train.Stations_TS1.name, SelectLocomotive(train.locomotiveType, train.locomotiveNumber, locomotives), train.type, train.weight, train.axis, train.length))
            .ToList();
            return new Route(routeID, route.start, route.finish, locomotives, trains, stations, passangers, route.comment, Convert.ToBoolean(route.single.GetValueOrDefault()));
        }

        private static DateTime GetDateTime(DateTime? nullableDatetime)
        {
            DateTime datetime;
            try
            {
                datetime = nullableDatetime.Value;
            }
            catch
            {
                datetime = default(DateTime);
            }
            return datetime;
        }

        private static TimeSpan GetManeuvers(Stations_TT station)
        {
            TimeSpan time;
            try
            {
                time = station.maneuvers.Value;
            }
            catch
            {
                time = default(TimeSpan);
            }
            return time;
        }

        private Locomotive SelectLocomotive(string locomotiveType, string locomotiveNumber, IEnumerable<Locomotive> locomotives)
        {
            return locomotives.SingleOrDefault(locomotive => locomotive.ToString() == locomotiveType + "-" + locomotiveNumber);
        }
    }
}