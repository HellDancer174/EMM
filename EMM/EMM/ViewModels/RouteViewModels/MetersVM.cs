using EMM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.ViewModels
{
    public class MetersVM
    {
        public int MotorMeterAtInspection { get; set; }
        public int MotorMeterAtChange { get; set; }
        public int BrakeMeterAtInspection { get; set; }
        public int BrakeMeterAtChange { get; set; }
        public int HeatingMeterAtInspection { get; set; }
        public int HeatingMeterAtChange { get; set; }
        public string LocomotiveName { get; set; }
        private IMetersModel metersModel;
        //public MetersVM(string locomotive)
        //{
        //    this.LocomotiveName = locomotive;
        //}
        public MetersVM(IMetersModel metersModel, string locomotive)
        {
            this.metersModel = metersModel;
            this.metersModel.Publish((id, motorInspection, motorChange, brakeInspection, brakeChange, heatingInspection, heatingChange) =>
            {
                this.MotorMeterAtInspection = motorInspection;
                this.MotorMeterAtChange = motorChange;
                this.BrakeMeterAtInspection = brakeInspection;
                this.BrakeMeterAtChange = brakeChange;
                this.HeatingMeterAtInspection = heatingInspection;
                this.HeatingMeterAtChange = heatingChange;
            });
            this.LocomotiveName = locomotive;
        }
        public Meters ToMeters()
        {
            RebuildModel();
            return (Meters)metersModel;
        }

        public void RebuildModel()
        {
            metersModel.Rebuild(new Meters(-1, MotorMeterAtInspection, MotorMeterAtChange, BrakeMeterAtInspection, BrakeMeterAtChange, HeatingMeterAtInspection, HeatingMeterAtChange));
        }
    }
}
