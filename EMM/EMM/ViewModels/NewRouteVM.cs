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
using EMM.Trip.Routes;

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
                return new Command(async() =>
                {
                    if (Save() == false) page.PrintErorAsync("Проверьте правильность введенных данных");
                    Route route = new InsertableRoute((Route)model, new ApiServices(), page);
                    await route.Transfer();
                });
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
