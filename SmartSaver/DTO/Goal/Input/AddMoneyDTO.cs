using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Goal.Input
{
    class AddMoneyDTO
    {
        public string userId { get; set; }
        public string goalId { get; set; }
        public string goalName { get; set; }
        public float moneyToAdd { get; set; }
        public float goalAllocatedMoney { get; set; }
        public float goalRequiredMoney { get; set; }

    }
}
