using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class RouteDistance : TrainDistance
    {
        public RouteDistance(string first, string last) : base(first, last)
        {
        }
        protected override void Initialize()
        {
            distance = new Dictionary<string, int>();
            distance.Add("Челябинск - Карталы", 261);
            distance.Add("Челябинск - Кропачево", 320);
            distance.Add("Челябинск - Златоуст", 160);
            distance.Add("Челябинск - Каменск-Уральский", 161);
            distance.Add("Челябинск - Екатеринбург", 271);
            distance.Add("Челябинск - Екатеринбург Пассажирский", 271);
            distance.Add("Челябинск - Екатеринбург Сортировочный", 271);
            distance.Add("Челябинск - Курган", 262);
            distance.Add("Челябинск - Петропавловск", 525);
        }
    }
}
