using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.User.Input
{
    public class NewUserDTO
    {
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
    }
}
