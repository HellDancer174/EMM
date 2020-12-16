using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class TrainDistance: DirectionFormatter, IDistance
    {
        private const string chelyabinsk = "Челябинск";
        //private const string kropachevoDirection = "Челябинск - Кропачево";
        private string direction;
        private string reversedirection;
        protected IDictionary<string, int> distance;



        //public TrainDistance(string direction)
        //{
        //    this.direction = direction;
            
        //    Initialize();
        //}
        public TrainDistance(string first, string last)
        {
            this.direction = GetDirection(first, last);
            this.reversedirection = GetReverseDirection(first, last);
            Initialize();
        }

        protected virtual void Initialize()
        {
            distance = new Dictionary<string, int>();
            distance.Add("Челябинск - Карталы", 261);
            distance.Add("Челябинск - Кропачево", 320);
            distance.Add("Челябинск - Златоуст", 160);
            distance.Add("Златоуст - Кропачево", 160);
            distance.Add("Челябинск - Нижняя", 121);
            distance.Add("Челябинск - Каменск-Уральский", 121);
            distance.Add("Челябинск - Екатеринбург", 121);
            distance.Add("Челябинск - Екатеринбург Пассажирский", 121);
            distance.Add("Челябинск - Екатеринбург Сортировочный", 121);
            distance.Add("Челябинск - Курган", 262);
            distance.Add("Курган - Петропавловск", 263);
            distance.Add("Челябинск - Петропавловск", 525);
        }

        public int ToDistance()
        {
            //if (stations.Count() == 0) return 0;
            //var first = stations.First();
            //if (first.StartsWith(chelyabinsk)) first = chelyabinsk;
            //var last = stations.Last();
            //if (last.StartsWith(chelyabinsk)) last = chelyabinsk;
            //var direction = String.Format("{0} - {1}", first, last);
            if (distance.ContainsKey(direction)) return distance[direction];
            else if(distance.ContainsKey(reversedirection)) return distance[reversedirection];
            else return 0;
        }

    }
}
