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
using SimpleCardMaker.DAL.DBO;
using SimpleCardMaker.DAL.Repositories;
using SimpleCardMaker.Models;

namespace SimpleCardMaker.Controllers
{
    public class CardsController : Controller
    {
        private readonly IRepository<Card> _cardRepo;
        private readonly IRepository<Keyword> _keywordRepo;
        private readonly IRepository<UnitType> _unitTypeRepo;

        private readonly IWebHostEnvironment _hostEnvironment;

        public CardsController(
            IRepository<Card> cardRepo,
            IRepository<Keyword> keywordRepo,
            IRepository<UnitType> unitTypeRepo,
            IWebHostEnvironment hostEnvironment)
        {
            _cardRepo = cardRepo;
            _keywordRepo = keywordRepo;
            _unitTypeRepo = unitTypeRepo;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
            return View(await _cardRepo.GetAllAsync());
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _cardRepo.GetByIdAsync(id.Value);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public async Task<IActionResult> Create()
        {
            ViewData["KeywordId"] = new SelectList(await _keywordRepo.GetAllAsync(), "Id", "Name");
            ViewData["UnitTypeId"] = new SelectList(await _unitTypeRepo.GetAllAsync(), "Id", "Name");
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

                await _cardRepo.CreateAsync(card);
                return RedirectToAction(nameof(Index));
            }
            ViewData["KeywordId"] = new SelectList(await _keywordRepo.GetAllAsync(), "Id", "Name", card.KeywordId);
            ViewData["UnitTypeId"] = new SelectList(await _unitTypeRepo.GetAllAsync(), "Id", "Name", card.UnitTypeId);
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _cardRepo.GetByIdAsync(id.Value);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["KeywordId"] = new SelectList(await _keywordRepo.GetAllAsync(), "Id", "Name", card.KeywordId);
            ViewData["UnitTypeId"] = new SelectList(await _unitTypeRepo.GetAllAsync(), "Id", "Name", card.UnitTypeId);
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


                    await _cardRepo.UpdateAsync(card);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_cardRepo.Exists(card.Id))
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
            ViewData["KeywordId"] = new SelectList(await _keywordRepo.GetAllAsync(), "Id", "Name", card.KeywordId);
            ViewData["UnitTypeId"] = new SelectList(await _unitTypeRepo.GetAllAsync(), "Id", "Name", card.UnitTypeId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _cardRepo.GetByIdAsync(id.Value);
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
            await _cardRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

     
    }
}
