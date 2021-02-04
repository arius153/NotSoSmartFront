using SmartSaver.DTO.Goal.Output;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SmartSaver.Pages
{
    [DesignTimeVisible(false)]
    public partial class GoalsPage : ContentPage
    {
        public delegate void NotifyParentDelegate();

        GoalProcessor gop;
        ObservableCollection<GoalDTO> goals;
        public ObservableCollection<GoalDTO> Goals { get { return goals; } }

        public GoalsPage()
        {
            InitializeComponent();
            gop = new GoalProcessor();
            goals = new ObservableCollection<GoalDTO>();
            GoalsList.ItemsSource = goals;
            GoalData();

        }

        public async void GoalData()
        {

            var _goals = await gop.GetGoals(App.ownerId);
            goals.Clear();
            foreach (var goal in _goals)
            {
                goals.Add(goal);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            var addGoalPage = new AddGoalPage();
            addGoalPage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
            var navAddGoalPage = new NavigationPage(addGoalPage);
            await Application.Current.MainPage.Navigation.PushModalAsync(navAddGoalPage);
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (GoalsList.SelectedItem != null)
            {
                var result = await gop.DeleteGoal(((GoalDTO)GoalsList.SelectedItem).Goalid);
                await DisplayAlert("", "Goal Delted", "Ok");
                GoalData();
            }
            else
            {
                await DisplayAlert("", "Please select an expense to remove", "Ok...");
            }
        }
        private async void ModifyButton_Clicked(object sender, EventArgs e)
        {
            if (GoalsList.SelectedItem != null)
            {
                var modifyGoalPage = new ModifyGoalPage((GoalDTO)GoalsList.SelectedItem);
                modifyGoalPage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
                var navModifyGoalPage = new NavigationPage(modifyGoalPage);
                await Application.Current.MainPage.Navigation.PushModalAsync(navModifyGoalPage);
                GoalsList.SelectedItem = null;
            }
            else
            {
                await DisplayAlert("", "Please select an expense to modify", "Ok!");
            }
        }
        private async void ViewButton_Clicked(object sender, EventArgs e)
        {
            if (GoalsList.SelectedItem != null)
            {
                var viewGoalPage = new ViewGoalPage((GoalDTO)GoalsList.SelectedItem);
                viewGoalPage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
                var navViewGoalPage = new NavigationPage(viewGoalPage);
                await Application.Current.MainPage.Navigation.PushModalAsync(navViewGoalPage);
            }
            else
            {
                await DisplayAlert("", "Please select an expense to modify", "Ok!");
            }
        }
        void _child_NotifyParentEvent()
        {
            GoalData();
        }

        private async void MoneyButton_Clicked(object sender, EventArgs e)
        {
            if (GoalsList.SelectedItem != null)
            {
                var addMoneyPage = new AddMoneyPage((GoalDTO)GoalsList.SelectedItem);
                addMoneyPage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
                var navAddMoneyPage = new NavigationPage(addMoneyPage);
                await Application.Current.MainPage.Navigation.PushModalAsync(navAddMoneyPage);
            }
            else
            {
                await DisplayAlert("", "Please select an expense to modify", "Ok!");
            }
        }
    }

}



