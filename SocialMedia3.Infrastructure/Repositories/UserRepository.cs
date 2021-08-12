using System;
using System.Collections.Generic;
using System.Linq;
using SocialMedia3.Core.Entities;
using SocialMedia3.Core.Interfaces;
using System.Threading.Tasks;
using SocialMedia3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia3.Infrastructure.Repositories
{
    public class UserRepository // IUserRepository
    {
        /*
        private readonly SocialMediaContext _context;

        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync( x=>x.UserId == id );
            return user;
        }
        */
        /*
        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            var currentUser = await GetUser(user.UserId);
            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.Email = user.Email;
            currentUser.DateOfBirth = user.DateOfBirth;
            currentUser.Telephone = user.Telephone;
            currentUser.IsActive = user.IsActive;
            int rows = await _context.SaveChangesAsync();
            return rows>0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var currentUser = await GetUser(id);
            _context.Users.Remove(currentUser);
            int rows = await _context.SaveChangesAsync();
            return rows>0;
        }
        */
    }
}