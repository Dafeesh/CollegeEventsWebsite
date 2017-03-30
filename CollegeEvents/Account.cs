using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeEvents
{
    public class Account
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AccessLevel GrantedAccess { get; set; }
    }
}