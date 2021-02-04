using Newtonsoft.Json;
using SmartSaver.DTO.Groups.Input;
using SmartSaver.DTO.Groups.Output;
using SmartSaver.DTO.User.Output;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSaver.Processors
{
    class GroupProcessor
    {
        HttpClient client;
        public GroupProcessor()
        {
            client = new HttpClient();

        }


        public async Task<string> AddUserToGroup(string groupId, string userEmail)
        {
            AddUserToGroupDTO data = new AddUserToGroupDTO { groupId = groupId, userEmail = userEmail };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync("http://194.5.157.98:88/api/Group/AddUserToGroup", stringContent);
                response.EnsureSuccessStatusCode();
                if (response != null)
                {
                    return response.ToString();
                }

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("AddUserToGroup: {0}", ex.ToString()));
            }

            return "Unexpected error";
        }




        public async Task<string> CreateGroup (string userId, string groupName)
        {
            NewGroupDTO data = new NewGroupDTO { userId = userId, groupName = groupName };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("http://194.5.157.98:88/api/Group", stringContent);
                response.EnsureSuccessStatusCode();
                if (response != null)
                {
                    return response.ToString();
                }

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("CreateGroup: {0}", ex.ToString()));
            }

            return "Unexpected error";
        }




        public async Task<List<GroupDTO>> GetGroups (string userId)
        {
            try
            {
                var response = await client.GetAsync(String.Format("http://194.5.157.98:88/api/Group/GetGroups?userId={0}", userId));
                response.EnsureSuccessStatusCode();
                //var responseInfo = response.GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<GroupDTO> groups = JsonConvert.DeserializeObject<List<GroupDTO>>(responseBody);
                    if (groups != null)
                    {
                        return groups;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("GetGroups: {0}", ex.ToString()));
            }

            return new List<GroupDTO>();
        }



        public async Task<List<User>> GetGroupUsers (string groupId)
        {
            try
            {
                var response = await client.GetAsync(String.Format("http://194.5.157.98:88/api/Group/GetGroupUsers?groupId={0}", groupId));
                response.EnsureSuccessStatusCode();
                //var responseInfo = response.GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<User> groupUsers = JsonConvert.DeserializeObject<List<User>>(responseBody);
                    if (groupUsers != null)
                    {
                        return groupUsers;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("GetGroupUsers: {0}", ex.ToString()));
            }

            return new List<User>();
        }



        public async Task<string> RemoveGroup(string groupId)
        {
            try
            {
                var response = await client.DeleteAsync(String.Format("http://194.5.157.98:88/api/Group/RemoveGroup/{0}", groupId));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("RemoveGroup: {0}", ex.ToString()));
            }
            return "Unexpected error";
        }


        public async Task<string> RemoveUserFromGroup(string userId, string groupId)
        {
            try
            {
                var response = await client.DeleteAsync(String.Format("http://194.5.157.98:88/api/Group/RemoveUserFromGroup/{0}&{1}", userId, groupId));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return "Success!";
                }
            }
            catch (Exception ex)
            {

                Logger.Log(string.Format("RemoveUserFromGroup: {0}", ex.ToString()));
            }
            return "Unexpected error";
        }



        public async Task<string> ModifyGroup(string groupId, string newGroupName)
        {
            ModifyGroupDTO data = new ModifyGroupDTO { groupId = groupId, newName = newGroupName };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync("http://194.5.157.98:88/api/Group/ModifyGroup", stringContent);
                response.EnsureSuccessStatusCode();
                if (response != null)
                {
                    return response.ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "Unexpected error";
        }


    }
}
