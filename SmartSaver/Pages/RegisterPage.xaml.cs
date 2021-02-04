using SmartSaver.DTO.Expenses.Input;
using SmartSaver.DTO.User.Input;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly Regex passwordRegex = new Regex(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$");
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (firstName.Text == null  || lastName.Text == null || firstName.Text == "" || lastName.Text == "")
            {
                await DisplayAlert("", "Please enter a First name and Last name", "Ok");
            }
            else
            {
                if (!IsValidEmail(emailAddress.Text))
                {
                    await DisplayAlert("", "Please enter a valid email address", "Ok");
                }
                else
                {
                    if (password.Text != null && repeatedPassword.Text != null)
                    {
                        if (passwordRegex.IsMatch(password.Text) && password.Text == repeatedPassword.Text)
                        {

                            if (await App._userProcessor.CreateNewUser(new NewUserDTO { userEmail = emailAddress.Text, userLastName = lastName.Text, userName = firstName.Text, userPassword = password.Text }))
                            {
                                await DisplayAlert("", "User created successfully!", "Ok");
                                App.Current.MainPage = new LoginPage();
                            }
                        }

                        else
                        {
                            await DisplayAlert("", "The password must contain at least 1 uppercase, 1 lowercase character, 1 number and 1 special character", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("", "The password must contain at least 1 uppercase, 1 lowercase character, 1 number and 1 special character", "Ok");
                    }

                }
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
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