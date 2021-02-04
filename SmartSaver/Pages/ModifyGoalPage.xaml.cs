using SmartSaver.DTO.Goal.Input;
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
    public partial class ModifyGoalPage : ContentPage
    {
        public event NotifyParentDelegate NotifyParentEvent;
        private readonly GoalProcessor gop = new GoalProcessor();
        string goalIds;
        public ModifyGoalPage(GoalDTO data)
        {
            InitializeComponent();
            modGoalName.Text = data.Goalname;
            modMoneyRequired.Text = data.Moneyrequired.ToString();
            datePicker.Date = data.Goalexpectedtime ?? default(DateTime);
            goalIds = data.Goalid;

        }

        private async void ModifyButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(modGoalName.Text) || string.IsNullOrEmpty(modMoneyRequired.Text))
            {
                await DisplayAlert("", "Goal info must be filled!", "Ok");
            }
            else
            {
                if (datePicker.Date < DateTime.Now)
                {
                    await DisplayAlert("", "You can not go back in time!", "Ok");
                }
                else
                {
                    var result = await gop.ModifyGoal(new ModifyGoalDTO
                    {
                        goalId = goalIds,
                        newExpectedTime = datePicker.Date,
                        newGoalName = modGoalName.Text,
                        newMoneyRequired = float.Parse(modMoneyRequired.Text)
                    }) ;
                    if (result)
                    {
                        await DisplayAlert("", "Goal modified!", "Ok");
                        NotifyParent();
                    }
                    else
                    {
                        await DisplayAlert("", "Goal was not modified!", "Ok");
                    }
                }
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
        public void NotifyParent()
        {
            if (NotifyParentEvent != null)
            {
                //Raise Event. All the listeners of this event will get a call.
                NotifyParentEvent();
            }
        }
    }
}