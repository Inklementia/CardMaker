using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            var card = await _unitOfWork.Cards.GetByIdAsync(id);

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
                SaveImage(card);

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
         
            SaveImage(card);

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

        public async void SaveImage(Card card)
        {
            // save image to wwwroot/images
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string extension;
            string fileName;
            string path;

            if (card.ImageFile != null)
            {
                extension = Path.GetExtension(card.ImageFile.FileName);
                fileName = Path.GetFileNameWithoutExtension(card.ImageFile.FileName);
                card.ImageFileName = fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                path = Path.Combine(wwwRootPath + "/uploads/", fileName);
            }
            else
            {
                path = Path.Combine(wwwRootPath + "/uploads/", "test.png");
            }
           
            using (var fileStream = new FileStream(path, FileMode.Create))
            await card.ImageFile.CopyToAsync(fileStream);
            
        }
    }
}
