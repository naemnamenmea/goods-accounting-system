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
using Microsoft.AspNetCore.Authorization;

namespace GoodsAccountingSystem.Controllers
{
    [Authorize(Roles = Role.ADMIN)]
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

        public async Task<IActionResult> Index()
        {
            var userList = _userManager.Users.ToList();
            var res = new List<UserViewModel>();
            foreach (var user in userList)
            {
                var viewUser = _mapper.Map<UserViewModel>(user);
                var roles = await _userManager.GetRolesAsync(user);
                if(roles.Contains(Role.ADMIN))
                {
                    viewUser.Role = Role.ADMIN;
                } else
                {
                    viewUser.Role = Role.USER;
                }
                res.Add(viewUser);
            }
            return View(res);
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserModel = _mapper.Map<UserModel>(model);
                UserModel.UserName = model.Email;
                UserModel.RegisterDate = DateTime.Now;
                var result = await _userManager.CreateAsync(UserModel, model.Password);
                if (result.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync(model.UserRole))
                    {
                        if (model.UserRole == Role.ADMIN)
                        {
                            await _userManager.AddToRoleAsync(UserModel, Role.ADMIN);
                            await _userManager.UpdateAsync(UserModel);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Указанной роли " + model.UserRole + " не существует");
                    }
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
            TempData.Put(Constants.ERROR_MODAL, partialViewHtml);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            UserModel UserModel = await _userManager.FindByIdAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EditUserViewModel>(UserModel);
            var roles = await _userManager.GetRolesAsync(UserModel);
            if (roles.Contains(Role.ADMIN))
            {
                model.UserRole = Role.ADMIN;
            }
            else
            {
                model.UserRole = Role.USER;
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel UserModel = await _userManager.FindByIdAsync(model.Id);
                if (UserModel != null)
                {
                    if (model.Password != null)
                    {
                        var newPassword = _userManager.PasswordHasher.HashPassword(UserModel, model.Password);
                        UserModel.PasswordHash = newPassword;
                    }

                    if (await _roleManager.RoleExistsAsync(model.UserRole))
                    {
                        if (model.UserRole == Role.ADMIN)
                        {
                            await _userManager.AddToRoleAsync(UserModel, Role.ADMIN);
                        }
                        else
                        {
                            await _userManager.RemoveFromRoleAsync(UserModel, Role.ADMIN);
                        }

                        _mapper.Map(model, UserModel);
                        UserModel.UserName = model.Email;

                        var result = await _userManager.UpdateAsync(UserModel);
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Указанной роли " + model.UserRole + " не существует");
                    }
                }
            }
            var partialViewHtml = await this.RenderViewAsync(nameof(Edit), model, true);
            TempData.Put(Constants.ERROR_MODAL, partialViewHtml);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var model = await _userManager.FindByIdAsync(id);
            var delViewModel = _mapper.Map<DeleteUserViewModel>(model);
            if (model == null)
            {
                return NotFound();
            }

            return PartialView(delViewModel);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            UserModel UserModel = await _userManager.FindByIdAsync(id);
            if (UserModel != null)
            {
                IList<string> AllUserRoles = await _userManager.GetRolesAsync(UserModel);
                await _userManager.RemoveFromRolesAsync(UserModel, AllUserRoles);
                await _userManager.DeleteAsync(UserModel);
            }
            return RedirectToAction(nameof(Index));
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
