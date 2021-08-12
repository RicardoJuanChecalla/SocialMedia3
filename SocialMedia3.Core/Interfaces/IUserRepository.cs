using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia3.Core.Entities;

namespace SocialMedia3.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
    }
}