using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _hostEnvironment;

        public CardsController(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards(int? keywordId, int? unittypeId)
        {
            var cards = await _unitOfWork.Cards.GetAllAsyncWithKeywordsAndUnitTypes();
            if (keywordId != null && unittypeId != null)
            {
                var filteredCards = cards
                    .Where(k => k.KeywordId == keywordId.Value)
                    .Where(u => u.UnitTypeId == unittypeId.Value);
                return Ok(filteredCards);
            }
            else if (keywordId != null)
            {
                var filteredCards = cards
                    .Where(k => k.KeywordId == keywordId.Value);
                return Ok(filteredCards);
            }
            else if(unittypeId != null)
            {
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
            if (id != card.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
       
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.Cards.CreateAsync(card);
            _unitOfWork.Complete();

            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _unitOfWork.Cards.GetByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _unitOfWork.Cards.Delete(card);
            _unitOfWork.Complete();

            return NoContent();
        }

   
    }
}
