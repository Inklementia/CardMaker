using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL;
using SimpleCardMaker.DAL.DBO;
using SimpleCardMaker.DAL.Repositories;

namespace SimpleCardMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitTypesController : ControllerBase
    {
        private readonly IRepository<UnitType> _unitTypeRepo;

        public UnitTypesController(IRepository<UnitType> unitTypeRepo)
        {
            _unitTypeRepo = unitTypeRepo;
        }

        // GET: api/UnitTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitType>>> GetUnitTypes()
        {
            return await _unitTypeRepo.GetAllAsync();
        }

        // GET: api/UnitTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitType>> GetUnitType(int id)
        {
            var unitType = await _unitTypeRepo.GetByIdAsync(id);

            if (unitType == null)
            {
                return NotFound();
            }

            return unitType;
        }

        // PUT: api/UnitTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitType(int id, UnitType unitType)
        {
            if (id != unitType.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitTypeRepo.UpdateAsync(unitType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitTypeRepo.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
           

            return NoContent();
        }

        // POST: api/UnitTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnitType>> PostUnitType(UnitType unitType)
        {
            if(ModelState.IsValid){
                await _unitTypeRepo.CreateAsync(unitType);
            }

            return CreatedAtAction("GetUnitType", new { id = unitType.Id }, unitType);
        }

        // DELETE: api/UnitTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitType(int id)
        {
            var unitType = await _unitTypeRepo.GetByIdAsync(id);
            if (unitType == null)
            {
                return NotFound();
            }

            await _unitTypeRepo.DeleteAsync(unitType);

            return NoContent();
        }

   
    }
}
