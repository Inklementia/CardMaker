using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCardMaker.DAL.Repositories
{
    public abstract class BaseRepo
    {
        protected readonly CardDbContext _context;

        protected BaseRepo(CardDbContext context)
        {
            _context = context;
        }
    }
}
