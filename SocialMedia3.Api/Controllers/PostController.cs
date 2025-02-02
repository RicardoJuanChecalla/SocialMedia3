using System.ComponentModel.DataAnnotations;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using SocialMedia3.Core.Entities;
using SocialMedia3.Infrastructure.Repositories;
using SocialMedia3.Core.Interfaces;
using System.Threading.Tasks;
using SocialMedia3.Core.DTOs;
using AutoMapper;
using SocialMedia3.Api.Responses;
using SocialMedia3.Core.QueryFilters;
using SocialMedia3.Core.CustomEntities;
using Newtonsoft.Json;
using SocialMedia3.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SocialMedia3.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class PostController : ControllerBase
    {
        //private readonly IPostRepository _postRepository;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            //_postRepository = postRepository;
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all Posts
        /// </summary>
        /// <param name="filters">Filters to Apply</param>
        /// <returns>
        /// </returns>
        [HttpGet(Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters)
        {
            // var posts = await _postService.GetPosts();
            var posts = _postService.GetPosts(filters);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            var metadata = new Metadata {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPages = posts.TotalPages,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters,Url.RouteUrl(nameof(GetPosts))!).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters,Url.RouteUrl(nameof(GetPosts))!).ToString()
            };
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto)
            {
                Meta = metadata
            };
            //Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metadata));
            Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post= _mapper.Map<Post>(postDto);
            await _postService.InsertPost(post);
            var postDtoNew = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDtoNew);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto postDto)
        {
            var post= _mapper.Map<Post>(postDto);
            //post.PostId = id;
            post.Id = id;
            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result  = await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}