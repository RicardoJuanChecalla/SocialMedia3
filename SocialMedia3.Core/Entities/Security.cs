using System;
using System.Collections.Generic;
using SocialMedia3.Core.Enumerations;

namespace SocialMedia3.Core.Entities
{
    public class Security : BaseEntity
    {
        public string User {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public RoleType Role {get; set;}
    }
}