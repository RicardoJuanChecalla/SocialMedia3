
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia3.Core.Entities;
using SocialMedia3.Core.Interfaces;
using SocialMedia3.Infrastructure.Data;

namespace SocialMedia3.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context): base(context){}
        public async Task<Security?> GetLoginByCredential(UserLogin login)
        {
            //return await _entities.FirstOrDefaultAsync(x=>x.User == login.User && x.Password ==login.Password);
            return await _entities.FirstOrDefaultAsync(x=>x.User == login.User);
        }
    }
}