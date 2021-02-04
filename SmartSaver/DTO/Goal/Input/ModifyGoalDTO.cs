using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Goal.Input
{
    class ModifyGoalDTO
    {
        public string goalId { get; set; }
        public string newGoalName { get; set; }
        public float newMoneyRequired { get; set; }
        public DateTime newExpectedTime { get; set; }

    }
}
