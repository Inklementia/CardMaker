using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
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
    public class CardsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CardsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards(int? keywordId, int? unittypeId)
        {
            var cards = await _unitOfWork.Cards.GetAllAsyncWithKeywordsAndUnitTypes();
            // if smth is selected from select list 
            if (keywordId != null && unittypeId != null)
            {
                // filter cards accordingly
                var filteredCards = cards
                    .Where(k => k.KeywordId == keywordId.Value)
                    .Where(u => u.UnitTypeId == unittypeId.Value);
                return Ok(filteredCards);
            }
            // if only keyword is selected
            else if (keywordId != null)
            {
                // filter cards only by keywordss
                var filteredCards = cards
                    .Where(k => k.KeywordId == keywordId.Value);
                return Ok(filteredCards);
            }
            // if only unit type is selected
            else if (unittypeId != null)
            {
                // filter cards only by unit types
                var filteredCards = cards
                    .Where(u => u.UnitTypeId == unittypeId.Value);
                return Ok(filteredCards);
            }
            return cards;
        }


        // GET: api/Cards/5
     
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _unitOfWork.Cards.GetByIdAsyncWithKeywordAndUnitType(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    
        [HttpPut("{id}")]
        public IActionResult PutCard(int id, Card card)
        {
            // if card id does not exist
            if (id != card.Id)
            {
                return BadRequest();
            }
            // if there are validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // if everything is fine -> try to update
            try
            {
                _unitOfWork.Cards.Update(card);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.Cards.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            // if there are validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // if everything is fine -> create
            await _unitOfWork.Cards.CreateAsync(card);
            _unitOfWork.Complete();

            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            // finding an existing card by id
            var card = await _unitOfWork.Cards.GetByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            // if there is a card with such id -> delete
            _unitOfWork.Cards.Delete(card);
            _unitOfWork.Complete();

            return NoContent();
        }

   
    }
}
