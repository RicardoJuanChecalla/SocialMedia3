using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia3.Core.Entities;
using SocialMedia3.Core.QueryFilters;
using SocialMedia3.Core.CustomEntities;

namespace SocialMedia3.Core.Interfaces
{
    public interface IPostService
    {
        // IEnumerable<Post> GetPosts(PostQueryFilter filters);
        PagedList<Post> GetPosts(PostQueryFilter filters);

        Task<Post?> GetPost(int id);

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);

        Task<bool> DeletePost(int id);
    }
}