using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Income.Input
{
    class NewIncomeDTO
    {
        public string ownerId { get; set; }
        public string userId { get; set; }
        public string incomeName { get; set; }
        public double moneyReceived { get; set; }

    }
}
