using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SmartSaver.Pages.PageOne;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePage : ContentPage
    {

        ExpensesProcessor exp;
        public class Category
        {
            public string CategoryName { get; set; }
            public int CategoryInt { get; set; }
        }


        IList<Category> categories;
        public IList<Category> Categories { get { return categories; } }
        public event NotifyParentDelegate NotifyParentEvent;
        public AddExpensePage()
        {
            InitializeComponent();

            exp = new ExpensesProcessor();
            int i = 0;
            categories = new List<Category>();

            foreach(var category in Enum.GetNames(typeof(CategoryEnum)))
            {
                categories.Add(new Category { CategoryName = category, CategoryInt = i });
                i++;
            }
            expenseCategory.ItemsSource = (System.Collections.IList)categories;


        }


        public async void AddButton_Clicked (object sender, EventArgs args)
        {
            if(expenseCategory.SelectedItem!=null)
            {
                if(string.IsNullOrEmpty(expenseName.Text) == false)
                {
                    if(string.IsNullOrEmpty(moneyUsed.Text) == false)
                    {
                        var result = await exp.AddExpense(App.user.Userid, App.ownerId, expenseName.Text, float.Parse(moneyUsed.Text), ((Category)expenseCategory.SelectedItem).CategoryInt);
                        await DisplayAlert("", result, "Ok");
                        if(result == "Expense added")
                        {
                            NotifyParent();
                        }

                    }
                }
                else
                {
                    await DisplayAlert("", "Expense info must be filled in!", "Ok");
                }
            }
            else
            {
                await DisplayAlert("", "A category must be selected!", "Ok");
            }
        }

        public void CancelButton_Clicked (object sender, EventArgs args)
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