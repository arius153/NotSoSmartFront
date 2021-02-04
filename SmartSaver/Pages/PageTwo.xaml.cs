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
    public partial class PageTwo : ContentPage
    {
        public delegate void NotifyParentDelegate();
        IncomeProcessor inc;
        ObservableCollection<IncomeDTO> incomes;
        private Lazy<List<IncomeDTO>> _items = new Lazy<List<IncomeDTO>>();
        public List<IncomeDTO> items
        {
            get { return _items.Value; }
        }
        public ObservableCollection<IncomeDTO> Incomes { get { return incomes; } }
        public PageTwo()
        {
            InitializeComponent();
            inc = new IncomeProcessor();
            incomes = new ObservableCollection<IncomeDTO>();
            IncomesList.ItemsSource = incomes;
            IncomeData();
        }

        private async void IncomeData()
        {
            int daysToShow = (DateTime.Now - datepicker.Date).Days;
            

            IEnumerable<IncomeDTO> _incomes = await inc.GetIncomes(App.ownerId, daysToShow, -1);
            Incomes.Clear();
            _incomes.Reverse();
            foreach (var income in _incomes)
            {
                incomes.Add(income);
            }
        }

        private async void datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            
            var _incomes = await inc.GetIncomes(App.ownerId, (DateTime.Now-datepicker.Date).Days, -1);
            incomes.Clear();
            _incomes.Reverse();
            foreach (var income in _incomes)
            {
                incomes.Add(income);
            }

        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            
            var addIncomePage = new AddIncomePage();
            addIncomePage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
            var navAddIncomePage = new NavigationPage(addIncomePage);
            await Application.Current.MainPage.Navigation.PushModalAsync(navAddIncomePage);
        }
    

        public async void RemoveButton_Clicked(object sender, EventArgs args)
        {
            if (IncomesList.SelectedItem != null)
            {
                var result = await inc.DeletIncome(((IncomeDTO)IncomesList.SelectedItem).Incomeid);
                await DisplayAlert("", "Income deleted", "Ok");
                IncomeData();
            }
            else
            {
                await DisplayAlert("", "Please select an expense to remove", "Ok...");
            }
        }

        public async void ModifyButton_Clicked(object sender, EventArgs args)
        {

            if (IncomesList.SelectedItem != null)
            {
                var modifyIncomePage = new ModifyIncomePage((IncomeDTO)IncomesList.SelectedItem);
                modifyIncomePage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
                var navModifyExpensePage = new NavigationPage(modifyIncomePage);
                await Application.Current.MainPage.Navigation.PushModalAsync(navModifyExpensePage);
            }
            else
            {
                await DisplayAlert("", "Please select an income to modify", "Ok!");
            }
        }
        void _child_NotifyParentEvent()
        {
            IncomeData();
        }

    }

}
