﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eVoting.Server.Infrastructure
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
