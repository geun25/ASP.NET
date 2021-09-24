﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Data.DataModels
{
    // C# Class => Database Table
    // 멤벼변수 => Table Column

    public class User
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }

        public DateTime JoinedUtcDate { get; set; } // 가입일자
    }
}
