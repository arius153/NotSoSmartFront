using SmartSaver.DTO.Income.Input;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SmartSaver.Pages.PageTwo;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddIncomePage : ContentPage
    {
        private readonly IncomeProcessor inc;
        public event NotifyParentDelegate NotifyParentEvent;
        public AddIncomePage()
        {
            InitializeComponent();
            inc = new IncomeProcessor();
            cancelButton.Clicked += delegate (object sender, EventArgs args)
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            };

           
        }
        public async void AddButton_Clicked(object sender, EventArgs args)
        {
           
                if (string.IsNullOrEmpty(incomeName.Text) == false)
                {
                    if (string.IsNullOrEmpty(moneyUsed.Text) == false)
                    {
                        var result = await inc.AddIncome( new NewIncomeDTO{ userId = App.user.Userid, ownerId = App.ownerId, incomeName = incomeName.Text, moneyReceived = float.Parse(moneyUsed.Text)});
                        await DisplayAlert("", "Income Added", "Ok");
                        if (result)
                        {
                             NotifyParent();
                        }
                }
                }
                else
                {
                    await DisplayAlert("", "Income info must be filled in!", "Ok");
                }
            
        }

        public void NotifyParent()
        {
            if (NotifyParentEvent != null)
            {
                NotifyParentEvent();
            }
        }

    }
}