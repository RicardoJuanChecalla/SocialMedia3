using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia3.Core.QueryFilters;
using SocialMedia3.Infrastructure.Interfaces;

namespace SocialMedia3.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUri = $"{_baseUri}{actionUrl}";
            return new Uri(baseUri);
        }
    }
}