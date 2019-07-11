using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoodsAccountingSystem;
using GoodsAccountingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GoodsAccountingSystem.Controllers
{
    public class AdminController : Controller
    {
        UserManager<UserModel> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public AdminController(
            UserManager<UserModel> manager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = manager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = RoleCategory.Admin.ToString())]
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        public IActionResult GetRoles()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
        //        // добавляем пользователя
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            // установка куки
        //            await _signInManager.SignInAsync(user, false);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        //// GET: Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userModel = await _userManager.UserModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (userModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userModel);
        //}

        //// GET: Users/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("FirstName,SecondName,MiddleName,RegisterDate,BirthDate,Password,Activity,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] UserModel userModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _userManager.Add(userModel);
        //        await _userManager.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userModel);
        //}

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userModel = await _userManager.UserModel.FindAsync(id);
        //    if (userModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userModel);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("FirstName,SecondName,MiddleName,RegisterDate,BirthDate,Password,Activity,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] UserModel userModel)
        //{
        //    if (id != userModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _userManager.Update(userModel);
        //            await _userManager.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserModelExists(userModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userModel);
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userModel = await _userManager.UserModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (userModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userModel);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var userModel = await _userManager.UserModel.FindAsync(id);
        //    _userManager.UserModel.Remove(userModel);
        //    await _userManager.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserModelExists(int id)
        //{
        //    return _userManager.UserModel.Any(e => e.Id == id);
        //}
    }
}
