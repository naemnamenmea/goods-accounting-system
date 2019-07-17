using AutoMapper;
using GoodsAccountingSystem.Helpers;
using GoodsAccountingSystem.Models;
using GoodsAccountingSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Controllers
{
    public class GoodsController : Controller
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        IHostingEnvironment _appEnvironment;

        public GoodsController(
            DataContext context,
            IMapper mapper,
            IHostingEnvironment appEnvironment
            )
        {
            _context = context;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
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

        [Authorize]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Description,AttachmentUpload")] CreateGoodViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newGood = _mapper.Map<GoodModel>(model);
                newGood.CreationDate = DateTime.Now;
                _context.Goods.Add(newGood);
                _context.SaveChanges();

                if (model.AttachmentUpload != null)
                {
                    string extension = Path.GetExtension(model.AttachmentUpload.FileName);
                    string path = "/Files/" + newGood.Id + extension;

                    //var Stream = model.AttachmentUpload.OpenReadStream();
                    //this.ResizeAndSaveImage(Stream, _appEnvironment.WebRootPath + path);

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.AttachmentUpload.CopyToAsync(fileStream);
                    }

                    newGood.Attachment = path;
                    _context.SaveChanges();
                }

            }
            else
            {
                var partialViewHtml = await this.RenderViewAsync(nameof(Create), model, true);
                TempData.Put(Constants.ERROR_MODAL, partialViewHtml);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Goods.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<EditGoodViewModel>(model);
            return PartialView(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,AttachmentUpload")] EditGoodViewModel viewModel)
        {
            var model = _context.Goods.Find(id);
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mapper.Map(viewModel, model);
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodModelExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (viewModel.AttachmentUpload != null)
                {
                    string extension = Path.GetExtension(viewModel.AttachmentUpload.FileName);
                    string path = "/Files/" + viewModel.Id + extension;
                    string absPath = _appEnvironment.WebRootPath + model.Attachment;

                    if (path != model.Attachment && System.IO.File.Exists(absPath))
                    {
                        System.IO.File.Delete(absPath);
                    }
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await viewModel.AttachmentUpload.CopyToAsync(fileStream);
                    }
                    model.Attachment = path;
                    _context.SaveChanges();
                }
            }
            else
            {
                var partialViewHtml = await this.RenderViewAsync(nameof(Edit), viewModel, true);
                TempData.Put(Constants.ERROR_MODAL, partialViewHtml);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
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

        [Authorize]
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Goods.FindAsync(id);
            string absPath = _appEnvironment.WebRootPath + model.Attachment;
            if(System.IO.File.Exists(absPath))
            {
                System.IO.File.Delete(absPath);
            }
            _context.Goods.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires=DateTimeOffset.UtcNow.AddYears(1)}
                );
            return LocalRedirect(returnUrl);
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
