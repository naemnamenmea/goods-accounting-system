﻿using AutoMapper;
using GoodsAccountingSystem.Models;
using GoodsAccountingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserModel, RegisterViewModel>();
            CreateMap<UserModel, UserViewModel>();
            CreateMap<UserModel, EditUserViewModel>();
            CreateMap<GoodModel, EditGoodViewModel>();
            CreateMap<EditUserViewModel, UserModel>();
            CreateMap<UserModel, DeleteUserViewModel>();
            CreateMap<UserViewModel, UserModel>();
            CreateMap<RegisterViewModel, UserModel>();
            CreateMap<CreateUserViewModel, UserModel>();

            CreateMap<CreateGoodViewModel, GoodModel>();
            CreateMap<EditGoodViewModel, GoodModel>().ForMember(
                to => to.Id, from => from.Ignore());
            CreateMap<GoodModel, CreateGoodViewModel>();
            CreateMap<GoodModel, GoodViewModel>();
        }
    }
}
