using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoodsAccountingSystem;
using GoodsAccountingSystem.Models;
//using Microsoft.AspNetCore.Identity;
using GoodsAccountingSystem.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using GoodsAccountingSystem.Helpers;

namespace GoodsAccountingSystem.Controllers
{
    public class UsersController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<UserModel> _userManager;
        IMapper _mapper;

        public UsersController(
            UserManager<UserModel> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index() {
            var userList = _userManager.Users.ToList();
            var res = new List<UserViewModel>();
            foreach(var user in userList)
            {
                res.Add(_mapper.Map<UserViewModel>(user));
            }
            return View(res);
        }

        public IActionResult Create(){
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserModel = _mapper.Map<UserModel>(model);
                var result = await _userManager.CreateAsync(UserModel, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            var partialViewHtml = await this.RenderViewAsync(nameof(Create), model, true);
            TempData.Put("CreateErrorModal", partialViewHtml);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            UserModel UserModel = await _userManager.FindByIdAsync(id + "");
            if (UserModel == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EditUserByAdminViewModel>(UserModel);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserByAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel UserModel = await _userManager.FindByIdAsync(model.Id + "");
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
            UserModel UserModel = await _userManager.FindByIdAsync(id);
            if (UserModel != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(UserModel);
            }
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> ChangePassword(string id)
        //{
        //    User user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    ChangePasswordAdminViewModel model = new ChangePasswordAdminViewModel { Id = user.Id, Email = user.Email };
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordAdminViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await _userManager.FindByIdAsync(model.Id);
        //        if (user != null)
        //        {
        //            var _passwordValidator =
        //                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
        //            var _passwordHasher =
        //                HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

        //            IdentityResult result =
        //                await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
        //                await _userManager.UpdateAsync(user);
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError(string.Empty, error.Description);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Пользователь не найден");
        //        }
        //    }
        //    return View(model);
        //}
    }
}
