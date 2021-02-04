using Newtonsoft.Json;
using SmartSaver.DTO.Income.Input;
using SmartSaver.DTO.Income.Output;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSaver.Processors
{
    class IncomeProcessor
    {
        public async Task<bool> AddIncome(NewIncomeDTO data)
        {
            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await App.client.PostAsync("http://194.5.157.98:88/api/Income", httpContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("AddIncome: {0}", ex.ToString()));
            }
            return false;
        }
        public async Task<bool> DeletIncome(string incomeId)
        {
            string url = string.Format("http://194.5.157.98:88/api/Income/{0}", incomeId);
            try
            {
                var response = await App.client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("DelteIncome: {0}", ex.ToString()));
            }
            return false;
        }
        public async Task<bool> ModifyIncome(ModifyIncomeDTO data)
        {
            string json = JsonConvert.SerializeObject(data);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, "http://194.5.157.98:88/api/Income");
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await App.client.SendAsync(message);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    return true;
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("ModifyIncome: {0}", ex.ToString()));
            }
            return false;
        }

        public async Task<List<IncomeDTO>> GetIncomes(string ownerId, int numberOfDaysToShow, int maxNumberOfExpensesToShow)
        {

            try
            {
                var response = await App.client.GetAsync(String.Format("http://194.5.157.98:88/api/Income/GetAllIncomes?ownerId={0}&numberOfDaysToShow={1}&maxNumberOfIncomesToShow={2}", ownerId, numberOfDaysToShow, maxNumberOfExpensesToShow));
                //var responseInfo = response.GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<IncomeDTO> incomes = JsonConvert.DeserializeObject<List<IncomeDTO>>(responseBody);
                    if (incomes != null)
                    {
                        return incomes;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("GetIncomes: {0}", ex.ToString()));
            }

            return new List<IncomeDTO>();

        }
        
        public async Task<List<IncomeSumByOwnerDTO>> GetSumOfIncomesByOwner(string ownerId, int numberOfDaysToShow)
        {

            try
            {
                var response = await App.client.GetAsync(String.Format("http://194.5.157.98:88/api/Income/GetSumOfIncomesByOwner?ownerId={0}&numberOfDaysToShow={1}", ownerId, numberOfDaysToShow));
                //var responseInfo = response.GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<IncomeSumByOwnerDTO> incomesByOwner = JsonConvert.DeserializeObject<List<IncomeSumByOwnerDTO>>(responseBody);
                    if (incomesByOwner != null)
                    {
                        return incomesByOwner;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("GetSumOfIncomesByOwner: {0}", ex.ToString()));
            }

            return new List<IncomeSumByOwnerDTO>();
        }

    }
}
