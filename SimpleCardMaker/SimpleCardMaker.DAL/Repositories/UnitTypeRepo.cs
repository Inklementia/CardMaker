using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.Repositories
{
    public class UnitTypeRepo : BaseRepo, IRepository<UnitType>
    {
        public UnitTypeRepo(CardDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(UnitType entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UnitType entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var unitType = await _context.UnitTypes.FindAsync(id);
            _context.UnitTypes.Remove(unitType);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UnitType>> GetAllAsync()
        {
            return await _context.UnitTypes
                .ToListAsync();
        }

        public async Task<UnitType> GetByIdAsync(int id)
        {
            return await _context.UnitTypes
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public bool Exists(int id)
        {
            return _context.UnitTypes.Any(e => e.Id == id);
        }

    }
}
