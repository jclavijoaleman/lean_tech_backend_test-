﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lean_tech_backend_test.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}