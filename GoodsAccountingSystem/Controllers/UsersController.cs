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
using GoodsAccountingSystem.ViewModels;
using AutoMapper;

namespace GoodsAccountingSystem.Controllers
{
    public class UsersController : Controller
    {
        UserManager<AppUser> _userManager;
        IMapper _mapper;

        public UsersController(
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserModel = _mapper.Map<AppUser>(model);
                var result = await _userManager.CreateAsync(UserModel, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            AppUser UserModel = await _userManager.FindByIdAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EditUserViewModel>(UserModel);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser UserModel = await _userManager.FindByIdAsync(model.Id);
                if (UserModel != null)
                {
                    _mapper.Map(model, UserModel);
                    UserModel.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(UserModel);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser UserModel = await _userManager.FindByIdAsync(id);
            if (UserModel != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(UserModel);
            }
            return RedirectToAction("Index");
        }
    }
}
