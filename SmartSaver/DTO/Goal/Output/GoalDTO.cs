using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.Goal.Output
{
    public class GoalDTO
    {
        public string Ownerid { get; set; }
        public string Goalid { get; set; }
        public float Moneyrequired { get; set; }
        public float Moneyallocated { get; set; }
        public DateTime? Goaltime { get; set; }
        public DateTime? Goalexpectedtime { get; set; }
        public string Goalname { get; set; }
    }
}
