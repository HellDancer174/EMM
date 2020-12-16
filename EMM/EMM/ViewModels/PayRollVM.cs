using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.Models;
using EMM.Services;
using EMM.Views;
using EMM.Helpers;
using EMM.Helpers.UserDecorators;

namespace EMM.ViewModels
{
    public class PayRollVM : ListViewVM
    {
        private Directions directions;
        private ApiServices services = new ApiServices();
        private int diselCount;
        private int electroCount;
        private string month;
        private readonly UsersShow user;
        private readonly UsersPayRollCreater usersPayRollCreater;

        public string Month
        {
            get => month;
            set
            {
                month = value;
                ExecuteLoadItemsCommand();
            }
        }
        public string Rate { get; set; }
        public string Position { get; set; }
        public ObservableCollection<string> Months { get; set; }

        public ObservableCollection<PayRollCellVM> PremiumStrings { get; set; }
        public string TotalCash { get; set; }
        public PayRollVM(IRoutes routes, ICommandPage page, User user) : base(routes, page)
        {
            Months = new ObservableCollection<string>() { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };
            this.user = new UsersShow(user);
            this.usersPayRollCreater = new UsersPayRollCreater(user);
            //RefreshUserStatus();
        }

        private void RefreshUserStatus()
        {
            Position = this.user.ShowPosition();
            Rate = this.user.ShowRate();
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(Rate));
        }

        protected override async Task Rebuild()
        {
            RefreshUserStatus();
            if (Month == null) return;
            var dirs = await services.GetDirectionsAsync();
            directions = new Directions(dirs.ToList());
            var newRoutes = await routes.GetItemsAsync();
            List<PayRollCellVM> cells = CreateCells(newRoutes, (DateTime.Now - new TimeSpan(2, 0, 0)).Year);
            if (cells.Count == 0) cells = CreateCells(newRoutes, (DateTime.Now - new TimeSpan(2, 0, 0)).Year - 1);
            PremiumStrings = new ObservableCollection<PayRollCellVM>(cells);
            TotalCash = "Итого: " + Math.Round(cells.Select(cell => cell.Value).Sum(), 2);
        
            OnPropertyChanged(nameof(TotalCash));
            OnPropertyChanged(nameof(PremiumStrings));
        }
        private List<PayRollCellVM> CreateCells(IEnumerable<Route> routes, int year) //Доделать
        {
            diselCount = 0;
            electroCount = 0;
            int monthsIndex = Months.IndexOf(month) + 1;
            var creater = usersPayRollCreater.CreatePayRoll(routes.ToList(), monthsIndex, year, directions);
            var list = new List<PayRollCellVM>() { creater.CreateFullTime(), creater.CreateHarmfulness(), creater.CreateArea(), creater.CreateHeavyTrain(), creater.CreateLongTrain(),
            creater.CreateNightTime(), creater.CreateFirstConnect(), creater.CreateLastConnect(), creater.CreateManueversWithoutHelper(), creater.CreateRailLubricator(), creater.CreatePassangerTime(),
            creater.CreateWaitPassangerTime()};
            list.Add(creater.CreateQualificationClass());
            list = list.Where(cell => !cell.IsEmpty()).ToList();
            var localpremium = list.Select(cell => cell.Value).Sum() * 0.15;
            list.Add(creater.CreateLocalPremium(localpremium));
            return list;

        }

    }
}
