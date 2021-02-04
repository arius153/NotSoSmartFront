using SmartSaver.DTO.Goal.Output;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SmartSaver.Pages.GoalsPage;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewGoalPage : ContentPage
    {
        public event NotifyParentDelegate NotifyParentEvent;
        private string goalId;
        private readonly GoalProcessor gop;
        public ViewGoalPage(GoalDTO data)
        {
            InitializeComponent();
            goalId = data.Goalid;
            gop = new GoalProcessor();
            goalName.Text = data.Goalname;
            allocatedMoney.Text = data.Moneyallocated.ToString();
            requiredMoney.Text = data.Moneyrequired.ToString();
            moneyLeft.Text = (data.Moneyrequired - data.Moneyallocated).ToString();
            finishDay.Text = data.Goalexpectedtime.ToString();
            DateTime expected = data.Goalexpectedtime ?? default(DateTime);
            daysLeft.Text = (expected - DateTime.Now).Days.ToString();
            progressBar.Progress = (1 / data.Moneyrequired * data.Moneyallocated);
            completeButton.IsEnabled = data.Moneyallocated == data.Moneyrequired;
        }
        public void NotifyParent()
        {
            if (NotifyParentEvent != null)
            {
                //Raise Event. All the listeners of this event will get a call.
                NotifyParentEvent();
            }
        }

        private async void CompleteButton_Clicked(object sender, EventArgs e)
        {
            var result = await gop.CompleteGoal(goalId);
            if (result)
            {
                await DisplayAlert("", "Income completed", "Ok");
                NotifyParent();
            }

        }

        private void GoBackButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }


    }
}