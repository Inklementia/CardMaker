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
    public class KeywordsController : ControllerBase
    {
        private readonly IRepository<Keyword> _keywordRepo;

        public KeywordsController(IRepository<Keyword> keywordRepo)
        {
            _keywordRepo = keywordRepo;
        }

        // GET: api/Keywords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Keyword>>> GetKeywords()
        {
            return await _keywordRepo.GetAllAsync();
        }

        // GET: api/Keywords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Keyword>> GetKeyword(int id)
        {
            var keyword = await _keywordRepo.GetByIdAsync(id);

            if (keyword == null)
            {
                return NotFound();
            }

            return keyword;
        }

        // PUT: api/Keywords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKeyword(int id, Keyword keyword)
        {
            if (id != keyword.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _keywordRepo.UpdateAsync(keyword);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_keywordRepo.Exists(id))
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

        // POST: api/Keywords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Keyword>> PostKeyword(Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                await _keywordRepo.CreateAsync(keyword);
            }
            return CreatedAtAction("GetKeyword", new { id = keyword.Id }, keyword);
        }

        // DELETE: api/Keywords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeyword(int id)
        {
            var keyword = await _keywordRepo.GetByIdAsync(id);
            if (keyword == null)
            {
                return NotFound();
            }

            await _keywordRepo.DeleteAsync(keyword);

            return NoContent();
        }

     
    }
}
