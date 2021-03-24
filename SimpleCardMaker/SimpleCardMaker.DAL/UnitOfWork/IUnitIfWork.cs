using SimpleCardMaker.DAL.DBO;
using SimpleCardMaker.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCardMaker.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICardRepository Cards{ get; }
        IRepository<Keyword> Keywords { get; }
        IRepository<UnitType> UnitTypes { get; }
        int Complete();
    }
}
