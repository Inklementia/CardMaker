using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.Repositories
{
    public class CardRepo : BaseRepo, IRepository<Card>
    {
        public CardRepo(CardDbContext context) : base(context)
        { 
        }

        public async Task CreateAsync(Card entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Card entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Card>> GetAllAsync()
        {
            return await _context.Cards.Include(p => p.Keyword).Include(p => p.UnitType)
                .ToListAsync();
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            return await _context.Cards.Include(p => p.Keyword).Include(p => p.UnitType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
