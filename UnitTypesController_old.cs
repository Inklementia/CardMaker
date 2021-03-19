using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleCardMaker.DAL;
using SimpleCardMaker.DAL.DBO;
using SimpleCardMaker.DAL.Repositories;
using SimpleCardMaker.Models;

namespace SimpleCardMaker.Controllers
{
    public class UnitTypesController_old : Controller
    {
        private readonly IRepository<UnitType> _unitTypeRepo;

        public UnitTypesController_old(IRepository<UnitType> unitTypeRepo)
        {
            _unitTypeRepo = unitTypeRepo;
        }

        // GET: UnitTypes
        public async Task<IActionResult> Index()
        {
            return View(await _unitTypeRepo.GetAllAsync());
        }

        // GET: UnitTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _unitTypeRepo.GetByIdAsync(id.Value);
               
            if (unitType == null)
            {
                return NotFound();
            }

            return View(unitType);
        }

        // GET: UnitTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] UnitType unitType)
        {
            if (ModelState.IsValid)
            {
                await _unitTypeRepo.CreateAsync(unitType);
                return RedirectToAction(nameof(Index));
            }
            return View(unitType);
        }

        // GET: UnitTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _unitTypeRepo.GetByIdAsync(id.Value);
            if (unitType == null)
            {
                return NotFound();
            }
            return View(unitType);
        }

        // POST: UnitTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] UnitType unitType)
        {
            if (id != unitType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitTypeRepo.UpdateAsync(unitType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitTypeRepo.Exists(unitType.Id))
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
            return View(unitType);
        }

        // GET: UnitTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _unitTypeRepo.GetByIdAsync(id.Value);
            if (unitType == null)
            {
                return NotFound();
            }

            return View(unitType);
        }

        // POST: UnitTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitTypeRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
