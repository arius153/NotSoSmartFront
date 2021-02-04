using SmartSaver.DTO.User.Input;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text;
using SmartSaver.DTO.User.Output;

namespace SmartSaver.Processors
{

    

    public class UserProcessor
    {
        private static readonly HttpClient _client = new HttpClient();

        //public UserProcessor(HttpClient httpClient)
        //{
        //    _client = httpClient;
        //}

        public async Task<bool> CreateNewUser(NewUserDTO data)
        {
            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync("http://194.5.157.98:88/api/User", httpContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("CreateNewUser: {0}",ex.ToString()));
            }
            return false;
        }

        public async Task<User> UserLogin(string userEmail, string userPassword)
        {
            try
            {
                string url = string.Format("http://194.5.157.98:88/api/User?email={0}&password={1}", userEmail, userPassword);
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    User user = JsonConvert.DeserializeObject<User>(responseBody);
                    return user;
                }
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("UserLogin: {0}", ex.ToString()));
            }
            return null;
        }

        public async Task<bool> ModifyUser(ModifyUserDTO data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, "http://194.5.157.98:88/api/User/ModifyUser");
                message.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(message);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    return true;
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("ModifyUser: {0}", ex.ToString()));
            }
            return false;
        }
        //public async Task<bool> ChangeUserPassword(ChangePasswordDTO data)
        //{
        //    try
        //    {
        //        string json = JsonConvert.SerializeObject(data);
        //        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, "http://194.5.157.98:88/api/User/ModifyUser");
        //        message.Content = new StringContent(json, Encoding.UTF8, "application/json");
        //        var response = await _client.SendAsync(message);
        //        response.EnsureSuccessStatusCode();
        //        if (response.IsSuccessStatusCode)
        //            return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(string.Format("ModifyUser: {0}", ex.ToString()));
        //    }
        //    return false;
        //}

        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                string url = string.Format("http://194.5.157.98:88/api/User/{0}", userId);
                var response = await _client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("DeleteUser: {0}", ex.ToString()));
            }
            return false;
        }
    }
}
