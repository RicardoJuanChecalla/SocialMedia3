using System.Runtime.CompilerServices;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia3.Core.Interfaces;
using SocialMedia3.Core.Entities;
using SocialMedia3.Core.Exceptions;
using SocialMedia3.Core.QueryFilters;
using SocialMedia3.Core.CustomEntities;
using Microsoft.Extensions.Options;

namespace SocialMedia3.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly PaginationOption _paginationOption;
        // private readonly IRepository<Post> _postRepository;
        // private readonly IRepository<User> _userRepository;

        // public PostService(IRepository<Post> postRepository,IRepository<User> userRepository)
        // {
        //     _postRepository = postRepository;
        //     _userRepository = userRepository;
        // }

        public PostService(IUnitOfWork unitofwork, IOptions<PaginationOption> options )
        {
            _unitofwork = unitofwork;
            _paginationOption = options.Value;
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitofwork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinessException("User doesn't exist");
            }
            var userPost = await _unitofwork.PostRepository.GetPostsByUser(post.UserId);
            if(userPost.Count()<10)
            {
                var lastPost = userPost.OrderByDescending(x=>x.Date).FirstOrDefault();
                if((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish the post");
                }
            }

            if(post.Description.Contains("sexo"))
            {
                throw new BusinessException("Content not allow");
            }
           await _unitofwork.PostRepository.Add(post);
           await _unitofwork.SaveChangesAsync();
        }

        // public async Task<IEnumerable<Post>> GetPosts()
        // public IEnumerable<Post> GetPosts(PostQueryFilter filters)
        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOption.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOption.DefaultPageSize : filters.PageSize;

            // return await _unitofwork.PostRepository.GetAll();
            var posts = _unitofwork.PostRepository.GetAll();
            if (filters.UserId != null)
            {
                posts = posts.Where(x=>x.UserId == filters.UserId );
            }
            if (filters.Date != null)
            {
                posts = posts.Where(x=>x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }
            if (filters.Description != null)
            {
                posts = posts.Where(x=>x.Description.ToLower().Contains(filters.Description.ToLower()));
            }
            var pageListPost = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);
            return pageListPost;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitofwork.PostRepository.GetById(id);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var consultPost = await _unitofwork.PostRepository.GetById(post.Id);
            consultPost.Description = post.Description;
            consultPost.Image = post.Image;
            _unitofwork.PostRepository.Update(consultPost);
            await _unitofwork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitofwork.PostRepository.Delete(id);
            await _unitofwork.SaveChangesAsync();
            return true;
        }

    }
}