using EMM.Models;
using EMM.Services;
using EMM.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public class EditRouteVM:RouteVMEditable
    {
        new public event Action RebuildYou;
        private ICommandPage page;
        public EditRouteVM(IRouteModel model, ICommandPage page) :base(model)
        {
            this.page = page;
        }

        public Command SaveCommand
        {
            get
            {
                return commander.CreateForRoute(ToDataBase, Save, page);
            }
        }

        private void ToDataBase()
        {
            MessagingCenter.Send(this, "RefreshRoute", (Route)model);
            if (RebuildYou != null) RebuildYou.Invoke();
        }

        public override bool Save()
        {
            base.Save();
            var check = model.Check();
            return check;
        }
    }
}
