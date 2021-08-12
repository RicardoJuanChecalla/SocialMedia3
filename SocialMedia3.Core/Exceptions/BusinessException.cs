using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia3.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }

         public BusinessException(string message) : base(message)
         {

         }

        
    }
}