using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class DBDirections: DirectionFormatter
    {
        private EmmDataContext context;
        private IDictionary<string, string> bigLittleStationsPair;
        public DBDirections()
        {
            context = new EmmDataContext();
            bigLittleStationsPair = new Dictionary<string, string>();
            bigLittleStationsPair.Add("Кропачево", "Златоуст");
            bigLittleStationsPair.Add("Петропавловск", "Курган");
            bigLittleStationsPair.Add("Карталы", "Карталы");
            bigLittleStationsPair.Add("Екатеринбург", "Нижняя");
        }
        private IEnumerable<string> GetStationsNames(string direction)
        {
            var directionsEntities = from dir in context.Directions_TS
                                     join station in context.Stations_TS on dir.stationID equals station.id
                                     where dir.direction == direction
                                     select station.name;
            return directionsEntities.AsEnumerable();
        }

        public Direction CreateDirection(string first, string last)
        {
            var stationsNames = GetStationsNames(GetDirection(first, last));
            var localStationsNames = GetStationsNames("Челябинский узел");
            return new Direction(stationsNames, localStationsNames, first, last, last, bigLittleStationsPair[last]);
        }
    }
}