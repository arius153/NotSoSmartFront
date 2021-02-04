using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Goal.Input
{
    class NewGoalDTO
    {
        public string ownerId { get; set; }
        public string goalName { get; set; }
        public float moneyRequired { get; set; }
        public DateTime goalExpectedTime { get; set; }

    }
}
