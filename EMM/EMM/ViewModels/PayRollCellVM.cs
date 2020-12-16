using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.ViewModels
{
    public class PayRollCellVM
    {
        public string Premium { get; set; }
        public double Value { get; set; }
        public bool IsEmpty()
        {
            return Value == 0;
        }
        public PayRollCellVM(string premium, double value)
        {
            Premium = premium;
            Value = Round(value);
        }
        private double Round(double value)
        {
            return Math.Round(value, 2);
        }

    }
}
