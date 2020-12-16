using EMM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using EMM.Services;
using System.Windows.Input;
using EMM.Helpers;
using Plugin.Connectivity;
using EMM.Views;

namespace EMM.ViewModels
{
    public class NewRouteVM: RouteVMEditable
    {
        private ICommandPage page;
        public NewRouteVM(IRouteModel model, ICommandPage page):base(model)
        {
            this.page = page;
        }

        public Command SaveCommand
        {
            get
            {
                return commander.CreateForRoute(() => MessagingCenter.Send(this, "AddRoute", (Route)model), Save, page);
            }
        }

        
        public override bool Save()
        {
            base.Save();
            var check = model.Check();
            //if (!check) Message = "Проверьте правильность введенных данных";
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    check = false;
            //    Message += "\n Отсутствует поключение к интернету";
            //}
            return check;
        }


    }
}
