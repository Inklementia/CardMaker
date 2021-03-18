using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL.DBO;
using System;
using System.Collections.Generic;
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
            return await _context.Set<Card>().Include(p => p.Keyword).Include(p => p.UnitType).ToListAsync();
        }

    }
}
