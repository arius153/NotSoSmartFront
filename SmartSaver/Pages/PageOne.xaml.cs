using SmartSaver.DTO.Expenses.Output;
﻿using SmartSaver.DTO.User.Output;
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
    public partial class PageOne : ContentPage
    {
        public delegate void NotifyParentDelegate();

        ExpensesProcessor exp;
        ObservableCollection<ExpenseDTO> expenses;
        public ObservableCollection<ExpenseDTO> Expenses { get { return expenses; } }
        public PageOne()
        {
            InitializeComponent();
            exp = new ExpensesProcessor();
            expenses = new ObservableCollection<ExpenseDTO>();
            ExpensesList.ItemsSource = expenses;
            ExpenseData();
       
        }


        private async void ExpenseData()
        {
            int daysToShow = (DateTime.Now - datepicker.Date).Days;
            var _expenses = await exp.GetExpenses(App.ownerId, daysToShow, -1);
            expenses.Clear();
            foreach(var expense in _expenses)
            {
                expenses.Add(expense);
            }
        }

        private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var now = DateTime.Now;
            var smf = DateTime.Now - datepicker.Date;
            var _expenses = await exp.GetExpenses(App.ownerId, smf.Days, -1);
            expenses.Clear();
            foreach(var expense in _expenses)
            {
                expenses.Add(expense);
            }
        }


        public async void AddButton_Clicked(object sender, EventArgs args)
        {
            var addExpensePage = new AddExpensePage();
            addExpensePage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
            var navAddExpensePage = new NavigationPage(addExpensePage);
            await Application.Current.MainPage.Navigation.PushModalAsync(navAddExpensePage);
        }


        public async void RemoveButton_Clicked(object sender, EventArgs args)
        {
            if(ExpensesList.SelectedItem!=null)
            {
                var result = await exp.RemoveExpense(((ExpenseDTO)ExpensesList.SelectedItem).Expenseid);
                await DisplayAlert("", result, "Ok");
                ExpenseData();
            }
            else
            {
                await DisplayAlert("", "Please select an expense to remove", "Ok...");
            }
        }

        public async void ModifyButton_Clicked(object sender, EventArgs args)
        {
            
            if (ExpensesList.SelectedItem != null)
            {
                var modifyExpensePage = new ModifyExpensePage((ExpenseDTO)ExpensesList.SelectedItem);
                modifyExpensePage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
                var navModifyExpensePage = new NavigationPage(modifyExpensePage);
                await Application.Current.MainPage.Navigation.PushModalAsync(navModifyExpensePage);
            }
            else
            {
                await DisplayAlert("", "Please select an expense to modify", "Ok!");
            }
        }


        public async void ChartsButton_Clicked(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ExpenseChartPage()));
        }

        void _child_NotifyParentEvent()
        {
            ExpenseData();
        }


    }
}