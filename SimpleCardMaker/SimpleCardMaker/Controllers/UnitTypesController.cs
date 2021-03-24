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
using SimpleCardMaker.DAL.UnitOfWork;

namespace SimpleCardMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitTypesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public UnitTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/UnitTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitType>>> GetUnitTypes()
        {
            return await _unitOfWork.UnitTypes.GetAllAsync();
        }

        // GET: api/UnitTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitType>> GetUnitType(int id)
        {
            var unitType = await _unitOfWork.UnitTypes.GetByIdAsync(id);

            if (unitType == null)
            {
                return NotFound();
            }

            return unitType;
        }

        // PUT: api/UnitTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUnitType(int id, UnitType unitType)
        {
            if (id != unitType.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            try
            {
                 _unitOfWork.UnitTypes.Update(unitType);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.UnitTypes.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
  
        }

        // POST: api/UnitTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnitType>> PostUnitType(UnitType unitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            await _unitOfWork.UnitTypes.CreateAsync(unitType);
            _unitOfWork.Complete();
            
            return CreatedAtAction("GetUnitType", new { id = unitType.Id }, unitType);
        }

        // DELETE: api/UnitTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitType(int id)
        {
            var unitType = await _unitOfWork.UnitTypes.GetByIdAsync(id);
            if (unitType == null)
            {
                return NotFound();
            }

            _unitOfWork.UnitTypes.Delete(unitType);
            _unitOfWork.Complete();
            return NoContent();
        }

   
    }
}
