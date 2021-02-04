using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Budget.Input
{
    class ModifyBudgetDTO
    {
        public string ownerId { get; set; }
        public List<string> listOfValues { get; set; }

    }
}
