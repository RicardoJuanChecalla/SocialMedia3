using System;
using SocialMedia3.Core.Enumerations;

namespace SocialMedia3.Core.DTOs
{
    public class SecurityDto
    {
        public string? User {get; set;}
        public string? UserName {get; set;}
        public string? Password {get; set;}
        public RoleType? Role {get; set;}
    }
}