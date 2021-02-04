using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Expenses.Output
{
    public class ExpenseDTO
    {
        public string Ownerid { get; set; }
        public string Userid { get; set; }
        public string Expenseid { get; set; }
        public float Moneyused { get; set; }
        public DateTime? Expensetime { get; set; }
        public string Expensename { get; set; }
        public int? Expensecategory { get; set; }

        public override string ToString()
        {
            return String.Format("Name: {0} Amount: {1}$ Date: {2}", this.Expensename, this.Moneyused, this.Expensetime);
        }
    }

}
