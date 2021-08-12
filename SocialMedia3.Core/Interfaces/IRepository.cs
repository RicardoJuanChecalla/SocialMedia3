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
        // Task<IEnumerable<T>> GetAll();
        IEnumerable<T> GetAll();
        Task<T> GetById(int id); 
        Task Add(T entity);
        // Task Update(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}