using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Expenses.Input
{
    public class ModifyExpenseDTO
    {
        public string expenseId { get; set; }
        public string expenseName { get; set; }
        public float moneyUsed { get; set; }
        public int expenseCategory { get; set; }
    }
}
