using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class RegisterBindingModel
    {
        public string Email { get; set; }
        public double Rate { get; set; }
        public string Position { get; set; }
        public string QualificationClass { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
