using EMM.Services;
using LifeHacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using EMM.Helpers;
using EMM.Views;

namespace EMM.ViewModels
{
    public class BaseSettingsVM : BaseViewModel
    {
        protected ObservableCollection<string> positions;
        public ObservableCollection<string> Positions
        {
            get { return positions; }
            set
            {
                if (value == positions) return;
                positions = value;
                OnPropertyChanged();
            }
        }

        protected string position;
        public string Position
        {
            get { return position; }
            set
            {
                if (value == position) return;
                position = value;
                SetPosition(position);
                OnPropertyChanged(nameof(Rate));
            }
        }
        protected double rate;

        public string Rate
        {
            get { return Convert.ToString(rate); }
            set
            {
                SetRate(value);
                //OnPropertyChanged();
            }
        }

        private void SetRate(string value)
        {
            rate = Convert.ToDouble(value);
        }

        protected string qualification;
        public string Qualification
        {
            get => qualification;
            set
            {
                if (value == qualification) return;
                else SetQualification(value);
                OnPropertyChanged();
            }
        }

        protected virtual void SetQualification(string value)
        {
            qualification = value;
        }

        protected ObservableCollection<string> qualifications;
        protected readonly ICommandPage page;

        public ObservableCollection<string> Qualifications
        {
            get
            {
                if (String.IsNullOrEmpty(position))
                {
                    qualifications.Clear();
                    //page.PrintErorAsync("Сначала выберите должность");
                }
                return qualifications;
            }
            set
            {
                if (value == qualifications) return;
                qualifications = value;
                OnPropertyChanged();
            }
        }

        public BaseSettingsVM(ICommandPage page)
        {
            positions = new ObservableCollection<string>();
            qualifications = new ObservableCollection<string>();
            position = String.Empty;
            this.page = page;
        }
        protected virtual void SetPosition(string value)
        {
            var catcher = new BooleanTryCatcher();
            if (!catcher.Execute(() => Position.StartsWith(value.Substring(0, 8)))) return;
            Qualifications.Clear();
            if (value.StartsWith("Машинист"))
            {
                Qualifications.AddRange("1 класс", "2 класс", "3 класс", "Без класса");
            }
            else if(value.StartsWith("Помощник"))
            {
                Qualifications.AddRange("С правами управления", "Без прав управления");
            }
        }
    }
}
