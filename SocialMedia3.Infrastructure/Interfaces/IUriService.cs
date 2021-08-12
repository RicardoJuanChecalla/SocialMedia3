using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia3.Core.QueryFilters;

namespace SocialMedia3.Infrastructure.Interfaces
{
    public interface IUriService
    {
          Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}