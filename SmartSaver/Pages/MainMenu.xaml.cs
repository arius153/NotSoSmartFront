using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SmartSaver.Models;

namespace SmartSaver.Pages
{
    public partial class MainMenu : MasterDetailPage
    {
        public List<MainMenuItem> MainMenuItems { get; set; }

        public MainMenu()
        {
            // Set the binding context to this code behind.
            BindingContext = this;

            // Build the Menu
            MainMenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "Main Page", Icon = "home.png", TargetType = typeof(MainPage)},
                new MainMenuItem() { Title = "Expenses", Icon = "expense.png", TargetType = typeof(PageOne) },
                new MainMenuItem() { Title = "Incomes", Icon = "incomes.png", TargetType = typeof(PageTwo) },
                new MainMenuItem() { Title = "Goals", Icon = "goals.png", TargetType = typeof(GoalsPage) },
                new MainMenuItem() { Title = "Budgets", Icon = "budgets.png", TargetType = typeof(BudgetsPage) },
                new MainMenuItem() { Title = "Groups", Icon = "budgets.png", TargetType = typeof(GroupPage) },
                new MainMenuItem() { Title = "Settings", Icon = "settings.png", TargetType = typeof(SettingsPage)},
                new MainMenuItem() { Title = "Logs", Icon = "budgets.png", TargetType = typeof(LogsPage)}
            };

            // Set the default page, this is the "home" page.
            Detail = new NavigationPage(new MainPage());

            InitializeComponent();
        }

        // When a MenuItem is selected.
        public void MainMenuItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuItem;
            if (item != null)
            {
                if (item.Title.Equals("Main Page"))
                {
                    Detail = new NavigationPage(new MainPage());
                }
                else if (item.Title.Equals("Expenses"))
                {
                    Detail = new NavigationPage(new PageOne());
                }
                else if (item.Title.Equals("Incomes"))
                {
                    Detail = new NavigationPage(new PageTwo());
                }
                else if (item.Title.Equals("Goals"))
                {
                    Detail = new NavigationPage(new GoalsPage());
                }
                else if (item.Title.Equals("Budgets"))
                {
                    Detail = new NavigationPage(new BudgetsPage());
                }
                else if (item.Title.Equals("Groups"))
                {
                    Detail = new NavigationPage(new GroupPage());
                }
                else if (item.Title.Equals("Logs"))
                {
                    Detail = new NavigationPage(new LogsPage());
                }
                else if (item.Title.Equals("Settings"))
                {
                    Detail = new NavigationPage(new SettingsPage());
                }

                MenuListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}