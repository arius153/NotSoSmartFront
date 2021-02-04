using Newtonsoft.Json;
using SmartSaver.DTO.Expenses.Input;
using SmartSaver.DTO.Expenses.Output;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSaver.Processors
{
    class ExpensesProcessor
    {

        HttpClient client;

        public ExpensesProcessor()
        {
            client = new HttpClient();
        }

        public async Task<string> AddExpense(string userId, string ownerId, string expenseName, float moneyUsed, int expenseCategory)
        {

            NewExpenseDTO data = new NewExpenseDTO { userId = userId, ownerId = ownerId, expenseName = expenseName, moneyUsed = moneyUsed, expenseCategory = expenseCategory};
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("http://194.5.157.98:88/api/Expenses", stringContent);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return responseBody;

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("AddExpense: {0}", ex.ToString()));
            }

            return "Unexpected error";

        }



        public async Task<List<ExpenseDTO>> GetExpenses(string ownerId, int numberOfDaysToShow, int maxNumberOfExpensesToShow)
        {

            try
            {
                var response = await client.GetAsync(String.Format("http://194.5.157.98:88/api/Expenses/GetExpenses?ownerId={0}&numberOfDaysToShow={1}&maxNumberOfExpensesToShow={2}", ownerId, numberOfDaysToShow, maxNumberOfExpensesToShow));
                //var responseInfo = response.GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<ExpenseDTO> expenses = JsonConvert.DeserializeObject<List<ExpenseDTO>>(responseBody);
                    if (expenses != null)
                    {
                        return expenses;
                    }

                
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("GetExpenses: {0}", ex.ToString()));
            }

            return new List<ExpenseDTO>();

        }


        public async Task<List<SumByCatDTO>> GetSumOfExpensesByCategory(string ownerId, int numberOfDaysToShow)
        {
            try
            {
                var response = await client.GetAsync(String.Format("http://194.5.157.98:88/api/Expenses/GetSumOfExpensesByCategory?ownerId={0}&numberOfDaysToShow={1}", ownerId, numberOfDaysToShow));
                //var responseInfo = response.GetAwaiter().GetResult();

                response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<SumByCatDTO> expensesByCat = JsonConvert.DeserializeObject<List<SumByCatDTO>>(responseBody);
                    if (expensesByCat != null)
                    {
                        return expensesByCat;
                    }
                
            }
            catch (Exception ex)
            {

                Logger.Log(string.Format("GetSumOfExpensesByCategory: {0}", ex.ToString()));
            }

            return new List<SumByCatDTO>();
        }



        public async Task<List<SumByOwnerDTO>> GetSumOfExpensesByOwner(string ownerId, int numberOfDaysToShow)
        {
            try
            {
                var response = await client.GetAsync(String.Format("http://194.5.157.98:88/api/Expenses/GetSumOfExpensesByOwner?ownerId={0}&numberOfDaysToShow={1}", ownerId, numberOfDaysToShow));
                //var responseInfo = response.GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<SumByOwnerDTO> expensesByOwner = JsonConvert.DeserializeObject<List<SumByOwnerDTO>>(responseBody);
                    if (expensesByOwner != null)
                    {
                        return expensesByOwner;
                    }
                
            }
            catch (Exception ex)
            {

                Logger.Log(string.Format("GetSumOfExpensesByOwner: {0}", ex.ToString()));
            }

            return new List<SumByOwnerDTO>();
        }




        public async Task<string> RemoveExpense(string expenseId)
        {
            try
            {
                var response = await client.DeleteAsync(String.Format("http://194.5.157.98:88/api/Expenses/{0}", expenseId));
                response.EnsureSuccessStatusCode();
                    return "Success!";
            }
            catch (Exception ex)
            {

                Logger.Log(string.Format("RemoveExpense: {0}", ex.ToString()));
            }
            return "Unexpected error";

        }


        public async Task<string> ModifyExpense(string expenseId, string expenseName, float moneyUsed, int expenseCategory)
        {
            ModifyExpenseDTO data = new ModifyExpenseDTO {expenseId = expenseId, expenseName = expenseName, moneyUsed = moneyUsed, expenseCategory = expenseCategory };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync("http://194.5.157.98:88/api/Expenses/ModifyExpense", stringContent);
                response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return responseBody;
                

            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("ModifyExpense: {0}", ex.ToString()));
            }

            return "Unexpected error";
        }


    }

}
