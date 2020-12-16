using EMM.Views;
using EMM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EMM.ViewModels
{
    public class SettingsVM: BaseSettingsVM
    {
        private readonly User user;
        private INavigational navigationPage;
        public SettingsVM(INavigational page, User user, string position, string qualification, double rate):base(page)
        {
            this.positions = new ObservableCollection<string>(Settings.PositionRates.Keys);
            Position = position;
            this.user = user;
            this.qualification = qualification;
            //OnPropertyChanged(nameof(Qualification));
            this.rate = rate;
            navigationPage = page;
        }
        public async Task Save()
        {
            await user.Rebuild(new User(Position, rate, Qualification), page);
        }
        public Command ChangePasswordCommand
        {
            get
            {
                return new Command(ToChangePasswordPage);
            }
        }
        private async void ToChangePasswordPage()
        {
            await navigationPage.PushAsync(new ChangePasswordPage());
        }



    }
}
