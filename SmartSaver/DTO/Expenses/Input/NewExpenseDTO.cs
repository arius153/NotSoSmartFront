using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Expenses.Input
{
    class NewExpenseDTO
    {
        public string userId { get; set; }
        public string ownerId { get; set; }
        public string expenseName { get; set; }
        public float moneyUsed { get; set; }
        public int expenseCategory { get; set; }

    }
}
