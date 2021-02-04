using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.DTO.User.Input
{
    public class ChangePasswordDTO
    {
        public string userId { get; set; }
        public string userNewPassword { get; set; }

    }
}
