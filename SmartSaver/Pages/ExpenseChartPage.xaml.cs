using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microcharts;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Entry = Microcharts.Entry;
using SmartSaver.Processors;
using SmartSaver.DTO.Expenses.Output;
using SmartSaver.DTO.Expenses.Input;
using System.Collections.ObjectModel;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseChartPage : ContentPage
    {

        public class EntryForList
        {
            public string Label { get; set; }
            public string ValueLabel { get; set; }
            public string Color { get; set; }
        }



        String[] colors = new string[13] { "#ad84e1", "#1291dc", "#88fead", "#f57e22", "#11f51d", "#330bc3", "#6b3542", "#bcf470", "#fc49ec", "#780997", "#fcdf75", "#191b32", "#b52f19" };
        public DateTime SelectedDate { get; set; }

        public string SelectedType { get; set; }

        ExpensesProcessor exp;

        ObservableCollection<EntryForList> entries;
        ObservableCollection<EntryForList> Entries { get { return entries; } }
        public ExpenseChartPage()
        {
            InitializeComponent();
            exp = new ExpensesProcessor();
            entries = new ObservableCollection<EntryForList>();
            EntriesList.ItemsSource = entries;
            datepicker.DateSelected += delegate (object sender, DateChangedEventArgs args)
            {
                ChartData();
            };
            SelectedType = "Expenses by category";
            ChartData();
        }

        async void ChartData()
        {
            if(SelectedType == "Expenses by category")
            {
                int daysToShow = (DateTime.Now - datepicker.Date).Days;
                List<SumByCatDTO> sumByCatList = await exp.GetSumOfExpensesByCategory(App.ownerId, daysToShow);
                entries.Clear();
                List<Entry> _entries = new List<Entry>();
                List<EntryForList> entriesTemp = new List<EntryForList>();
                int i = 0;
                foreach(var sumByCat in sumByCatList)
                {
                    _entries.Add(new Entry((float)sumByCat.sum) { Color = SKColor.Parse(colors[i]) });
                    entriesTemp.Add(new EntryForList {Label = sumByCat.category, ValueLabel = String.Format("{0:0.00}",sumByCat.sum), Color = colors[i] });
                    i++;
                }
                entriesTemp = new List<EntryForList>(entriesTemp.OrderByDescending(x => float.Parse(x.ValueLabel)));
                foreach(var entry in entriesTemp)
                {
                    entries.Add(entry);
                }
                chartViewPie.Chart = new DonutChart { Entries = _entries, LabelTextSize=40};
            }
            else if(SelectedType == "Expenses by owner")
            {

            }
        }

    }
}