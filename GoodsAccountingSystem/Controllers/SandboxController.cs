﻿using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Controllers
{
    public class SandboxController : Controller
    {
        private readonly IStringLocalizer<SandboxController> _localizer;
        private readonly DataContext _context;

        public SandboxController(
            DataContext context,
            IStringLocalizer<SandboxController> localizer)
        {
            _localizer = localizer;
            _context = context;
        }

        public IActionResult Test()
        {
            TempData["test"] = "some text";
            return RedirectToAction(nameof(Index));
        }


        // GET: Sandbox
        public async Task<IActionResult> Index()
        {
            ViewData["Message"] = _localizer["localized text"];
            return View(await _context.Goods.ToListAsync());
        }
        // GET: Sandbox/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodModel = await _context.Goods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodModel == null)
            {
                return NotFound();
            }

            return View(goodModel);
        }

        // GET: Sandbox/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sandbox/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreationDate,Name,Price,Description,InStock,Attachment")] GoodModel goodModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(goodModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goodModel);
        }

        // GET: Sandbox/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodModel = await _context.Goods.FindAsync(id);
            if (goodModel == null)
            {
                return NotFound();
            }
            return View(goodModel);
        }

        // POST: Sandbox/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreationDate,Name,Price,Description,InStock,Attachment")] GoodModel goodModel)
        {
            if (id != goodModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodModelExists(goodModel.Id))
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
            return View(goodModel);
        }

        // GET: Sandbox/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodModel = await _context.Goods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodModel == null)
            {
                return NotFound();
            }

            return View(goodModel);
        }

        // POST: Sandbox/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goodModel = await _context.Goods.FindAsync(id);
            _context.Goods.Remove(goodModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodModelExists(int id)
        {
            return _context.Goods.Any(e => e.Id == id);
        }
    }
}
