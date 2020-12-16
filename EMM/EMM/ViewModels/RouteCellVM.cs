using EMM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM.ViewModels
{
    public abstract class RouteCellVM: IStringFromRoute
    {
        protected IRouteModel model;
        public DateTime Date { get; set; }
        public string Direction { get; set; }

        public RouteCellVM(IRouteModel model)
        {
            this.model = model;
            Rebuild();
        }

        public abstract void Rebuild();

    }
}
