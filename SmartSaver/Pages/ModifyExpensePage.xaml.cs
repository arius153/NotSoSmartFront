using SmartSaver.DTO.Expenses.Output;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SmartSaver.Pages.AddExpensePage;
using static SmartSaver.Pages.PageOne;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModifyExpensePage : ContentPage
    {
        ExpensesProcessor exp = new ExpensesProcessor();
        public event NotifyParentDelegate NotifyParentEvent;
        IList<Category> categories;
        private readonly string expenseId;
        public IList<Category> Categories { get { return categories; } }
        public ModifyExpensePage(ExpenseDTO selectedItem)
        {
            InitializeComponent();
            expenseId = selectedItem.Expenseid;
            int i = 0;
            categories = new List<Category>();

            cancelButton.Clicked += delegate (object sender, EventArgs args)
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            };


            foreach (var category in Enum.GetNames(typeof(CategoryEnum)))
            {
                categories.Add(new Category { CategoryName = category, CategoryInt = i });
                i++;
            }
            modExpenseCategory.ItemsSource = (System.Collections.IList)categories;
            this.modExpenseCategory.SelectedItem = selectedItem.Expensecategory;
            this.modMoneyUsed.Text = (selectedItem.Moneyused).ToString();
            this.modExpenseName.Text = selectedItem.Expensename;
            
            
        }


        public async void ModifyButton_Clicked(object sender, EventArgs args)
        {

            if (modExpenseCategory.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(modExpenseName.Text) == false)
                {
                    if (string.IsNullOrEmpty(modMoneyUsed.Text) == false)
                    {
                        var result = await exp.ModifyExpense(expenseId, modExpenseName.Text, float.Parse(modMoneyUsed.Text), ((Category)modExpenseCategory.SelectedItem).CategoryInt);
                        await DisplayAlert("", "Expense modified", "Ok");
                        NotifyParent();

                        //Application.Current.MainPage.Navigation.PopModalAsync();

                    }
                }
                else
                {
                    await DisplayAlert("", "Expense info must be filled in!", "Ok");
                }
            }
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