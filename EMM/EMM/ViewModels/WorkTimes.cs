using EMM.Services;
using EMM.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public class WorkTimes : ListViewVM
    {
        public string Month
        {
            get => month;
            set
            {
                month = value;
                ExecuteLoadItemsCommand();
            }
        }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<WorkTime> WorkTimeStrings { get; set; }
        public string TotalHours { get; set; }
        private string month;

        public WorkTimes(IRoutes routes, WorkTimesPage page):base(routes, page)
        {
            Months = new ObservableCollection<string>() { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };
        }

        protected override async Task Rebuild()
        {
            if (Month == null) return;
            var newRoutes = await routes.GetItemsAsync();
            List<WorkTime> workTimes = ToWorkTimes(newRoutes, (DateTime.Now - new TimeSpan(2, 0, 0)).Year);
            if (workTimes.Count == 0) workTimes = ToWorkTimes(newRoutes, (DateTime.Now - new TimeSpan(2, 0, 0)).Year - 1);
            WorkTimeStrings = new ObservableCollection<WorkTime>(workTimes);
            TimeSpan time = default(TimeSpan);
            workTimes.ForEach(workTime => time += workTime.ToTimeSpan());
            TotalHours = "Итого: " + Math.Round(time.TotalHours, 2);
            OnPropertyChanged(nameof(TotalHours));
            OnPropertyChanged(nameof(WorkTimeStrings));
        }

        private List<WorkTime> ToWorkTimes(IEnumerable<Models.Route> newRoutes, int year)
        {
            return newRoutes.Where(route => route.ToWorkTime(Months.IndexOf(month) + 1, year) != default(TimeSpan)).Select(route => new WorkTime(route)).ToList();
        }


        //public async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;
        //    if (!CrossConnectivity.Current.IsConnected)
        //    {
        //        page.PrintErorAsync("Отсутствует подключение к интернету");
        //        return;
        //    }

        //    try
        //    {
        //        await Rebuild();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        OnPropertyChanged(nameof(TotalHours));
        //        OnPropertyChanged(nameof(WorkTimeStrings));
        //        IsBusy = false;
        //    }
        //}

    }
}
