using SmartSaver.DTO.User.Output;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly UserProcessor userProcessor = new UserProcessor();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void loginClick (object sender, EventArgs args)
        {
            if (email.Text == null || email.Text == "" || password.Text == null || password.Text == "")
            {
                await DisplayAlert("", "Enter email and password!", "Ok");
            }
            else
            {
                User user = await userProcessor.UserLogin(email.Text, password.Text);
                if (user != null)
                {
                    App.user = user;
                    App.ownerId = user.Userid;
                    App.Current.MainPage = new MainMenu();
                }
                else
                {
                    await DisplayAlert("", "Email or password is incorrect!", "Ok");
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new RegisterPage();
        }
    }
}