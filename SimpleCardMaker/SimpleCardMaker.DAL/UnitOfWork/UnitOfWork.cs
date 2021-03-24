using SimpleCardMaker.DAL.DBO;
using SimpleCardMaker.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCardMaker.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CardDbContext _context;
        public ICardRepository Cards { get; }
        public IRepository<Keyword> Keywords { get; }
        public IRepository<UnitType> UnitTypes { get; }


        public UnitOfWork(
            CardDbContext context,
            ICardRepository cardRepository,
            IRepository<Keyword> keywordRepository,
            IRepository<UnitType> unitTypeRepository
            )
        {
            _context = context;
            Cards = cardRepository;
            Keywords = keywordRepository;
            UnitTypes = unitTypeRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
