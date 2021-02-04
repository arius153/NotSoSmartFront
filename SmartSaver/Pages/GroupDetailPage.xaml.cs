using SmartSaver.DTO.Groups.Output;
using SmartSaver.DTO.User.Output;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SmartSaver.Pages.GroupPage;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupDetailPage : ContentPage
    {
        GroupDTO group;
        ObservableCollection<User> users;
        public event NotifyParentDelegate NotifyParentEvent;
        GroupProcessor grp;
        public ObservableCollection<User> Users { get { return users; } }
        public GroupDetailPage(GroupDTO group)
        {
            InitializeComponent();
            this.BindingContext = this;
            users = new ObservableCollection<User>();
            grp = new GroupProcessor();
            UserList.ItemsSource = users;
            this.group = group;
            UsersData();
        }

        public async void UsersData()
        {
            users.Clear();
            List<User> _users = await grp.GetGroupUsers(group.Groupid);
            foreach(var user in _users)
            {
                users.Add(user);
            }
        }

        public async void RemoveUserButton_Clicked(object sender, EventArgs args)
        {
            if(UserList.SelectedItem != null)
            {
                var result = await grp.RemoveUserFromGroup(((User)UserList.SelectedItem).Userid, group.Groupid);
                if(((User)UserList.SelectedItem).Userid != App.user.Userid)
                {
                    await DisplayAlert("", result, "Ok");
                    UsersData();
                }
                else
                {
                    if(App.ownerId == group.Groupid)
                    {
                        App.ownerId = App.user.Userid;
                    }
                    NotifyParent();
                    Application.Current.MainPage.Navigation.PopModalAsync();
                }
                
            }
            else
            {
                await DisplayAlert("", "You need to select a user to remove", "Ok");
            }
        }



        public async void DeleteButton_Clicked(object sender, EventArgs args)
        {
            await grp.RemoveGroup(group.Groupid);
            if(App.ownerId == group.Groupid)
            {
                App.ownerId = App.user.Userid;
            }
            NotifyParent();
            Application.Current.MainPage.Navigation.PopModalAsync();
        }


        public async void AddUserButton_Clicked(object sender, EventArgs args)
        {
            string result = await DisplayPromptAsync("", "Enter the user's email");
            if (string.IsNullOrEmpty(result))
            {
                await DisplayAlert("", "Email must be entered!", "Ok");
            }
            else
            {
                var response = await grp.AddUserToGroup(group.Groupid, result);
                await DisplayAlert("", response, "Ok");
                UsersData();
            }
        }


        public async void ModifyGroupButton_Clicked(object sender, EventArgs args)
        {
            string result = await DisplayPromptAsync("", "Enter new group name");
            if (string.IsNullOrEmpty(result))
            {
                await DisplayAlert("", "New name must be entered!", "Ok");
            }
            else
            {
                var response = await grp.ModifyGroup(group.Groupid, result);
                await DisplayAlert("", response, "Ok");
                NotifyParent();
                Application.Current.MainPage.Navigation.PopModalAsync();

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