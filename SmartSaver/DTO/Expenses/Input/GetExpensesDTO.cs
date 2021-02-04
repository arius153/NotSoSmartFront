using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Expenses.Input
{
    class GetExpensesDTO
    {
        public string ownerId { get; set; }
        public int numberOfDaysToShow { get; set; }
        public int maxNumberOfExpensesToShow { get; set; }

    }
}
