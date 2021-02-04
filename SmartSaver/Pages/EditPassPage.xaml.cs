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
    public partial class EditPassPage : ContentPage
    {
        private readonly Regex passwordRegex = new Regex(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$");
        public EditPassPage()
        {
            InitializeComponent();
        }
        //private async void SaveClicked(object sender, EventArgs e)
        //{
        //    if (CurrentPassword.Text == null)
        //    {
        //        await DisplayAlert("", "Please enter the current password", "Ok");
        //    }
        //    else
        //    {
        //        if(CurrentPassword.Text == App.user.Userpassword)
        //        {
        //            if (password.Text != null && repeatedPassword.Text != null)
        //            {
        //                if (passwordRegex.IsMatch(password.Text) && password.Text == repeatedPassword.Text)
        //                {

        //                    if (await App._userProcessor.ChangeUserPassword(new ChangePasswordDTO { userId = App.user.Userid, userNewPassword = password.Text }))
        //                    {
        //                        await DisplayAlert("", "User created successfully!", "Ok");
        //                        App.Current.MainPage = new LoginPage();
        //                    }
        //                }

        //                else
        //                {
        //                    await DisplayAlert("", "The password must contain at least 1 uppercase, 1 lowercase character, 1 number and 1 special character", "Ok");
        //                }
        //            }
        //            else
        //            {
        //                await DisplayAlert("", "No new password was entered", "Ok");
        //            }
        //        }
        //        else
        //        {
        //            await DisplayAlert("", "Current password is incorrect", "Ok");
        //        }
        //    }
        //}
        private void CancelClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new SettingsPage();
        }
        private void SaveClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new SettingsPage();
        }
    }
}