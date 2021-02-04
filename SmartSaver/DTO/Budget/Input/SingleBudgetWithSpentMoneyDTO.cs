using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Budget.Input
{
   public class SingleBudgetWithSpentMoneyDTO : SingleBudgetDTO
    {
        public float spent { get; set; }
        public string NewProperty
        {

            get
            {
                return string.Format("{0}/{1}", spent, limit);
            }
        }
        public string color
        {
            get
            {
                if (spent > limit)
                {
                    return "Red";
                }
                else
                    return "White";
            }
        }
    }
}
