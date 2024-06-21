using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia3.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia3.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T?> GetById(int id); 
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}