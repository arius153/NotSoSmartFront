using Newtonsoft.Json;
using SmartSaver.DTO.Budget.Input;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSaver.Processors
{
    class BudgetProcessor
    {
        HttpClient client;
        public BudgetProcessor()
        {
            client = new HttpClient();
        }


        public async Task<List<SingleBudgetDTO>> GetValuesOfCategoryLimits(string ownerId)
        {
            try
            {
                var response = await client.GetAsync(String.Format("http://194.5.157.98:88/api/Budget/GetValuesOfCategoryLimits?ownerId={0}", ownerId));
                //var responseInfo = response.GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<SingleBudgetDTO> budgetLimits = JsonConvert.DeserializeObject<List<SingleBudgetDTO>>(responseBody);
                    if (budgetLimits != null)
                    {
                        return budgetLimits;
                    }
                }

                response.EnsureSuccessStatusCode();

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("BudgetGetLimits: {0}", ex.ToString()));
            }

            return new List<SingleBudgetDTO>();
        }



        public async Task<string> ModifyBudget(string ownerId, List<string> limits)
        {
            ModifyBudgetDTO data = new ModifyBudgetDTO { ownerId = ownerId, listOfValues = limits };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync("http://194.5.157.98:88/api/Budget/ModifyBudget", stringContent);
                response.EnsureSuccessStatusCode();
                if (response != null)
                return response.ToString();

                

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("ModifyBudget: {0}", ex.ToString()));               
            }

            return "Unexpected error";
        }

    }
}
