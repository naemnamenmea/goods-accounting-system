using AutoMapper;
using GoodsAccountingSystem.Helpers;
using GoodsAccountingSystem.Models;
using GoodsAccountingSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Controllers
{
    public class GoodsController : Controller
    {
        private readonly DataContext _context;
        private IMapper _mapper;

        public GoodsController(
            DataContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Goods.Select(good => _mapper.Map<GoodViewModel>(good)).ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Name,Price,Description,Attachment")] CreateGoodViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newGood = _mapper.Map<GoodModel>(model);
                newGood.CreationDate = DateTime.Now;
                newGood.InStock = true;
                _context.Goods.Add(newGood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(model);
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
            return PartialView(goodModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreationDate,Name,Price,Description,Attachment")] GoodModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodModelExists(model.Id))
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
            TempData.Put("EditErrors", model);
            return RedirectToAction(nameof(Index));
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

            return PartialView(goodModel);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool GoodModelExists(int id)
        {
            return _context.Goods.Any(e => e.Id == id);
        }
    }
}
