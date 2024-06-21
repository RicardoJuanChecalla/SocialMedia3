using System.Threading.Tasks;
using SocialMedia3.Core.Entities;

namespace SocialMedia3.Core.Interfaces
{
    public interface ISecurityRepository  : IRepository<Security>
    {
        Task<Security?> GetLoginByCredential(UserLogin login);
    }
}