using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Income.Output
{
    public class IncomeDTO
    {
        public string Ownerid { get; set; }
        public string Userid { get; set; }
        public string Incomeid { get; set; }
        public float Moneyrecieved { get; set; }
        public DateTime? Incometime { get; set; }
        public string Incomename { get; set; }
    }
}
