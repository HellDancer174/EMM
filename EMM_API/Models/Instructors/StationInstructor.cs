using EMM_API.Models.RouteModels;
using LifeHacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models.Instructors
{
    public class StationInstructor:DBInstructor
    {
        private IEnumerable<Station> stations;
        private List<int> deletingID;
        public StationInstructor(EmmDataContext context, Routes_TT route, IEnumerable<Station> stations)
        {
            this.context = context;
            this.route = route;
            this.routeID = route.id;
            this.stations = stations;
            deletingID = new List<int>();
            deletingID.AddRange(route.Stations_TT.Select(station => station.id).ToList());
        }
        public bool UpdateStations()
        {
            //if (deletingID.Count == 0) return false;
            var flag = !stations.Select(station => station.Publish(UpdateStation)).ToList().Contains(false);
            return flag;
        }
        public bool UpdateStation(int id, string name, DateTime depatureTime, DateTime arravalTime, TimeSpan maneurus)
        {
            var necessaryStation = context.Stations_TT.SingleOrDefault(station => station.id == id);
            updateCount++;
            if (necessaryStation == null)
            {
                CreateStation(id, name, depatureTime, arravalTime, maneurus);
                return true;
            }
            else
            {
                var stationID = GetStationID(name);
                if (stationID == -1) return false;
                necessaryStation.stationID = stationID;
                necessaryStation.depatureTime = depatureTime;
                necessaryStation.arravalTime = arravalTime;
                necessaryStation.maneuvers = maneurus;
                if(deletingID.Count>0)
                deletingID.Remove(necessaryStation.id);
                return true;
            }
        }

        public int GetStationID(string name)
        {
            var stationTS = context.Stations_TS.FirstOrDefault(station => station.name == name);
            if (stationTS == null) return -1;
            else return stationTS.id;
        }

        public bool CreateStation(int id, string name, DateTime depatureTime, DateTime arravalTime, TimeSpan maneurus)
        {
            Stations_TT newStation = ToStationTT(id, name, depatureTime, arravalTime, maneurus);
            if (newStation.stationID == -1) return false;
            context.Stations_TT.InsertOnSubmit(newStation);
            return true;
        }
        public bool CreateStations(IEnumerable<Station> stations)
        {
            var newStations = stations.Select(station => station.Publish(ToStationTT)).ToList();
            if (newStations.Select(station => station.stationID).Contains(-1)) return false;
            context.Stations_TT.InsertAllOnSubmit(newStations);
            return true;
        }

        private Stations_TT ToStationTT(int id, string name, DateTime depatureTime, DateTime arravalTime, TimeSpan maneurus)
        {
            var stationID = GetStationID(name);
            return new Stations_TT() { routeID = this.routeID, depatureTime = depatureTime, arravalTime = arravalTime, maneuvers = maneurus, stationID = stationID };
        }
        public bool FinalUpdateStation()
        {
            if (updateCount < stations.Count()) throw new InvalidOperationException("Update is't finished. UpdateCount = {0}, stations.Count = {1}".Format(updateCount, stations.Count()));
            var deletingStation = route.Stations_TT.Where(station => deletingID.Contains(station.id)).ToList();
            context.Stations_TT.DeleteAllOnSubmit(deletingStation);
            deletingID.Clear();
            return true;
        }
        public bool DeleteStation(int id, string name, DateTime depatureTime, DateTime arravalTime, TimeSpan maneurus)
        {
            var deletingStation = route.Stations_TT.SingleOrDefault(station => station.id == id);
            if (deletingStation == null) return false;
            context.Stations_TT.DeleteOnSubmit(deletingStation);
            return true;
        }
        public void DeleteStations()
        {
            context.Stations_TT.DeleteAllOnSubmit(route.Stations_TT);
        }
    }
}