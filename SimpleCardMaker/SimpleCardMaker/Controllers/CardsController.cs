using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL;
using SimpleCardMaker.Models;

namespace SimpleCardMaker.Controllers
{
    public class CardsController : Controller
    {
        private readonly CardDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CardsController(CardDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
            var cardDbContext = _context.Cards.Include(c => c.Keyword).Include(c => c.UnitType);
            return View(await cardDbContext.ToListAsync());
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.Keyword)
                .Include(c => c.UnitType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name");
            ViewData["UnitTypeId"] = new SelectList(_context.UnitTypes, "Id", "Name");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ManaCost,Attack,Defence,ImageFile,KeywordId,UnitTypeId")] Card card)
        {
            if (ModelState.IsValid)
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

                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", card.KeywordId);
            ViewData["UnitTypeId"] = new SelectList(_context.UnitTypes, "Id", "Name", card.UnitTypeId);
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", card.KeywordId);
            ViewData["UnitTypeId"] = new SelectList(_context.UnitTypes, "Id", "Name", card.UnitTypeId);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ManaCost,Attack,Defence,ImageFile,KeywordId,UnitTypeId")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
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


                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", card.KeywordId);
            ViewData["UnitTypeId"] = new SelectList(_context.UnitTypes, "Id", "Name", card.UnitTypeId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.Keyword)
                .Include(c => c.UnitType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
