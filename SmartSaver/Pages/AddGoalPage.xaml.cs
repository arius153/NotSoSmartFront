using SmartSaver.DTO.Goal.Input;
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
    public partial class AddGoalPage : ContentPage
    {
        public event NotifyParentDelegate NotifyParentEvent;
        private readonly GoalProcessor gop;
        public AddGoalPage()
        {
            gop = new GoalProcessor();
            InitializeComponent();
            cancelButton.Clicked += delegate (object sender, EventArgs args)
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            };
            
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
           if(string.IsNullOrEmpty(goalName.Text) || string.IsNullOrEmpty(moneyRequired.Text))
            {
                await DisplayAlert("", "Goal info must be filled in!", "ok");
            }
           else
            {
                if (datePicker.Date < DateTime.Now)
                {
                    await DisplayAlert("", "You can not go back in time!", "Ok");
                }
                else
                {
                    var response = await gop.AddGoal(new NewGoalDTO
                    {
                        goalExpectedTime = datePicker.Date,
                        goalName = goalName.Text,
                        moneyRequired = float.Parse(moneyRequired.Text),
                        ownerId = App.ownerId
                    });
                    if (response)
                    {
                        await DisplayAlert("", "Goal Added!", "Ok");
                        NotifyParentEvent();
                    }
                }
            }
        }

        public void NotifyParent()
        {
            if (NotifyParentEvent != null)
            { 
                NotifyParentEvent();
            }
        }
    }
}