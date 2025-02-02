using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SocialMedia3.Infrastructure.Options;
using SocialMedia3.Infrastructure.Interfaces;
using System.Security.Cryptography;

namespace SocialMedia3.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;
        public PasswordService(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public string Hash(string password)
        {
            //PBKDF2 implementation
            using(
                var algorithm = new Rfc2898DeriveBytes(password, _options.SaltSize, _options.Iterations)
            )
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_options.Iterations}.{salt}.{key}";
            }
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format");
            }
            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            using(var algorithm = new Rfc2898DeriveBytes(password,salt,iterations))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }
    }
}