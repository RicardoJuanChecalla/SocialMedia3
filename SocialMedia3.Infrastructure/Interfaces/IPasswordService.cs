namespace SocialMedia3.Infrastructure.Interfaces
{
    public interface IPasswordService
    {
         string Hash(string password);
         bool Check(string hash, string password);
    }
}