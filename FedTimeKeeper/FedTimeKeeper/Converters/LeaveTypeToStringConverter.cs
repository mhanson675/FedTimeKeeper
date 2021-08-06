using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FedTimeKeeper.Converters
{
    public class LeaveTypeToStringConverter : IValueConverter
    {
        private const string TIMEOFF = "Time-off Award";
        private const string ANNUAL = "Annual";
        private const string SICK = "Sick";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LeaveType leaveType = (LeaveType)value;

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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string leaveString = (string)value;

            switch (leaveString)
            {
                case SICK:
                    return LeaveType.Sick;
                case TIMEOFF:
                    return LeaveType.Timeoff;
                default:
                    return LeaveType.Annual;
            }
        }
    }
}
