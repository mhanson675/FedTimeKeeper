using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using FedTimeKeeper.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaveInformationView : ContentPage
    {
        private readonly ILeaveSummaryService leaveSummaryService;

        public LeaveInformationViewModel LeaveInformationViewModel { get; set; }
        public LeaveInformationView(ILeaveSummaryService leaveSummaryService)
        {
            InitializeComponent();

            LeaveInformationViewModel = new LeaveInformationViewModel(leaveSummaryService);

            BindingContext = LeaveInformationViewModel;
            this.leaveSummaryService = leaveSummaryService;
        }
    }
}