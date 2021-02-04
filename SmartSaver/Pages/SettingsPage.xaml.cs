using SmartSaver.Processors;
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
    public partial class SettingsPage : ContentPage
    {
        public delegate void NotifyParentDelegate();

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void EditClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new EditPage();
        }
        private void EditPassClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new EditPassPage();
        }
    }
}