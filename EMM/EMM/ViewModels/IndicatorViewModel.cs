using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.ViewModels
{
    public class IndicatorViewModel: BaseViewModel
    {
        private bool isEnable = false;
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                if (isEnable == value) return;
                isEnable = value;
                OnPropertyChanged();
            }
        }

        private bool isVisible = false;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible == value) return;
                isVisible = value;
                OnPropertyChanged();
            }
        }
        public void OnIndicator()
        {
            IsBusy = true;
            IsVisible = true;
            IsEnable = true;
        }
        public void OffIndicator()
        {
            IsBusy = false;
            IsVisible = false;
            IsEnable = false;
        }
    }
}
