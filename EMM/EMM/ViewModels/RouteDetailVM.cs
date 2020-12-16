using EMM.Models;
using EMM.Services;
using EMM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public class RouteDetailVM : RouteVM
    {
        //private HeightControl heightControl = new HeightControl();
        public event Action RebuildYou;

        public RouteDetailVM(IRouteModel model):base(model)
        {
        }

        public override void RebuildMe()
        {
            base.RebuildMe();
            if (RebuildYou != null) RebuildYou.Invoke();
            RefreshView();
        }

        public EditRouteVM GoToEditRoute(ICommandPage page)
        {
            var editPage = new EditRouteVM(this.model, page);
            editPage.RebuildYou += RebuildMe;
            return editPage;
        }
        protected void RefreshView()
        {
            OnPropertyChanged(nameof(DateOfStart));
            OnPropertyChanged(nameof(Start));
            OnPropertyChanged(nameof(Finish));
            OnPropertyChanged(nameof(Passangers));
            OnPropertyChanged(nameof(Locomotives));
            OnPropertyChanged(nameof(Meters));
            OnPropertyChanged(nameof(Stations));
            OnPropertyChanged(nameof(Trains));
            OnPropertyChanged(nameof(Comment));
        }
        public Command DeleteCommand
        {
            get
            {
                return new Command(() => Delete());
            }
        }

        protected virtual void Delete()
        {
            MessagingCenter.Send(this, "DeleteRoute", (Route)model);
        }
    }
}
