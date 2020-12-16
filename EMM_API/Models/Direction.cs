using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EMM_API.Models
{
    [DataContract]
    public class Direction: DirectionFormatter
    {
        [DataMember]
        private string name;
        [DataMember]
        private string first;
        [DataMember]
        private string last;
        [DataMember]
        private readonly string[] stationsName;
        [DataMember]
        private readonly string[] localStations;
        [DataMember]
        private readonly string bigTechStation;
        [DataMember]
        private readonly string littleTechStation;


        public Direction(IEnumerable<string> stationsName, IEnumerable<string> localStations, string first, string last, string bigTechStation, string littleTechStation)
        {
            this.stationsName = stationsName.ToArray();
            this.localStations = localStations.ToArray();
            this.first = first;
            this.last = last;
            Rename();
            this.bigTechStation = bigTechStation; /*new List<string>() { "Кропачево", "Петропавловск", "Карталы", "Верхний Уфалей", "Екатеринбург", "Екатеринбург Пассажирский", "Екатеринбург Сортировочный" };*/
            this.littleTechStation = littleTechStation;/*new List<string>() { "Златоуст", "Курган", "Нижняя" } ;*/
        }

        private void Rename()
        {
            if (this.stationsName.Length == 0)
            {
                name = "Без имени";
                return;
            }
            if (first.StartsWith("Челябинск")) first = "Челябинск";
            if (last.StartsWith("Челябинск")) last = "Челябинск";
            if (first.StartsWith("Екатеринбург")) first = "Екатеринбург";
            if (last.StartsWith("Екатеринбург")) last = "Екатеринбург";
            name = GetDirection(first, last);
        }

        public void Reverse()
        {
            var temp = first;
            first = last;
            last = temp;
            Rename();
            stationsName.Reverse();
        }
        public override string ToString() => name;
        public bool IsOwner(IEnumerable<string> stations)
        {
            var countStations = stations.Count();
            if (countStations == 0) return false;
            var countDirectionsStations = stations.Select(station => stationsName.Contains(station)).Where(station => station == true).Count();
            var countLocalsStations = stations.Select(station => localStations.Contains(station)).Where(station => station == true).Count();
            if (countDirectionsStations == countStations || countDirectionsStations + countLocalsStations == countStations && countStations != countLocalsStations) return true;
            else return false;
        }
        public string ToString(List<string> stations)
        {
            if (IsOwner(stations) == false) return String.Empty;

            var lastFlag = bigTechStation == stations.LastOrDefault() && stations.FirstOrDefault().StartsWith("Челябинск");
            var firstFlag = bigTechStation == stations.FirstOrDefault() && stations.LastOrDefault().StartsWith("Челябинск");
            if (lastFlag) return GetDirection("Челябинск", stations.LastOrDefault());
            if (firstFlag) return GetDirection("Челябинск", stations.FirstOrDefault());

            //var smallTechStationsFlags = smallTechStations.Select(station => stations.Contains(station)).ToList();
            if (stations.Contains(littleTechStation)) return GetDirection("Челябинск", littleTechStation);
            return String.Empty;
        }
    }
}