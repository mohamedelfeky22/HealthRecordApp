﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.Authentication.DTO.Outgoing
{
    public class AuthResult
    {
        public string Token { get; set; }

        public string RefershToken { get; set; }
        public bool  Success { get; set; }
        public List<string> Errors { get; set; }

    }
}
