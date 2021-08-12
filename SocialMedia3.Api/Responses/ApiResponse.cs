using System;
using System.Collections.Generic;
using System.Linq;
using SocialMedia3.Core.CustomEntities;

namespace SocialMedia3.Api.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data {get; set;}

        public Metadata Meta {get; set;}
    }
}