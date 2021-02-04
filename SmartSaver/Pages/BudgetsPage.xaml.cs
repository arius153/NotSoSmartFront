using SmartSaver.DTO.Budget.Input;
using SmartSaver.DTO.Expenses.Input;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartSaver.Pages
{
    [DesignTimeVisible(false)]
    public partial class BudgetsPage : ContentPage
    {
        public delegate void NotifyParentDelegate();
        BudgetProcessor bup;
        ExpensesProcessor exp;
        ObservableCollection<SingleBudgetWithSpentMoneyDTO> budgets;
        public ObservableCollection<SingleBudgetWithSpentMoneyDTO> Budgets { get { return budgets; } }
        public BudgetsPage()
        {
            InitializeComponent();
            bup = new BudgetProcessor();
            exp = new ExpensesProcessor();
            budgets = new ObservableCollection<SingleBudgetWithSpentMoneyDTO>();
            BudgetssList.ItemsSource = budgets;
            BudgetData();
        }

        private async void BudgetData()
        {
            var _budgets = await bup.GetValuesOfCategoryLimits(App.ownerId);
            var _expensesByCategory = await exp.GetSumOfExpensesByCategory(App.ownerId, -1);
            budgets.Clear();
            for (int i = 0; i < _budgets.Count; i++)
            {
                budgets.Add(new SingleBudgetWithSpentMoneyDTO { category = _budgets[i].category, limit = _budgets[i].limit, spent = _expensesByCategory[i].sum });
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var _budgets = await bup.GetValuesOfCategoryLimits(App.ownerId);
            var modifyBudgetPage = new ModifyBudgetLimits(_budgets);
            modifyBudgetPage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
            var navModifyExpensePage = new NavigationPage(modifyBudgetPage);
            await Application.Current.MainPage.Navigation.PushModalAsync(navModifyExpensePage);
        }
        void _child_NotifyParentEvent()
        {
            BudgetData();
        }
    }
    }

