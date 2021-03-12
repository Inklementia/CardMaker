using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.Repositories
{
    public class KeywordRepo : BaseRepo, IRepository<Keyword>
    {
        public KeywordRepo(CardDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Keyword entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Keyword entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var keyword = await _context.Keywords.FindAsync(id);
            _context.Keywords.Remove(keyword);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Keyword>> GetAllAsync()
        {
            return await _context.Keywords
                .ToListAsync();
        }

        public async Task<Keyword> GetByIdAsync(int id)
        {
            return await _context.Keywords
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public bool Exists(int id)
        {
            return _context.Keywords.Any(e => e.Id == id);
        }
    }
}
