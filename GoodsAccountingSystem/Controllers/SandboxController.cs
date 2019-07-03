using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoodsAccountingSystem;
using GoodsAccountingSystem.Models;

namespace GoodsAccountingSystem.Controllers
{
    public class SandboxController : Controller
    {
        private readonly AppDbContext _context;

        public SandboxController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sandbox
        public async Task<IActionResult> Index()
        {
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
