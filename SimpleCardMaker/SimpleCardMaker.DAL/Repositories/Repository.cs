using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly CardDbContext _context;

        public Repository(CardDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
             // _context.SaveChangesAsync();
        }
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
           // await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            /* for mvc
            var entityToDelete = _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }
            await _context.SaveChangesAsync();
            */

            // for api
            _context.Set<TEntity>().Remove(entity);
            // _context.SaveChangesAsync(); 
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            // get list in desc order by id
            return await _context.Set<TEntity>().OrderByDescending(e => e.Id).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }
        // check if that id exists
        public bool Exists(int id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }

    }
}
