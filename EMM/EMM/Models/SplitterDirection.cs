using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EMM.Models
{
    public class SplitterDirection: DirectionFormatter
    {
        private string arraval;
        private string depature;
        private string direction;

        public SplitterDirection(string arraval, string depature)
        {
            this.arraval = arraval;
            this.depature = depature;
            this.direction = GetDirection(arraval, depature);
        }
        public SplitterDirection(string direction)
        {
            var regex = new Regex(@"(?<first>.+) - (?<last>.+)");
            var techStations = regex.Match(direction);
            this.arraval = techStations.Groups["first"].ToString();
            this.depature = techStations.Groups["last"].ToString();
            this.direction = GetDirection(arraval, depature);
        }
        private string Handle(string station)
        {
            if (station.StartsWith("Челябинск")) return "Челябинск";
            else return station;
        }
        //public string GetDirection(string arraval, string depature) => string.Format("{0} - {1}", arraval, depature);

        //public string GetReverseDirection(string arraval, string depature) => string.Format("{0} - {1}", depature, arraval);

        public List<Tuple<string, string>> Split()
        {
            if (direction == "Челябинск - Кропачево") return new List<Tuple<string, string>>() { Tuple.Create(arraval, "Златоуст"), Tuple.Create("Златоуст", "Кропачево") };
            if (direction == "Кропачево - Челябинск") return new List<Tuple<string, string>>() { Tuple.Create("Кропачево", "Златоуст"), Tuple.Create("Златоуст", depature) };
            if (direction == "Челябинск - Петропавловск") return new List<Tuple<string, string>>() { Tuple.Create(arraval, "Курган"), Tuple.Create("Курган", "Петропавловск") };
            if (direction == "Петропавловск - Челябинск") return new List<Tuple<string, string>>() { Tuple.Create("Петропавловск", "Курган"), Tuple.Create("Курган", depature) };
            return new List<Tuple<string, string>>() { Tuple.Create(arraval, depature) };
        }

    }
}
