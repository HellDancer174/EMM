using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class BaseTrain
    {
        private int id;
        private int number;
        private string arravalStation;
        private string depatureStation;
        private BaseLocomotive locomotive;
        private string type;
        private int weight;
        private int axisCount;
        private int length;

        public void ToAway(Action<int, int, string, string, BaseLocomotive, string, int, int, int> instructionsForMe)
        {
            instructionsForMe(id, number, arravalStation, depatureStation, locomotive, type, weight, axisCount, length);
        }

        public BaseTrain(int id, int number, string arravalStation, string depatureStation, BaseLocomotive locomotives, string type, int weight, int axis, int length)
        {
            this.id = id;
            this.number = number;
            this.arravalStation = arravalStation;
            this.depatureStation = depatureStation;
            this.locomotive = locomotives;
            this.type = type;
            this.weight = weight;
            this.axisCount = axis;
            this.length = length;
        }
        public TResult ToAway<TResult>(Func<int, int, string, string, BaseLocomotive, string, int, int, int, TResult> instructions)
        {
            return instructions(id, number, arravalStation, depatureStation, locomotive, type, weight, axisCount, length);
        }

    }
}