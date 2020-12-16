using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EMM_API.Models.RouteModels
{
    [DataContract]
    public class Train : ITrainModel
    {
        [DataMember]
        private int id;
        [DataMember]
        private int number;
        [DataMember]
        private string arravalStation;
        [DataMember]
        private string depatureStation;
        [DataMember]
        private Locomotive locomotive;
        [DataMember]
        private string type;
        [DataMember]
        private int weight;
        [DataMember]
        private int axisCount;
        [DataMember]
        private int length;

        public void Publish(Action<int, int, string, string, Locomotive, string, int, int, int> instructionsForMe)
        {
            instructionsForMe(id, number, arravalStation, depatureStation, locomotive, type, weight, axisCount, length);
        }

        public Train(int id, int number, string arravalStation, string depatureStation, Locomotive locomotives, string type, int weight, int axis, int length)
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

        public Train()
        {
            id = -1;
            number = 0;
            arravalStation = String.Empty;
            depatureStation = String.Empty;
            locomotive = null;
            type = null;
            weight = 0;
            axisCount = 0;
            length = 0;
        }

        public bool CheckArraval(string firstStation)
        {
            if (firstStation == arravalStation) return true;
            return false;
        }
        public bool CheckDepature(string lastStation)
        {
            if (lastStation == depatureStation) return true;
            return false;
        }
        public bool Check()
        {
            var trueData = (weight == 0 && axisCount == 0 && length == 0) || (weight != 0 && axisCount != 0 && length != 0);
            return !(number == 0 || String.IsNullOrEmpty(arravalStation) || String.IsNullOrEmpty(depatureStation) || locomotive == null) && trueData;
        }
        public TResult Publish<TResult>(Func<int, int, string, string, Locomotive, string, int, int, int, TResult> instructions)
        {
            return instructions(id, number, arravalStation, depatureStation, locomotive, type, weight, axisCount, length);
        }

        public void Rebuild(Train train)
        {
            this.number = train.number;
            this.weight = train.weight;
            this.type = train.type;
            this.axisCount = train.axisCount;
            this.length = train.length;
            this.arravalStation = train.arravalStation;
            this.depatureStation = train.depatureStation;
            this.locomotive = train.locomotive;
        }
    }
}
