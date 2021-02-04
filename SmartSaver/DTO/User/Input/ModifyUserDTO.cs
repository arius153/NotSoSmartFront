using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.User.Input
{
    public class ModifyUserDTO
    {
        public string userId { get; set; }
        public string newUserName { get; set; }
        public string newUserEmail { get; set; }
        public float newUserMoney { get; set; }

    }
}
