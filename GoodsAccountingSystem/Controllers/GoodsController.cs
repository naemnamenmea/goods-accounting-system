using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoodsAccountingSystem;
using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace GoodsAccountingSystem.Controllers
{
    public class GoodsController : Controller
    {
        private readonly DataContext _context;

        public GoodsController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Goods.ToListAsync());
        }

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

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreationDate,Name,Price,Description,Attachment")] GoodModel goodModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(goodModel);
                await _context.SaveChangesAsync();
                return Ok();                
            }
            return BadRequest();
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreationDate,Name,Price,Description,Attachment")] GoodModel goodModel)
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
