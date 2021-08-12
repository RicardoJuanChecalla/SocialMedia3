using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia3.Core.Entities;

namespace SocialMedia3.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUser(int userId);

        // Task<IEnumerable<Post>> GetPosts();

        // Task<Post> GetPost(int id);

        // Task InsertPost(Post post);

        // Task<bool> UpdatePost(Post post);

        // Task<bool> DeletePost(int id);
    }
}