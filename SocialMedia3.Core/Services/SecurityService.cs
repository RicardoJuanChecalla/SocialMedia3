using System;
using System.Threading.Tasks;
using SocialMedia3.Core.Interfaces;
using SocialMedia3.Core.Entities;

namespace SocialMedia3.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security?> GetLoginByCredential(UserLogin login)
        {
            return await _unitOfWork.SecurityRepository.GetLoginByCredential(login);
        }

        public async Task RegisterUser(Security security)
        {
            await _unitOfWork.SecurityRepository.Add(security);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}