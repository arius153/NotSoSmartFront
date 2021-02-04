using SmartSaver.DTO.User.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        private readonly Regex passwordRegex = new Regex(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$");
        public EditPage()
        {
            InitializeComponent();
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            if (firstName.Text == null || firstName.Text == "")
            {
                await DisplayAlert("", "Please enter a First name", "Ok");
            }
            else
            {
                if (!IsValidEmail(emailAddress.Text))
                {
                    await DisplayAlert("", "Please enter a valid email address", "Ok");
                }
                else
                {
                    if (await App._userProcessor.ModifyUser(new ModifyUserDTO { userId = App.user.Userid, newUserName = firstName.Text,  newUserEmail = emailAddress.Text}))
                    {
                        await DisplayAlert("", "User created successfully!", "Ok");
                        App.Current.MainPage = new SettingsPage();
                    }
                }
            }
        }
        private void CancelClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new SettingsPage();
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}