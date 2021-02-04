using SmartSaver.DTO.Budget.Input;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SmartSaver.Pages.BudgetsPage;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModifyBudgetLimits : ContentPage
    {
        private readonly BudgetProcessor budgetProcessor = new BudgetProcessor();
        public event NotifyParentDelegate NotifyParentEvent;
        List<Entry> listOfEntries;
        public ModifyBudgetLimits(List<SingleBudgetDTO> receivedBudget)
        {
            InitializeComponent();
            food.Text = receivedBudget[0].limit.ToString();
            leisure.Text = receivedBudget[1].limit.ToString();
            rent.Text = receivedBudget[2].limit.ToString();
            loan.Text = receivedBudget[3].limit.ToString();
            alcohol.Text = receivedBudget[4].limit.ToString();
            tobacco.Text = receivedBudget[5].limit.ToString();
            insurance.Text = receivedBudget[6].limit.ToString();
            car.Text = receivedBudget[7].limit.ToString();
            subscriptions.Text = receivedBudget[8].limit.ToString();
            goal.Text = receivedBudget[9].limit.ToString();
            other.Text = receivedBudget[10].limit.ToString();
            clothes.Text = receivedBudget[11].limit.ToString();
            listOfEntries = new List<Entry>() { food, leisure, rent, loan, alcohol, tobacco, insurance, car, subscriptions, goal, other, clothes };
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            foreach (Entry entry in listOfEntries)
            {
                if (string.IsNullOrEmpty(entry.Text))
                {
                    await DisplayAlert("", "Budget info must be filled in!", "Ok");
                    return;
                }
                
                
            }
            List<string> listOfValues = new List<string>();
            for (int i = 0; i < listOfEntries.Count; i++)
            {
                listOfValues.Add(listOfEntries[i].Text);
            }
            budgetProcessor.ModifyBudget(App.ownerId, listOfValues);
            await DisplayAlert("", "Budget modified!", "Ok");
            NotifyParent();
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