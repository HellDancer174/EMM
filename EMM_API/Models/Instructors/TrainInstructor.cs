using EMM_API.Models.RouteModels;
using LifeHacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models.Instructors
{
    public class TrainInstructor:DBInstructor
    {
        private IEnumerable<Train> trains;
        private List<int> deletingID;
        private LocomotiveInstructor locomotiveInstructor;
        private StationInstructor stationInstructor;
        public TrainInstructor(EmmDataContext context, Routes_TT route, IEnumerable<Train> trains)
        {
            this.context = context;
            this.route = route;
            this.routeID = route.id;
            this.trains = trains;
            deletingID = new List<int>();
            deletingID.AddRange(context.Trains_TT.Where(train => train.routeID == routeID).Select(train => train.id).ToList());
            locomotiveInstructor = new LocomotiveInstructor(context, route, new List<Locomotive>());
            stationInstructor = new StationInstructor(context, route, new List<Station>());
        }
        public bool CreateTrain(int id, int number, string arravalStation, string depatureStation, Locomotive locomotive, string type, int weight, int axis, int length)//заменить на Train
        {
            context.Trains_TT.InsertOnSubmit(ToNewTrainTT(id, number, arravalStation, depatureStation, locomotive, type, weight, axis, length));
            return true;
        }
        public bool CreateTrains(IEnumerable<Train> trains)
        {
            var newTrains = trains.Select(train => train.Publish(ToNewTrainTT)).ToList();
            context.Trains_TT.InsertAllOnSubmit(newTrains);
            return true;
        }
        public bool UpdateTrains()
        {
            //if (deletingID.Count == 0) return false;
            var flag = !trains.Select(train => train.Publish(UpdateTrain)).ToList().Contains(false);
            FinalUpdate();
            return flag;
        }
        public bool UpdateTrain(int id, int number, string arravalStation, string depatureStation, Locomotive locomotive, string type, int weight, int axis, int length)
        {
            var necessaryTrain = context.Trains_TT.SingleOrDefault(train => train.id == id);
            updateCount++;
            if (necessaryTrain == null)
            {
                CreateTrain(id, number, arravalStation, depatureStation, locomotive, type, weight, axis, length);
                return true;
            }
            else
            {
                necessaryTrain.routeID = routeID;
                necessaryTrain.number = number;
                AddParameters(locomotive, arravalStation, depatureStation, necessaryTrain);
                necessaryTrain.type = type;
                necessaryTrain.weight = weight;
                necessaryTrain.axis = axis;
                necessaryTrain.length = length;
                if(deletingID.Count>0)
                deletingID.Remove(necessaryTrain.id);
                return true;
            }
        }

        private void AddParameters(Locomotive locomotive, string arravalStation, string depatureStation, Trains_TT necessaryTrain)
        {
            var necessaryLocomotive = locomotive.Publish(locomotiveInstructor.ToLocomotiveTT);
            if (necessaryLocomotive == null) throw new InvalidOperationException("Искомый локомотив не найден");
            var arravalStationID = stationInstructor.GetStationID(arravalStation);
            var depatureStationID = stationInstructor.GetStationID(depatureStation);
            if (arravalStationID == -1 || depatureStationID == -1) throw new InvalidOperationException("Искомые станции не найдены");
            necessaryTrain.arravalStationID = arravalStationID;
            necessaryTrain.depatureStationID = depatureStationID;
            necessaryTrain.locomotiveType = necessaryLocomotive.locomotiveType;
            necessaryTrain.locomotiveNumber = necessaryLocomotive.locomotiveNumber;
        }

        private Trains_TT ToNewTrainTT(int id, int number, string arravalStation, string depatureStation, Locomotive locomotive, string type, int weight, int axis, int length)
        {
            
            var newTrain = new Trains_TT()
            {
                number = number,
                type = type,
                weight = weight,
                axis = axis,
                length = length,
                routeID = routeID
            };
            AddParameters(locomotive, arravalStation, depatureStation, newTrain);
            return newTrain;
        }
        public bool FinalUpdate()
        {
            if (updateCount < trains.Count()) throw new InvalidOperationException("Update is't finished. UpdateCount = {0}, trains.Count = {1}".Format(updateCount, trains.Count()));
            var deletingTrain = context.Trains_TT.Where(train => deletingID.Contains(train.id)).ToList();
            context.Trains_TT.DeleteAllOnSubmit(deletingTrain);
            deletingID.Clear();
            return true;
        }
        public bool DeleteTrain(int id, int number, string arravalStation, string depatureStation, Locomotive locomotive, string type, int weight, int axis, int length)
        {
            var deletingTrain = context.Trains_TT.SingleOrDefault(train => train.id == id);
            if (deletingTrain == null) return false;
            context.Trains_TT.DeleteOnSubmit(deletingTrain);
            return true;
        }
        public bool DeleteTrains()
        {
            context.Trains_TT.DeleteAllOnSubmit(context.Trains_TT.Where(train => train.routeID == routeID));
            return true;
        }
    }
}