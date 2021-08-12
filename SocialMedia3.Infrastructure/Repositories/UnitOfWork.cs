using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SocialMedia3.Core.Interfaces;
using SocialMedia3.Infrastructure.Data;
using SocialMedia3.Core.Entities;

namespace SocialMedia3.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        //private readonly IPostRepository _postRepository;
        // private readonly IRepository<Post> _postRepository;
        // private readonly IRepository<User> _userRepository;
        // private readonly IRepository<Comment> _commentRepository;

        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        // public IRepository<Post> PostRepository => _postRepository ?? new BaseRepository<Post>(_context);

        // public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        // public IRepository<Comment> CommentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);

        public IPostRepository PostRepository => new PostRepository(_context);

        public IRepository<User> UserRepository => new BaseRepository<User>(_context);

        public IRepository<Comment> CommentRepository => new BaseRepository<Comment>(_context);

        public ISecurityRepository SecurityRepository => new SecurityRepository(_context);

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        } 
    }
}