using SimpleCardMaker.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<List<Card>> GetAllAsyncWithKeywordsAndUnitTypes();
    }
}
