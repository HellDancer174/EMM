using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.ViewModels
{
    public class UserVM
    {
        public UserVM(string position, double rate, string qualification)
        {
            Position = position;
            Rate = rate;
            Qualification = qualification;
        }
        public string Position { get; set; }
        public double Rate { get; set; }
        public string Qualification { get; set; }
    }
}
