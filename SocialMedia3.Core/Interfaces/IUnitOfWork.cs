using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia3.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia3.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        // IRepository<Post> PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        ISecurityRepository SecurityRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync(); 
    }
}