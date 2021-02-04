using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Income.Input
{
    class GetAllDTO
    {
        public string ownerId { get; set; }
        public int numberOfDaysToShow { get; set; }
        public int maxNumberOfIncomesToShow { get; set; }

    }
}
