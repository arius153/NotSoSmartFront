using SmartSaver.DTO.Expenses.Output;
using SmartSaver.DTO.Income.Output;
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
    public partial class MainPage : ContentPage
    {
        ExpensesProcessor exp = new ExpensesProcessor();
        IncomeProcessor inp = new IncomeProcessor();
        ObservableCollection<ExpenseDTO> expenses;
        ObservableCollection<IncomeDTO> incomes;
        ObservableCollection<ExpenseDTO> Expenses { get { return expenses; } }
        ObservableCollection<IncomeDTO> Incomes { get { return incomes; } }
        public MainPage()
        {
            InitializeComponent();
            expenses = new ObservableCollection<ExpenseDTO>();
            incomes = new ObservableCollection<IncomeDTO>();
            lastFiveExpenses.ItemsSource = expenses;
            lastFiveIncomes.ItemsSource = incomes;

            MainData();
        }


        private async void MainData()
        {

            var expenses = await exp.GetExpenses(App.ownerId, -1, -1);
            var incomes = await inp.GetIncomes(App.ownerId, -1, -1);
            float esum = 0;
            float isum = 0;
            foreach(var expense in expenses)
            {
                esum += expense.Moneyused;
            }
            foreach(var income in incomes)
            {
                isum += income.Moneyrecieved;
            }

            expenses = new List<ExpenseDTO>(expenses.OrderByDescending(e => e.Expensetime));
            incomes = new List<IncomeDTO>(incomes.OrderByDescending(e => e.Incometime));

            this.expenses.Clear();
            foreach(var expense in expenses)
            {
                if(this.expenses.Count<5) this.expenses.Add(expense);
            }
            this.incomes.Clear();
            foreach (var income in incomes)
            {
                if (this.incomes.Count < 5) this.incomes.Add(income);
            }
            yourMoney.Text = String.Format("Current money: {0:0.00}$", isum - esum);
        }
    }

}
