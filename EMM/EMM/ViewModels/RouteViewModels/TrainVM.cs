using EMM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EMM.ViewModels
{
    public class TrainVM
    {
        public int Id { get; set; }
        public int Number { get; set; }
        private string arravalStation;
        public string ArravalStation { get => arravalStation; set { if (value == arravalStation) return; arravalStation = value; } }
        private string depatureStation;
        public string DepatureStation { get => depatureStation; set { if (value == depatureStation) return; depatureStation = value; } }
        private IEnumerable<LocomotiveVM> locomotives { get; set; }
        public ObservableCollection<string> LocomotivesName { get; set; }
        private IEnumerable<StationVM> stations { get; set; }
        public ObservableCollection<string> StationsName { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Types { get; set; } = new ObservableCollection<string>() { "Т", "Д", "ТД", "ПВДС", "СГ", "СП" };
        public string Type { get; set; }
        private ITrainModel trainModel;
        public void ResetStationsName()
        {
            StationsName.Clear();
            foreach(var station in stations)
            {
                if (station.Name == null) continue;
                StationsName.Add(station.Name);
            }
        }
        private string locomotive;
        public string Locomotive { get=>locomotive; set { if (value == locomotive) return; locomotive = value;} }
        public int Weight { get; set; }
        public int Axis { get; set; }
        public int Length { get; set; }
        //public TrainVM(IEnumerable<StationVM> stations, IEnumerable<LocomotiveVM> locomotives)
        //{
        //    this.stations = stations;
        //    this.locomotives = locomotives;
        //    ResetStationsName();
        //}
        public void RefreshLocomotiveName()
        {
            LocomotivesName.Clear();
            foreach(var loc in locomotives)
            {
                if (loc == null) continue;
                LocomotivesName.Add(loc.Name);
            }
        }
        public TrainVM(IEnumerable<StationVM> stations, IEnumerable<LocomotiveVM> locomotives, ITrainModel trainModel)
        {
            this.stations = stations;
            this.locomotives = locomotives;
            this.trainModel = trainModel;
            this.LocomotivesName = new ObservableCollection<string>();
            RefreshLocomotiveName();
            ResetStationsName();
            trainModel.Publish((id, number, arravalStation, depatureStation, loc, type, weight, axis, length)=> 
            {
                this.Number = number;
                this.ArravalStation = arravalStation;
                this.DepatureStation = depatureStation;
                this.Weight = weight;
                this.Axis = axis;
                this.Length = length;
                this.Type = type;
                if (loc == null) this.Locomotive = null;
                else this.Locomotive = loc.ToString();
            });
        }
        public Train ToTrain(Locomotive locomotive)
        {
            RebuildModel(locomotive);
            return (Train)trainModel;
        }

        public void RebuildModel(Locomotive locomotive)
        {
            trainModel.Rebuild(new Train(-1, this.Number, this.ArravalStation, this.DepatureStation, locomotive, this.Type, this.Weight, this.Axis, this.Length));
        }
    }
}
