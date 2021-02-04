using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Income.Input
{
   public class ModifyIncomeDTO
    {
        public string incomeId { get; set; }
        public string incomeName { get; set; }
        public double moneyReceived { get; set; }
    }
}
