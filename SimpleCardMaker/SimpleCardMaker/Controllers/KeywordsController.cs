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
    public class KeywordsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public KeywordsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Keywords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Keyword>>> GetKeywords()
        {
            // getting list of keywords
            return await _unitOfWork.Keywords.GetAllAsync();
        }

        // GET: api/Keywords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Keyword>> GetKeyword(int id)
        {
            // getting a single keyword
            var keyword = await _unitOfWork.Keywords.GetByIdAsync(id);

            if (keyword == null)
            {
                return NotFound();
            }

            return keyword;
        }

        // PUT: api/Keywords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutKeyword(int id, Keyword keyword)
        {
            // if there is no keyword with such id
            if (id != keyword.Id)
            {
                return BadRequest();
            }
            // if there are validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // updating
            try
            {
                _unitOfWork.Keywords.Update(keyword);
                _unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.Keywords.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Keywords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Keyword>> PostKeyword(Keyword keyword)
        {
            // if there are validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // creating
            await _unitOfWork.Keywords.CreateAsync(keyword);
            _unitOfWork.Complete();

            return CreatedAtAction("GetKeyword", new { id = keyword.Id }, keyword);
        }

        // DELETE: api/Keywords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeyword(int id)
        {
            // finding keyword by id
            var keyword = await _unitOfWork.Keywords.GetByIdAsync(id);
            if (keyword == null)
            {
                return NotFound();
            }
            // deleting
            _unitOfWork.Keywords.Delete(keyword);
            _unitOfWork.Complete();
            return NoContent();
        }

     
    }
}
