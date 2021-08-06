using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FedTimeKeeper.Models
{
    public class LeaveSummaryModel : INotifyPropertyChanged
    {
        private string type;
        private string beginningBalance;
        private string endingBalance;
        private string earned;
        private string used;


        public string Type
        {
            get => type;
            set
            {
                type = value;
                RaisePropertyChanged(nameof(Type));
            }
        }
        public string BeginningBalance
        {
            get => beginningBalance;
            set
            {
                beginningBalance = value;
                RaisePropertyChanged(nameof(BeginningBalance));
            }
        }

        public string EndingBalance
        {
            get => endingBalance;
            set
            {
                endingBalance = value;
                RaisePropertyChanged(nameof(EndingBalance));
            }
        }

        public string Earned
        {
            get => earned;
            set
            {
                earned = value;
                RaisePropertyChanged(nameof(Earned));
            }
        }

        public string Used
        {
            get => used;
            set
            {
                used = value;
                RaisePropertyChanged(nameof(Used));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
