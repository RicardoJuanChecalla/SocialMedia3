using System;
using System.Threading.Tasks;
using SocialMedia3.Core.Entities;

namespace SocialMedia3.Core.Interfaces
{
    public interface ISecurityService
    {
         Task<Security?> GetLoginByCredential(UserLogin login);

         Task RegisterUser(Security security);
    }
}