using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FedTimeKeeper.Models
{
    public class PayPeriodModel : INotifyPropertyChanged
    {
        private int period;
        private DateTime startDate;
        private DateTime endDate;

        public int Period
        {
            get => period;
            set
            {
                period = value;
                RaisePropertyChanged(nameof(Period));
            }
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                RaisePropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                RaisePropertyChanged(nameof(EndDate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
