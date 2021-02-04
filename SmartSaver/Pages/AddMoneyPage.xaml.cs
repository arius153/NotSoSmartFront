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
    public partial class AddMoneyPage : ContentPage
    {
        private readonly GoalProcessor gop;
        private readonly GoalDTO goalToAdd;
        public event NotifyParentDelegate NotifyParentEvent;
        public AddMoneyPage(GoalDTO goal)
        {
            gop = new GoalProcessor();
            InitializeComponent();
            moneyReceived.Text = 0.ToString();
            goalToAdd = goal;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(moneyReceived.Text))
            {
                await DisplayAlert("", "You can not add nothing!", "Ok");
            }
            else
            {
                var result = await gop.AddMoneyToGoal(new AddMoneyDTO
                {
                    goalAllocatedMoney = goalToAdd.Moneyallocated,
                    moneyToAdd = float.Parse(moneyReceived.Text),
                    goalId = goalToAdd.Goalid,
                    goalName = goalToAdd.Goalname,
                    goalRequiredMoney = goalToAdd.Moneyrequired,
                    userId = App.ownerId
                });
                if (result)
                {
                    await DisplayAlert("", "Money Added!", "Ok");
                    NotifyParent();
                }
                else
                {
                    await DisplayAlert("", "Too much money!", "Ok");
                }
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
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