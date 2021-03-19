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

namespace SimpleCardMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepo;
        private readonly IRepository<Keyword> _keywordRepo;
        private readonly IRepository<UnitType> _unitTypeRepo;

        private readonly IWebHostEnvironment _hostEnvironment;

        public CardsController(
            ICardRepository cardRepo,
            IRepository<Keyword> keywordRepo,
            IRepository<UnitType> unitTypeRepo,
            IWebHostEnvironment hostEnvironment)
        {
            _cardRepo = cardRepo;
            _keywordRepo = keywordRepo;
            _unitTypeRepo = unitTypeRepo;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _cardRepo.GetAllAsyncWithKeywordsAndUnitTypes();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _cardRepo.GetByIdAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    SaveImage(card);

                    await _cardRepo.UpdateAsync(card);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_cardRepo.Exists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            if (ModelState.IsValid)
            {
                SaveImage(card);

                await _cardRepo.CreateAsync(card);
            }
            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _cardRepo.GetByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            await _cardRepo.DeleteAsync(card);

            return NoContent();
        }

        public async void SaveImage(Card card)
        {
            // save image to wwwroot/images
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string extension = Path.GetExtension(card.ImageFile.FileName);
            string fileName = Path.GetFileNameWithoutExtension(card.ImageFile.FileName);


            card.ImageFileName = fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            string path = Path.Combine(wwwRootPath + "/images/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await card.ImageFile.CopyToAsync(fileStream);
            }
        }
    }
}
