using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia3.Core.Entities;
using SocialMedia3.Core.Interfaces;
using SocialMedia3.Infrastructure.Data;

namespace SocialMedia3.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        //public async Task<IEnumerable<T>> GetAll()
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
            // return await _entities.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
            //_entities.Add(entity);
            //await _context.SaveChangesAsync();
        }

        //public async Task Update(T entity)
        public void Update(T entity)
        {
            _entities.Update(entity);
            //await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        /*
        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
            //await _context.SaveChangesAsync();
        }
        */
    }
}