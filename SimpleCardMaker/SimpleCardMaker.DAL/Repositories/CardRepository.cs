using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.Repositories
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(CardDbContext context) : base(context)
        { 
        }

        public async Task<List<Card>> GetAllAsyncWithKeywordsAndUnitTypes()
        {
                return await _context.Set<Card>()
                    .Include(k => k.Keyword)
                    .Include(u => u.UnitType)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();    
        }
        public async Task<Card> GetByIdAsyncWithKeywordAndUnitType(int id)
        {
            return await _context.Set<Card>()
                .Include(k => k.Keyword)
                .Include(u => u.UnitType)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
