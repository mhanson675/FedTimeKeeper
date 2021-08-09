using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FedTimeKeeper.ViewModels
{
    //TODO: Tighten up validation, HoursTaken can still be left blank somehow.
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
        public bool SaveButtonEnabled => IsValidLeave();


        private LeaveType selectedLeaveType;
        public LeaveType SelectedLeaveType
        {
            get => selectedLeaveType;
            set
            {
                selectedLeaveType = value;
                OnPropertyChanged(nameof(SelectedLeaveType));
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
                startDate = ValidateStartDate(value);
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = ValidateEndDate(value);
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private double hoursTaken;
        public double HoursTaken
        {
            get => hoursTaken;
            set
            {
                hoursTaken = ValidateHours(value);
                OnPropertyChanged(nameof(HoursTaken));
                OnPropertyChanged(nameof(SaveButtonEnabled));
            }
        }

        public AddLeaveViewModel(INavigationService navigation, IScheduledLeaveService leaveService)
        {
            this.navigation = navigation;
            this.leaveService = leaveService;

            LeaveTypes = new List<string> { ANNUAL, SICK, TIMEOFF };
            LeaveTypeText = string.Empty;
            SelectedLeaveType = new LeaveType();
            EndDate = DateTime.Now.Date;
            StartDate = DateTime.Now.Date;
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

        private DateTime ValidateStartDate(DateTime dateEntered)
        {
            if (dateEntered.Date <= EndDate.Date)
            {
                return dateEntered;
            }
            else
            {
                ThrowInvalidEntryMessage(nameof(StartDate));
                return startDate;
            }
        }

        private DateTime ValidateEndDate(DateTime dateEntered)
        {
            if (StartDate.Date <= dateEntered.Date)
            {
                return dateEntered;
            }
            else
            {
                ThrowInvalidEntryMessage(nameof(EndDate));
                return endDate;
            }
        }

        private double ValidateHours(double hoursEntered)
        {
            int days = endDate.Subtract(startDate).Days + 1;

            int possibleHours = days * 8;

            if (hoursEntered > possibleHours)
            {
                ThrowInvalidEntryMessage(nameof(HoursTaken));
                return hoursTaken;
            }
            else
            {
                return hoursEntered;
            }
        }

        private bool IsValidLeave()
        {
            return HoursTaken > 0;
        }

        private void ThrowInvalidEntryMessage(string entry)
        {
            switch (entry)
            {
                case "StartDate":
                    navigation.DisplayAlertMessage("Invalid Start Date", "The Start Date must be on or before the End Date.", "Ok");
                    break;
                case "EndDate":
                    navigation.DisplayAlertMessage("Invalid End Date", "The End Date must be on or after the Start Date.", "Ok");
                    break;
                case "HoursTaken":
                    navigation.DisplayAlertMessage("Invalid Hours", "The Hours taken cannot exceed 8 hours per day.", "Ok");
                    break;
                default:
                    navigation.DisplayAlertMessage("Invalid Entry", "You entered and invalid value.", "Ok");
                    break;
            }
        }
    }
}