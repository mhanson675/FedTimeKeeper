using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FedTimeKeeper.ViewModels
{
    public class AddLeaveViewModel : BaseViewModel
    {
        private const string TIMEOFF = "Time-off Award";
        private const string ANNUAL = "Annual";
        private const string SICK = "Sick";

        private readonly INavigationService navigation;
        private readonly IScheduledLeaveService leaveService;

        public List<string> LeaveTypes { get; set; }

        public ICommand SaveLeaveCommand => new Command(SaveLeave);
        public ICommand CancelCommand => new Command(Cancel);

        
        private LeaveType selectedLeaveType;
        public LeaveType SelectedLeaveType
        {
            get => selectedLeaveType;
            set
            {
                selectedLeaveType = value;
                OnPropertyChanged(nameof(SelectedLeaveType));
                LeaveTypeText = LeaveTypeToText(selectedLeaveType);
            }
        }

        private string leaveTypeText;
        public string LeaveTypeText
        {
            get => leaveTypeText;
            set
            {
                leaveTypeText = value;
                OnPropertyChanged(nameof(LeaveTypeText));
            }
        }


        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private double hoursTaken;
        public double HoursTaken
        {
            get => hoursTaken;
            set
            {
                hoursTaken = value;
                OnPropertyChanged(nameof(HoursTaken));
            }
        }

        public AddLeaveViewModel(INavigationService navigation, IScheduledLeaveService leaveService)
        {
            this.navigation = navigation;
            this.leaveService = leaveService;

            LeaveTypes = new List<string> { ANNUAL, SICK, TIMEOFF };
            LeaveTypeText = string.Empty;
            SelectedLeaveType = new LeaveType();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        private void Cancel(object obj)
        {
            navigation.GoBack();
        }

        private void SaveLeave(object obj)
        {
            var newLeave = new ScheduledLeave
            {
                Type = selectedLeaveType,
                StartDate = startDate,
                EndDate = endDate,
                HoursTaken = hoursTaken
            };

            leaveService.SaveLeave(newLeave);
            navigation.DisplayAlertMessage("Leave Saved", $"{newLeave.Type} leave has been scheduled.", "Ok");
            navigation.GoBack();
        }
        private string LeaveTypeToText(LeaveType leaveType)
        {
            switch (leaveType)
            {
                case LeaveType.Sick:
                    return SICK;
                case LeaveType.Timeoff:
                    return TIMEOFF;
                default:
                    return ANNUAL;
            };
        }
    }
}