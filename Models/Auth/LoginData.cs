﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Auth
{
    public enum AuthType
    {
        TRADITIONAL = 0,
        GOOGLE = 1
    }

    [DataContract]
    public class LoginData
    {
        [DataMember]
        public string Email { get; set; } = string.Empty;
        
        [DataMember]
        public string Password { get; set; } = string.Empty;

        [DataMember]
        public AuthType authType { get; set; }
    }
}
