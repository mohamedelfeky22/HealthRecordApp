﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.Authentication.Configuration
{
    public class JWTConfig
    {
        public string Secret { get; set; }
        public TimeSpan ExpiryTime { get; set; }
    }
}
