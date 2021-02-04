using SmartSaver.DTO.Income.Output;
using SmartSaver.Processors;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.User.Output
{
   
    public class User
    {
        private static IncomeProcessor inc = new IncomeProcessor();
        public string Userid { get; set; }
        public string Useremail { get; set; }
        public string Userpassword { get; set; }
        public string Username { get; set; }
        public string Userlastname { get; set; }
        public float? Usermoney { get; set; }


    }
}
