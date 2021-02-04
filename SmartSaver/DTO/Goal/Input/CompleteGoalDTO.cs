using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Goal.Input
{
    class CompleteGoalDTO
    {
        public string goalId { get; set; }
        public float moneyRequired { get; set; }
        public float moneyAllocated { get; set; }

    }
}
