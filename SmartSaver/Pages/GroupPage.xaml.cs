using SmartSaver.DTO.Groups.Output;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartSaver.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        public delegate void NotifyParentDelegate();
        GroupProcessor grp;
        ObservableCollection<GroupDTO> groups;
        public ObservableCollection<GroupDTO> Groups { get { return groups; } }
        public GroupPage()
        {
            InitializeComponent();
            grp = new GroupProcessor();
            groups = new ObservableCollection<GroupDTO>();
            GroupList.ItemsSource = groups;
            GroupsData();
        }


        public async void GroupsData()
        {
            groups.Clear();
            List<GroupDTO> _groups = await grp.GetGroups(App.user.Userid);
            foreach(var group in _groups)
            {
                groups.Add(group);
            }
        }


        public async void EnterButton_Clicked(object sender, EventArgs args)
        {
            var selectedItem = (GroupDTO)((Button)sender).CommandParameter;
            if (App.ownerId != selectedItem.Groupid)
            {
                App.ownerId = selectedItem.Groupid;
                await DisplayAlert("", "You are now using this group", "Ok");
            }
            else await DisplayAlert("", "Already using this group", "Ok");
        }


        public async void DetailsButton_Clicked(object sender, EventArgs args)
        {
            var selectedItem = (GroupDTO)((Button)sender).CommandParameter;
            var groupDetailPage = new GroupDetailPage(selectedItem);
            groupDetailPage.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
            var navGroupDetailPage = new NavigationPage(groupDetailPage);
            await Application.Current.MainPage.Navigation.PushModalAsync(navGroupDetailPage);

        }



        public async void CreateButton_Clicked(object sender, EventArgs args)
        {
            string result = await DisplayPromptAsync("", "Enter a name for the group");
            if(string.IsNullOrEmpty(result))
            {
                await DisplayAlert("", "Group name must be entered!", "Ok");
            }
            else
            {
                await grp.CreateGroup(App.user.Userid, result);
                await DisplayAlert("", "Group created!", "Ok");
                GroupsData();
            }
        }


        void _child_NotifyParentEvent()
        {
            GroupsData();
        }

    }
}